using FinalPratic2.Context;
using FinalPratic2.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FinalPratic2.Areas.Admin.Controllers;
[Area("Admin")]

public class TrainerController(AppDbContext _context, IWebHostEnvironment _enviroment) : Controller
{
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


    



    private async Task SendCategoryWithViewBag()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;

    }
}
