namespace PerfumeShop.Web.UIModels;

public sealed class CheckboxItem
{
    public int Id { get; set; }
    public string LabelName { get; set; }
    public bool IsChecked { get; set; }

    public CheckboxItem()
    {
    }

    public CheckboxItem(int id, string labelName, bool isChecked)
    {
        Id = id;
        LabelName = labelName;
        IsChecked = isChecked;
    }
}
