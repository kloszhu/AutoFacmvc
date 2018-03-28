using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Com.Models;
using MongoDB.Driver;
using Common;
namespace MongoDBX.Com.Core
{
    public interface IExtentions<T> where T : BaseModel
    {
        IMongoCollection<T> Document { get; set; }

        void Add(IEnumerable<T> list);
        void Add(T model);
        Task AddAsync(IEnumerable<T> list);
        Task AddAsync(T model);
        long Delete(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression);
        T GetModel(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetPageList(Expression<Func<T, bool>> expression, Expression<Func<T, object>> SortExpression, int? pagesize, int? CurrentPage, ref long count);
        T Update(Expression<Func<T, bool>> expression, T model);
    }
}