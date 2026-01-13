using FinalPratic2.Models.Common;

namespace FinalPratic2.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Trainer> Trainers = [];
    }
}
