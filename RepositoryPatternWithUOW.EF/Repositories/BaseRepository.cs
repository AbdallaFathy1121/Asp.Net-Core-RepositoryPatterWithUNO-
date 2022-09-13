using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private ApplicationDbContext context;
        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Method Get Data With All includes
        private IQueryable<T> GetDataWithIncludes(string[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
                foreach (var item in includes)
                    query = query.Include(item);

            return query;
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            IEnumerable<T> result = GetDataWithIncludes(includes).ToList();

            return result;
        }

        public T GetById(int id, string[] includes = null)
        {
            return context.Set<T>().Find(id);
        }

        public T Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            var result = GetDataWithIncludes(includes).SingleOrDefault(match);

            return result;
        }
        
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null)
        {
            var result = GetDataWithIncludes(includes).Where(match).ToList();

            return result;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes = null, Expression<Func<T, object>> orderBy = null, string orederByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = GetDataWithIncludes(includes).Where(match);

            if(orderBy is not null)
            {
                if (orederByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();

        }

        public T Add(T entity)
        {
            context.Set<T>().Add(entity);

            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entity)
        {
            context.Set<T>().AddRange(entity);

            return entity;
        }

        public T Update(T entity)
        {
            context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public int Count()
        {
            return context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return context.Set<T>().Count(criteria);
        }
    }
}
