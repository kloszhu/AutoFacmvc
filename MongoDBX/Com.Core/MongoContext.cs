using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver.Core;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.GeoJsonObjectModel.Serializers;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using Common;

namespace MongoDBX.Com.Core
{
    public class MongoContext : IMongoContext
    {
        public static readonly string connectionString = "mongodb://localhost:27017";
        public static readonly string database = "CommonDB";
        /// <summary>
        /// 默认连接
        /// </summary>
        public MongoContext()
        {
            Client = new MongoClient(connectionString);
            DataBase = Client.GetDatabase(database);
           
        }

        /// <summary>
        /// 更换连接池
        /// </summary>
        /// <param name="connectionName"></param>
        public MongoContext(string connectionName)
        {
            Client = new MongoClient(connectionName);
            DataBase = Client.GetDatabase(database);
        }

        public MongoContext(MongoClient client, IMongoDatabase dataBase)
        { 
            Client = client;
            DataBase = dataBase;
        }

        public  IMongoCollection<T> GetCollection<T>(string documentName) where T : BaseModel
        {
          return  DataBase.GetCollection<T>(documentName);
        }

        public IMongoCollection<T> GetCollection<T>() where T : BaseModel
        {
            return DataBase.GetCollection<T>(typeof(T).Name.ToLower());
        }
        /// <summary>
        /// 根据实体未集成操作数据
        /// </summary>
        /// <typeparam name="T"></typeparam >
        /// <param name="documentName"></param>
        /// <returns></returns>
        public IMongoCollection<T> GetCommonCollection<T>(string documentName)
        {
            return DataBase.GetCollection<T>(documentName);
        }

        public override bool Equals(object obj)
        {
            var context = obj as MongoContext;
            return context != null &&
                   EqualityComparer<MongoClient>.Default.Equals(Client, context.Client) &&
                   EqualityComparer<IMongoDatabase>.Default.Equals(DataBase, context.DataBase);
        }

        public override int GetHashCode()
        {
            var hashCode = 218492390;
            hashCode = hashCode * -1521134295 + EqualityComparer<MongoClient>.Default.GetHashCode(Client);
            hashCode = hashCode * -1521134295 + EqualityComparer<IMongoDatabase>.Default.GetHashCode(DataBase);
            return hashCode;
        }

        public MongoClient Client { get; set; }

        public IMongoDatabase DataBase { get; set; }

      

    }
}
//    #region 扩展
//    public class MongoHelper
//    {
//        public static readonly string connectionString = "Servers=127.0.0.1:2222;ConnectTimeout=30000;ConnectionLifetime=300000;MinimumPoolSize=8;MaximumPoolSize=256;Pooled=true";
//        public static readonly string database = "DiDiDataBase";

//    #region 新增
//        /// <summary>
//        /// 插入新数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="collectionName"></param>
//        /// <param name="entiry"></param>
//        public static void InsertOne<T>(string collectionName, T entity) where T : class
//        {


//            IMongoClient mongo = new MongoClient(connectionString);
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            categories.InsertOne(entity);



//        }
//        /// <summary>
//        /// 插入多个数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="collectionName"></param>
//        /// <param name="entiry"></param>
//        public static void InsertAll<T>(string collectionName, IEnumerable<T> entity) where T : class
//        {

//            IMongoClient mongo = new MongoClient(connectionString);
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories =  friends.GetCollection<T>(collectionName);
//            categories.InsertMany(entity);


//        }
//    }
//    #endregion

//    #region 更新
//    /// <summary>
//    /// 更新操作
//    /// </summary>
//    /// <typeparam name="T">类型</typeparam>
//    /// <param name="collectionName">表名</param>
//    /// <param name="query">条件</param>
//    /// <param name="entry">新实体</param>
//    public static void Update<T>(string collectionName, T entity, T query) 
//    {
//        IMongoClient mongo = new MongoClient(connectionString);
//        IMongoDatabase friends = mongo.GetDatabase(database);
//        IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            categories.UpdateOne(entity, query);
          
        
//    }
//    /// <summary>
//    /// 更新操作
//    /// </summary>
//    /// <typeparam name="T">类型</typeparam>
//    /// <param name="collectionName">表名</param>
//    /// <param name="query">条件</param>
//    /// <param name="entry">新实体</param>
//    public static void UpdateAll<T>(string collectionName, Document entity, Document query) where T : class
//    {
//        using (Mongo mongo = new Mongo(connectionString))
//        {
//            mongo.Connect();
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            categories.Update(entity, query, UpdateFlags.MultiUpdate, true);
//            mongo.Disconnect();
//        }
//    }
//    #endregion

