using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDBX.Com.Core;
namespace PersistentLayer
{
    public class PersistentModel<Key, T> : IPersistentModel<Key, T>
        where T : BaseModel
        where Key : IEnumerable<String>
    {
        public PersistentModel(Key key)
        {
            if (key.Count() == 1)
            {
                MongoDBX = new MongoDBX<T>(key.ToArray()[0]);
            }
            else if (key.Count() > 1)
            {
                foreach (var item in key)
                {
                    var dBXs = new MongoDBX<T>(item);
                    MongoDBXList.Append(dBXs);
                }
            }
            else
            {
                throw new Exception("多个document更新未实现");
            }
        }
        public IMongoDBX<T> MongoDBX { get; set; }

        public IEnumerable<IMongoDBX<T>>  MongoDBXList{get;set;}
    }
}
