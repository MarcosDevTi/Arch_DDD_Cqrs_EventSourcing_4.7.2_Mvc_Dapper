using System;
using System.Collections.Generic;

namespace Arch.Infra.Shared.Pagination
{
    public class PagedResult<T> : IEnumerable<T>
    {
        public PagedResult()
        {
        }

        public PagedResult(IEnumerable<T> items, int totalNumberOfItems, Paging paging)
        {
            this.Items = items;
            this.TotalNumberOfItems = totalNumberOfItems;
            this.Paging = paging;
        }

        public IEnumerable<T> Items { get; private set; }

        public int TotalNumberOfItems { get; private set; }

        public int TotalNumberOfPages
        {
            get
            {
                if (this.Paging == null || this.Paging.PageSize <= 0)
                {
                    return 0;
                }

                return (int)Math.Ceiling((double)this.TotalNumberOfItems / this.Paging.PageSize);
            }
        }

        public Paging Paging { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }
    }
}
