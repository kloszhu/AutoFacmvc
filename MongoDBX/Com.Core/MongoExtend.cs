using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using Common;

namespace MongoDBX.Com.Core
{
    public static class MongoExtend
    {
        public static void Add<T>(this IMongoCollection<T> collenction, T Model) where T : BaseModel
                  => collenction.InsertOne(Model);
        public static Task AddAsync<T>(this IMongoCollection<T> collenction, T Model) => collenction.InsertOneAsync(Model);
        public static void AddList<T>(this IMongoCollection<T> collenction, IEnumerable<T> Models) where T : BaseModel
                 => collenction.InsertMany(Models);

        public static Task AddListAsync<T>(this IMongoCollection<T> collenction, IEnumerable<T> Models)
            => collenction.InsertManyAsync(Models);

        /// <summary>
        /// 查找第一个
        /// </summary>
        public static T FirstOrDefault<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression) where T : BaseModel
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return collenction.Find(expression).FirstOrDefault();
        }

        public static T GetModel<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression) where T : BaseModel
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return collenction.Find(expression).FirstOrDefault();
        }
        public static List<T> GetList<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression) where T : BaseModel
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return collenction.Find(expression).ToList();
        }

        public static long GetCount<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression) where T : BaseModel
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return collenction.Count(expression);
        }

        public static IEnumerable<T> GetPageList<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression,Expression<Func<T,object>> SortExpression,int? pagesize,int? CurrentPage,ref long count) where T : BaseModel
        {
            if (!pagesize.HasValue)
            {
                pagesize = 1;
            }
            if (!CurrentPage.HasValue)
            {
                CurrentPage = 1;
            }

            count= GetCount(collenction,expression);
         
            //BsonDocument doc = BsonExtensionMethods.ToBsonDocument(model);
            //var query = Builders<BsonDocument>.Filter.Eq("_id", model._id);
            return collenction.Find(expression).SortByDescending(SortExpression).Skip(pagesize * (CurrentPage-1)).Limit(pagesize).ToEnumerable();

        }

        /// <summary>
        /// 查找符合数据列表
        /// </summary>
        public static IEnumerable<T> FindToList<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression) where T : BaseModel
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return collenction.Find(expression).ToList();
        }


        //public static long UpdateOne<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression, T model) where T : BaseModel
        //{
        //    //BsonDocument doc = BsonExtensionMethods.ToBsonDocument(model);
        //    //var query = Builders<BsonDocument>.Filter.Eq("_id", model._id);
        //    UpdateDefinition
        //    var result= collenction.UpdateOneAsync<T>(filter,)

        //}
        //public static long UpdateMany<T>(this IMongoCollection<T> collenction, IEnumerable<T> model) where T : BaseModel
        //{
        //    //BsonDocument doc = BsonExtensionMethods.ToBsonDocument(model);
        //    //var query = Builders<BsonDocument>.Filter.Eq("_id", model._id);
        //    return collenction.UpdateMany(model);

        //}
        /// <summary>
        /// 删除一个
        /// </summary>
        public static long DeleteOne<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            var  result = collenction.DeleteOneAsync(expression);
          return   result.Result.DeletedCount;
        }
        public static long DeleteMany<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            var result = collenction.DeleteManyAsync(expression);
            return result.Result.DeletedCount;
        }
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

