using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MongoDBX.Com.Core
{
    public class MongoDBX<T> : IMongoDBX<T> where T:BaseModel
    {
        public MongoDBX() {
            Mongo = new MongoContext();
            Extentions= new Extentions<T>(typeof(T).Name.ToLower());
        }
        public MongoDBX(string DocumentName) {
            Mongo = new MongoContext();
            Extentions = new Extentions<T>(DocumentName);
        }
        public IMongoContext Mongo { get; set; }

       public IExtentions<T> Extentions { get; set; }
    }
}
