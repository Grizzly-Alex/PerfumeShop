namespace PerfumeShop.Web.Extensions;

public static class CheckBoxHelper
{
	public static List<CheckBoxViewModel> GetCheckBoxList<TEnum>() where TEnum : Enum
	{
		var checkBox = new List<CheckBoxViewModel>();

		foreach (var item in Enum.GetValues(typeof(TEnum)))
		{
			checkBox.Add(new CheckBoxViewModel((int)item, item.ToString(), false));
		}
		
		return checkBox;
	}

	public static List<CheckBoxViewModel> GetCheckBoxList<TEnum>(List<TEnum> enumList) where TEnum : Enum
	{
		var checkBox = new List<CheckBoxViewModel>();

		foreach (var item in Enum.GetValues(typeof(TEnum)))
		{
			checkBox.Add(new CheckBoxViewModel((int)item, item.ToString(), enumList.Contains((TEnum)item)));
		}

		return checkBox;
	}

	public static List<TEnum> GetСheckedItems<TEnum>(List<CheckBoxViewModel> checkedItems)
		where TEnum : Enum	
		=> checkedItems.Where(i => i.IsChecked).Select(i => i.Id.ToEnum<TEnum>()).ToList()!;
}
