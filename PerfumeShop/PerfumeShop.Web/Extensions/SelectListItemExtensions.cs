namespace PerfumeShop.Web.Extensions;

public static class SelectListItemExtensions
{
    public static List<SelectListItem> ToSelectListItems(this IEnumerable<ItemViewModel> items)
    {
        return items.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).OrderBy(i => i.Text).ToList();
    }

    public static List<SelectListItem> ToSelectListItems(this IEnumerable<ItemViewModel> items, SelectListItem defaultItem)
    {
        var selectList = items.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).OrderBy(i => i.Text).ToList();
        selectList.Insert(0, defaultItem);
        return selectList;
    }

    public static List<SelectListItem> ToSelectListItems(this IEnumerable<string> items)
    {
        return items.Select(i => new SelectListItem { Value = i, Text = i }).OrderBy(i => i.Text).ToList();
    }
}
