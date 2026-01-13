namespace FinalPratic2.ViewModels.TrainerViewModel;

public class TrainerGetVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Age { get; set; }
    public int Experience { get; set; }

    public string ImagePath { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    
}
