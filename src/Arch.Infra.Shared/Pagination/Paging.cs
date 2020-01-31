namespace Arch.Infra.Shared.Pagination
{
    public class Paging
    {
        public Paging()
            : this(0, 10)
        {
        }

        public Paging(int pageIndex, int pageSize)
        {
            this.SortDirection = SortDirection.Asc;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public Paging(int pageIndex, int pageSize, string sortColumn)
            : this(pageIndex, pageSize)
        {
            this.SortColumn = sortColumn;
        }

        public Paging(int pageIndex, int pageSize, string sortColumn, SortDirection sortDirection)
            : this(pageIndex, pageSize, sortColumn)
        {
            this.SortDirection = sortDirection;
        }

        public SortDirection SortDirection { get; set; }

        public string SortColumn { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public override string ToString()
        {
            return "SortColumn: " + this.SortColumn + ", PageIndex: " + this.PageIndex + ", PageSize: " + this.PageSize;
        }
    }
}
