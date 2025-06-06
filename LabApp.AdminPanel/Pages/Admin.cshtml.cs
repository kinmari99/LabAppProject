using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace LabApp.AdminPanel.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
