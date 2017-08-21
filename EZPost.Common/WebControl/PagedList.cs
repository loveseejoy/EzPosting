using System;
using System.Collections.Generic;
using System.Linq;

namespace EZPost.Common.WebControl
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="beforeFilterTotalCount">before filter totalCount</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize, int beforeFilterTotalCount = 0)
        {
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.BeforeFilterTotalCount = beforeFilterTotalCount;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }


        public PagedList(int totalCount, IList<T> source)
        {
            TotalCount = totalCount;
            //TotalPages = TotalCount / pageParms.Length;

            //if (TotalCount % pageParms.Length > 0)
            //    TotalPages++;

            //this.PageSize = pageParms.Length;
            //this.PageIndex = pageParms.PageIndex;
            this.AddRange(source);
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">Total count</param>
        /// <param name="beforeFilterTotalCount"></param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount, int beforeFilterTotalCount = 0)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.BeforeFilterTotalCount = beforeFilterTotalCount;
            this.AddRange(source);
        }

        public PagedList(IList<T> source, int pageIndex, int pgaeSize, int totalCount, int totalPages, int beforeFilterCount)
        {
            PageIndex = pageIndex;
            PageSize = pgaeSize;
            BeforeFilterTotalCount = beforeFilterCount;
            TotalCount = totalCount;
            TotalPages = totalPages;

            this.AddRange(source);
        }


        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int BeforeFilterTotalCount { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}