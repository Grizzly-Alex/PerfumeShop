namespace PerfumeShop.Core.Interfaces;

public interface IPagedList<TItem>
{
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int ItemsPerPage { get; }
    public int TotalItems { get; }
    public bool HasPreviousPage { get; }
    public bool HasNextPage { get; }
    public IList<TItem> Items { get; }
}
