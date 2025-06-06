using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LabApp.Dtos;


namespace LabApp.AdminPanel.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Wprowadü login i has≥o.";
                return Page(); 
            }


            var http = new HttpClient();
            var response = await http.PostAsJsonAsync("https://localhost:7199/api/Auth/login", new
            {
                Username = Username,
                Password = Password
            });

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Nieprawid≥owy login lub has≥o.";
                return Page();
            }

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (result is null || string.IsNullOrEmpty(result.Token))
            {
                ErrorMessage = "B≥πd logowania.";
                return Page();
            }

            Response.Cookies.Append("jwt", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return RedirectToPage("/Admin");
        }
    }
    }

