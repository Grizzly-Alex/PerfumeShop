namespace PerfumeShop.Core.Models;

public sealed class PagedList<TItem> : IPagedList<TItem>
{
    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public int ItemsPerPage { get; init; }
    public int TotalItems { get; init; }
    public bool HasPreviousPage => PageIndex > 0;
    public bool HasNextPage => PageIndex + 1 < TotalPages;
    public IList<TItem> Items { get; init; }

    public PagedList(IEnumerable<TItem> items, int pageIndex, int itemsPerPage, int totalItems)
    {
        Items = items.ToList();
        PageIndex = pageIndex;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)ItemsPerPage);
    }

    public PagedList() => Items = Array.Empty<TItem>();
}
