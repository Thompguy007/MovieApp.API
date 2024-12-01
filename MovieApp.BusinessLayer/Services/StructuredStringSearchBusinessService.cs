public class StructuredStringSearchBusinessService
{
    private readonly StructuredStringSearchService _searchService;

    public StructuredStringSearchBusinessService(StructuredStringSearchService searchService)
    {
        _searchService = searchService;
    }

    public async Task<List<StructuredStringSearchResult>> SearchAsync(
        string titleOfMovie, string plotDesc, string characterName, string actorName)
    {
        return await _searchService.SearchMoviesAsync(titleOfMovie, plotDesc, characterName, actorName);
    }
}
