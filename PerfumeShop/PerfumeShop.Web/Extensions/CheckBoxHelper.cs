namespace PerfumeShop.Web.Extensions;

public static class CheckBoxHelper
{
	public static List<CheckBoxViewModel> GetCheckBoxList<T>() where T : Enum
	{
		var checkBox = new List<CheckBoxViewModel>();

		foreach (var item in Enum.GetValues(typeof(T)))
		{
			checkBox.Add(new CheckBoxViewModel((int)item, item.ToString(), false));
		}
		
		return checkBox;
	}
}
