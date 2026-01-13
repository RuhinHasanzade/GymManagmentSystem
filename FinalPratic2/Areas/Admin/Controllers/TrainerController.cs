using FinalPratic2.Context;
using FinalPratic2.Helpers;
using FinalPratic2.Models;
using FinalPratic2.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FinalPratic2.Areas.Admin.Controllers;
[Area("Admin")]

public class TrainerController : Controller
{

    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string folderPath;

    public TrainerController(AppDbContext context , IWebHostEnvironment environment)
    {
        _context= context;
        _environment= environment;
        folderPath = Path.Combine(_environment.WebRootPath, "images");
    }

    public async Task<IActionResult> Index()
    {
        //var trainers =  _context.Trainers.Include(t => t.Category).ToList();
        var resultVm = await _context.Trainers.Select(t => new TrainerGetVm()
        {
            Id = t.Id,
            Age = t.Age,
            Description = t.Description,
            Name = t.Name,
            CategoryName = t.Category.Name,
            Experience = t.Experience,
            ImagePath = t.ImagePath
        }).ToListAsync();

        return View(resultVm);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await SendCategoryWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrainerCreateVm vm)
    {
        await SendCategoryWithViewBag();

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var isExistCategory = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);

        if(!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "This Category not found");
        }

        if(vm.Image.Length > 2 * Math.Pow(2,20))
        {
            ModelState.AddModelError("Image", "Max size image 2MB");
            return View(vm);
        }

        if(!vm.Image.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image", "Only Images!!");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.FileUploadAsync(folderPath);

        Trainer trainer = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            Age = vm.Age,
            Experience = vm.Experience,
            CategoryId = vm.CategoryId,
            ImagePath = uniqueFileName,
        };

        await _context.Trainers.AddAsync(trainer);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        
        var trainer =  await _context.Trainers.FindAsync(id);
        if (trainer == null)
        {
            return NotFound();
        }

        TrainerUpdateVm vm = new()
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Description = trainer.Description,
            Age = trainer.Age,
            Experience = trainer.Experience,
            CategoryId = trainer.CategoryId,
        };

        await SendCategoryWithViewBag();

        return View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> Update(TrainerUpdateVm vm)
    {
        await SendCategoryWithViewBag();

        if (!ModelState.IsValid)
            return View(vm);

        var existTrainer = await _context.Trainers.FindAsync(vm.Id);

        if (existTrainer == null)
            return BadRequest();

        var isExistCategory = await _context.Categories.AnyAsync(c => c.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("CategoryId", "This Category is not found");
            return View(vm);
        }
        if (vm.Image.Length > 2 * Math.Pow(2, 20))
        {
            ModelState.AddModelError("Image", "Max size image 2MB");
            return View(vm);
        }

        if (!vm.Image.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image", "Only Images!!");
            return View(vm);
        }

        existTrainer.Age = vm.Age;
        existTrainer.Name = vm.Name;
        existTrainer.Description = vm.Description;
        existTrainer.Experience = vm.Experience;
        existTrainer.CategoryId = vm.CategoryId;

        if(vm.Image is not null)
        {
            string newImagePath = await vm.Image.FileUploadAsync(folderPath);

            string deleteImage = Path.Combine(folderPath, existTrainer.ImagePath);

            FileHelper.FileDelete(deleteImage);

            existTrainer.ImagePath = newImagePath;
        }

        _context.Trainers.Update(existTrainer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }


    public async Task<IActionResult> Delete(int id)
    {
        var trainer = await _context.Trainers.FindAsync(id);
        if(trainer is null)
        {
            return NotFound();
        }

        _context.Trainers.Remove(trainer);
        await _context.SaveChangesAsync();
        string deletedPath = Path.Combine(folderPath, trainer.ImagePath);
        FileHelper.FileDelete(deletedPath);
        return RedirectToAction(nameof(Index));
    }
    



    private async Task SendCategoryWithViewBag()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;

    }
}
