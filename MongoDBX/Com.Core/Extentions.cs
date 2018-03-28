
using Common;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBX.Com.Core
{
    public class Extentions<T> : MongoContext, IExtentions<T> where  T:BaseModel
    {
        public Extentions(string DocumentName)
        {
            Document = base.GetCollection<T>(DocumentName);
        }

        public Extentions()
        {
            Document = base.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public IMongoCollection<T> Document { get; set; }
        public void Add(T model){
            Document.Add(model);
        }
        public void Add(IEnumerable<T> list) {
            Document.AddList(list);
        }
        public Task AddAsync(T model) {
          return  Document.AddAsync(model);
        }
        public Task AddAsync(IEnumerable<T> list)
        {
            return Document.AddListAsync(list);
        }

        public T GetModel(Expression<Func<T, bool>> expression) {
            return Document.FirstOrDefault(expression);
        }
        public IEnumerable< T> GetList(Expression<Func<T, bool>> expression)
        {
            return Document.FindToList(expression);
        }
        public IEnumerable<T> GetPageList(Expression<Func<T, bool>> expression, Expression<Func<T, object>> SortExpression, int? pagesize, int? CurrentPage, ref long count) {
            return Document.GetPageList(expression, SortExpression, pagesize, CurrentPage, ref count);
        }

        public T Update(Expression<Func<T, bool>> expression,T model) {
            return Document.FindOneAndReplace(expression, model);
        }
       
        public long Delete(Expression<Func<T, bool>> expression) {
            var result = Document.DeleteMany(expression);
            return result.DeletedCount;
        }
    }
}
