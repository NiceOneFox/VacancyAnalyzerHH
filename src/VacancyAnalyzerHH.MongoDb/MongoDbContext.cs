using MongoDB.Driver;

namespace VacancyAnalyzerHH.MongoDb;

public class MongoDbContext
{
    protected readonly IMongoDatabase MongoDatabase;

    public MongoDbContext(IMongoClient mongoClient)
    {
        MongoDatabase = mongoClient.GetDatabase("VacancyAnalyzerHH");
    }
}
