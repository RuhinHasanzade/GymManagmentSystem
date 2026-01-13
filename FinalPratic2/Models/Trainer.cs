using FinalPratic2.Models.Common;

namespace FinalPratic2.Models
{
    public class Trainer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Experience { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public Category? Category { get; set; } 
    }
}
