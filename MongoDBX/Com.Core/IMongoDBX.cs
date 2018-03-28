using Common;

namespace MongoDBX.Com.Core
{
    public interface IMongoDBX<T> where T : BaseModel
    {
        IExtentions<T> Extentions { get; set; }
        IMongoContext Mongo { get; set; }
    }
}