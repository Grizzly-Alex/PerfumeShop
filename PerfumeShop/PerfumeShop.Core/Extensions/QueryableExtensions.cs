namespace PerfumeShop.Core.Extensions;

public static class QueryableExtensions
{
    public static async Task<IPagedList<TItem>> ToPagedListAsync<TItem>(this IQueryable<TItem> source,
        int pageIndex, int itemsPerPage, CancellationToken cancellationToken = default)
    {
        var totalItems = await source
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);
        var items = await source
            .Skip(pageIndex * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedList<TItem>(items, pageIndex, itemsPerPage, totalItems);
    }
}