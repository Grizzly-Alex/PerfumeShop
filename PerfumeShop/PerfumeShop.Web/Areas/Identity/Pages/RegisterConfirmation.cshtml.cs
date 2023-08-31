using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PerfumeShop.Web.Areas.Identity.Pages;

public class RegisterConfirmationModel : PageModel
{
    private readonly ILogger<RegisterConfirmationModel> _logger;
    public RegisterConfirmationModel(ILogger<RegisterConfirmationModel> logger)
    {
        _logger = logger;           
    }

    public ResultViewModel Result { get; set; }

    public void OnGet(string email, bool isSendedEmail)
    {
        Result = new()
        {
            Success = isSendedEmail,
            StatusMessage = isSendedEmail
            ? $""
            : $""
        };
    }
}
