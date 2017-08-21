using System;
using System.Linq;
using System.Linq.Expressions;
using  System.Linq.Dynamic;
using EZPost.Common.WebControl;

namespace EZPost.Common
{
    public  static class QueryableExtensions
    {
        /// <summary>
        /// Used for paging. Can be used as an alternative to Skip(...).Take(...) chaining.
        /// </summary>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            return Queryable.Take<T>(Queryable.Skip<T>(query, skipCount), maxResultCount);
        }

        /// <summary>
        /// Used for paging with an <see cref="T:Abp.Application.Services.Dto.IPagedResultRequest" /> object.
        /// </summary>
        /// <param name="query">Queryable to apply paging</param>
        /// <param name="pagedResultRequest">An object implements <see cref="T:Abp.Application.Services.Dto.IPagedResultRequest" /> interface</param>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, DataTablesParameters pagedResultRequest)
        {
            return query.PageBy<T>(pagedResultRequest.Start, pagedResultRequest.Length);
        }

        /// <summary>
        /// Filters a <see cref="T:System.Linq.IQueryable`1" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <see cref="!:condition" /></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (!condition)
                return query;
            return query.Where<T>(predicate);
        }

        /// <summary>
        /// Filters a <see cref="T:System.Linq.IQueryable`1" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="query">Queryable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the query</param>
        /// <returns>Filtered or not filtered query based on <see cref="!:condition" /></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            if (!condition)
                return query;
            return query.Where<T>(predicate);
        }

        public static IQueryable<T> OrderBys<T>(this IQueryable<T> query, DataTablesParameters page)
        {
            if (page.OrderBy == string.Empty)
            {
                query = query.OrderBy(page.OrderPerfix+"Id desc");
            }
            else
            {
                query = query.OrderBy(page.OrderPerfix+ page.OrderBy +" "+ page.OrderDir);
            }
            return query;
        }
    }
}