//    #region 查询
//    /// <summary>
//    /// 获取一条数据
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="collectionName"></param>
//    /// <param name="query"></param>
//    /// <returns></returns>
//    public static T GetOne<T>(string collectionName, Document query) where T : class
//    {
//        T result = default(T);
//        using (Mongo mongo = new Mongo(connectionString))
//        {
//            mongo.Connect();
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            result = categories.FindOne(query);
//            mongo.Disconnect();

//        }
//        return result;
//    }
//    /// <summary>
//    /// 获取一条数据
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="collectionName"></param>
//    /// <param name="query"></param>
//    /// <returns></returns>
//    public static T GetOne<T>(string collectionName, Document query, Document fields) where T : class
//    {
//        T result = default(T);
//        using (Mongo mongo = new Mongo(connectionString))
//        {
//            mongo.Connect();
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            result = categories.Find(query, fields).Skip(0).Limit(1).Documents.First();
//            mongo.Disconnect();

//        }
//        return result;
//    }
//    /// <summary>
//    /// 获取一个集合下所有数据
//    /// </summary>
//    /// <param name="collectionName"></param>
//    /// <returns></returns>
//    public static List<T> GetAll<T>(string collectionName) where T : class
//    {
//        List<T> result = new List<T>();
//        using (Mongo mongo = new Mongo(connectionString))
//        {
//            mongo.Connect();
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            foreach (T entity in categories.FindAll().Limit(50).Documents)
//            {
//                result.Add(entity);
//            }
//            mongo.Disconnect();

//        }
//        return result;
//    }
//    /// <summary>
//    /// 获取列表
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="collectionName"></param>
//    /// <param name="query"></param>
//    /// <param name="Sort"></param>
//    /// <param name="cp"></param>
//    /// <param name="mp"></param>
//    /// <returns></returns>
//    public static List<T> GetList<T>(string collectionName, object selector, Document sort, int cp, int mp) where T : class
//    {
//        List<T> result = new List<T>();
//        using (Mongo mongo = new Mongo(connectionString))
//        {
//            mongo.Connect();
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            foreach (T entity in categories.Find(selector).Sort(sort).Skip((cp - 1) * mp).Limit(mp).Documents)
//            {
//                result.Add(entity);
//            }
//            mongo.Disconnect();

//        }
//        return result;
//    }
//    /// <summary>
//    /// 获取列表
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="collectionName"></param>
//    /// <param name="query"></param>
//    /// <param name="Sort"></param>
//    /// <param name="cp"></param>
//    /// <param name="mp"></param>
//    /// <returns></returns>
//    public static List<T> GetList<T>(string collectionName, object selector, object fields, Document sort, int cp, int mp) where T : class
//    {
//        List<T> result = new List<T>();
//        using (Mongo mongo = new Mongo(connectionString))
//        {
//            mongo.Connect();
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            foreach (T entity in categories.Find(selector, fields).Sort(sort).Skip((cp - 1) * mp).Limit(mp).Documents)
//            {
//                result.Add(entity);
//            }
//            mongo.Disconnect();

//        }
//        return result;
//    }
//    #endregion


//    #region 删除
//    /// <summary>
//    /// 删除数据
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="collectionName"></param>
//    /// <param name="entity"></param>
//    public static void Delete<T>(string collectionName, Document query) where T : class
//    {
//        using (Mongo mongo = new Mongo(connectionString))
//        {
//            mongo.Connect();
//            IMongoDatabase friends = mongo.GetDatabase(database);
//            IMongoCollection<T> categories = friends.GetCollection<T>(collectionName);
//            categories.Remove(query, true);
//            mongo.Disconnect();
//        }
//    }
//    #endregion
//}
//#endregion

