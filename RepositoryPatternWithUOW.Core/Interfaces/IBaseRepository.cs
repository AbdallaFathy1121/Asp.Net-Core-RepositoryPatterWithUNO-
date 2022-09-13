using RepositoryPatternWithUOW.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id, string[] includes = null);
        IEnumerable<T> GetAll(string[] includes = null);
        T Find(Expression<Func<T, bool>> match, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null, Expression<Func<T, object>> orderBy = null, string orederByDirection = OrderBy.Ascending);
        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entity);
        T Update(T entity);
        void Delete(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
    }
}
