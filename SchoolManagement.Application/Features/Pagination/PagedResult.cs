namespace SchoolManagement.Application.Features.Pagination;

public class PagedResult<T>
{
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public IEnumerable<T> Items { get; set; } = new List<T>();
}