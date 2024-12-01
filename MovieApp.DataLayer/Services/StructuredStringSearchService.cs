using MovieApp.DataLayer;
using Npgsql;

public class StructuredStringSearchService
{
    private readonly MovieContext _dbContext;

    public StructuredStringSearchService(MovieContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StructuredStringSearchResult>> SearchMoviesAsync(
        string titleOfMovie, string plotDesc, string characterName, string actorName)
    {
        var query = "SELECT * FROM structured_string_search(@titleOfMovie, @plotDesc, @characterName, @actorName)";
        return await _dbContext.StructuredStringSearchResults
            .FromSqlRaw(query,
                new NpgsqlParameter("titleOfMovie", titleOfMovie ?? (object)DBNull.Value),
                new NpgsqlParameter("plotDesc", plotDesc ?? (object)DBNull.Value),
                new NpgsqlParameter("characterName", characterName ?? (object)DBNull.Value),
                new NpgsqlParameter("actorName", actorName ?? (object)DBNull.Value))
            .ToListAsync();
    }
}
