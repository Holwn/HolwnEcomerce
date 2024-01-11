using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace HolwnEcommerce.Public.Web.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public RegisterModel(IConfiguration configuration) {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Redirect(_configuration["AuthServer:Authority"] + "/" + "Account/Register");
        }
    }
}
