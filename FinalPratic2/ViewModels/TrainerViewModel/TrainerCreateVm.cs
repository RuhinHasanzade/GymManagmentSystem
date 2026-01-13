using System.ComponentModel.DataAnnotations;

namespace FinalPratic2.ViewModels.TrainerViewModel
{
    public class TrainerCreateVm
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; } = string.Empty;

        [Range(18,80)]
        public int Age { get; set; }

        [Range(3,30)]
        public int Experience { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public IFormFile Image { get; set; }


    }
}
