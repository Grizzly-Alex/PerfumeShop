namespace PerfumeShop.Web.ViewModels;

public sealed class NotificationViewModel
{
    public NotificationStatus? Status { get; init; }    
    public string? Text { get; init; }

    public NotificationViewModel()
    {
        
    }

    public NotificationViewModel(NotificationStatus? status, string? text)
    {
        Status = status ?? NotificationStatus.Info;
        Text = text ?? string.Empty;           
    }
}
