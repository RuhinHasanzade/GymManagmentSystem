using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;

namespace FinalPratic2.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
