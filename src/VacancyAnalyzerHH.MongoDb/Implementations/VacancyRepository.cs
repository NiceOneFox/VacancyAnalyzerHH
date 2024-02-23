using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using VacancyAnalyzerHH.Core.Repositories;

namespace VacancyAnalyzerHH.MongoDb.Implementations;

/// <summary>
/// Репозиторий для работы с вакансиями
/// </summary>
public class VacancyRepository : MongoDbContext, IVacancyRepository
{
    private readonly IMongoCollection<BsonDocument> _vacancyCollection;

    public VacancyRepository(IMongoClient mongoClient)
        : base(mongoClient)
    {
        _vacancyCollection = MongoDatabase.GetCollection<BsonDocument>("Vacancies");
    }

    /// <summary>
    /// Вставка одной вакансии
    /// </summary>
    /// <param name="vacancy">json вакансии</param>
    /// <returns></returns>
    public async Task InsertOneAsync(string vacancy)
    {
        var document = BsonSerializer.Deserialize<BsonDocument>(vacancy);

        await _vacancyCollection.InsertOneAsync(document);
    }
}
