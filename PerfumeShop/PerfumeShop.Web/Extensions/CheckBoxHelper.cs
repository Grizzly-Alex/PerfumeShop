namespace PerfumeShop.Web.Extensions;

public static class CheckBoxHelper
{
	public static List<CheckboxItem> GetCheckBoxList<TEnum>() where TEnum : Enum
	{
		var checkBox = new List<CheckboxItem>();

		foreach (var item in Enum.GetValues(typeof(TEnum)))
		{
			var @enum = (TEnum)item;
			checkBox.Add(new CheckboxItem((int)item, @enum.GetDisplayName(), false));
		}
		
		return checkBox;
	}

	public static List<CheckboxItem> GetCheckBoxList<TEnum>(List<TEnum> enumList) where TEnum : Enum
	{
		var checkBox = new List<CheckboxItem>();

		foreach (var item in Enum.GetValues(typeof(TEnum)))
		{
            var @enum = (TEnum)item;
            checkBox.Add(new CheckboxItem((int)item, @enum.GetDisplayName(), enumList.Contains((TEnum)item)));
		}

		return checkBox;
	}

	public static List<TEnum> GetСheckedItems<TEnum>(List<CheckboxItem> checkedItems)
		where TEnum : Enum	
		=> checkedItems.Where(i => i.IsChecked).Select(i => i.Id.ToEnum<TEnum>()).ToList()!;
}
