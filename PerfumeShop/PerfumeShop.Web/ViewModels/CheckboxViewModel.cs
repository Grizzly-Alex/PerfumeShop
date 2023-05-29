namespace PerfumeShop.Web.ViewModels;

public sealed class CheckBoxViewModel
{
	public int Id { get; set; }
	public string LabelName { get; set; } 
	public bool IsChecked { get; set; }

    public CheckBoxViewModel(int id, string labelName, bool isChecked)
    {
        Id = id;
        LabelName = labelName;
        IsChecked = isChecked;            
    }
}
