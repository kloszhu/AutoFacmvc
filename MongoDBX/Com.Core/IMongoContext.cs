using Common;
using MongoDB.Driver;

namespace MongoDBX.Com.Core
{
    public interface IMongoContext
    {
        MongoClient Client { get; set; }
        IMongoDatabase DataBase { get; set; }
        IMongoCollection<T> GetCollection<T>() where T : BaseModel;
        IMongoCollection<T> GetCollection<T>(string documentName) where T : BaseModel;
        IMongoCollection<T> GetCommonCollection<T>(string documentName);
    }
}