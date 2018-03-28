using System.Collections.Generic;
using Common;
using MongoDBX.Com.Core;

namespace PersistentLayer
{
    public interface IPersistentModel<Key,T> 
        where T : BaseModel
        where Key:IEnumerable<string>
    {
        IMongoDBX<T> MongoDBX { get; set; }
        IEnumerable<IMongoDBX<T>> MongoDBXList { get; set; }
    }
}