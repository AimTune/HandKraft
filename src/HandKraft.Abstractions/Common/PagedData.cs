namespace HandKraft.Abstractions.Common;

public class PagedData<TEntity>
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    
    public List<TEntity> Items { get; set; } = new();
    public int TotalCount { get; set; }
    
    public int PageNumber 
    { 
        get => _pageNumber;
        set => _pageNumber = value > 0 ? value : 1;
    }
    
    public int PageSize 
    { 
        get => _pageSize;
        set => _pageSize = value > 0 ? value : 10;
    }
    
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize; 
}