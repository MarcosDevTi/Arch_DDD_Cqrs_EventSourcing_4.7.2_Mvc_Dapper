using Arch.Infra.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Arch.Mvc.Helpers
{
    public static class PagingHtmlHelper
    {
        public static MvcHtmlString GetSortingUrl<T>(this HtmlHelper html, PagedResult<T> pagedResult, string propertyName, string url)
        {
            var extendedUrl = url
                .SetParameter("sortColumn", propertyName)
                .SetParameter("sortDirection", GetSortDirection(pagedResult.Paging, propertyName).ToString())
                .SetParameter("pageIndex", "0");

            return MvcHtmlString.Create(extendedUrl);
        }

        public static MvcHtmlString GetPager<T>(this HtmlHelper html, PagedResult<T> pagedResult, string url)
        {
            if (pagedResult == null || pagedResult.TotalNumberOfItems <= pagedResult.Paging.PageSize)
            {
                return MvcHtmlString.Create(string.Empty);
            }

            var listBuilder = new TagBuilder("ul");
            listBuilder.AddCssClass("pagination");

            var pagingIndexes = GetPagingIndexes(
                pagedResult.Paging.PageIndex,
                (int)Math.Ceiling((double)pagedResult.TotalNumberOfItems / pagedResult.Paging.PageSize));

            for (int i = 0; i < pagingIndexes.Length; i++)
            {
                if (i > 0 && pagingIndexes[i - 1] != pagingIndexes[i] - 1)
                {
                    var extraLiBuilder = new TagBuilder("li");
                    extraLiBuilder.InnerHtml = "<span>&hellip;</span>";
                    extraLiBuilder.AddCssClass("disabled");
                    listBuilder.InnerHtml += extraLiBuilder.ToString();
                }

                var itemBuilder = new TagBuilder("li");
                itemBuilder.AddCssClass("page-item");
                if (pagedResult.Paging.PageIndex == pagingIndexes[i])
                {
                    itemBuilder.InnerHtml =
                        $"<a class=\"page-link\" href=\"#\"> {(pagingIndexes[i] + 1).ToString()} " +
                        $"<span class=\"sr-only\">(current)</span></a>";
                    itemBuilder.AddCssClass("active");
                    itemBuilder.Attributes.Add("aria-current", "page");
                }
                else
                {
                    var pagingLinkBuilder = new TagBuilder("a");

                    string extendedUrl = url
                        .SetParameter("sortColumn", pagedResult.Paging.SortColumn)
                        .SetParameter("sortDirection", pagedResult.Paging.SortDirection.ToString())
                        .SetParameter("pageIndex", pagingIndexes[i].ToString());

                    pagingLinkBuilder.MergeAttribute("href", extendedUrl);
                    pagingLinkBuilder.AddCssClass("page-link");
                    pagingLinkBuilder.SetInnerText((pagingIndexes[i] + 1).ToString());

                    itemBuilder.InnerHtml = pagingLinkBuilder.ToString();
                }

                listBuilder.InnerHtml += itemBuilder.ToString();
            }

            return MvcHtmlString.Create(listBuilder.ToString());
        }

        private static string SetParameter(this string url, string param, string value)
        {
            var questionMarkIndex = url.IndexOf('?');
            NameValueCollection parameters;
            var result = new StringBuilder();

            if (questionMarkIndex == -1)
            {
                parameters = new NameValueCollection();
                result.Append(url);
            }
            else
            {
                parameters = HttpUtility.ParseQueryString(url.Substring(questionMarkIndex));
                result.Append(url.Substring(0, questionMarkIndex));
            }

            if (string.IsNullOrEmpty(value))
                parameters.Remove(param);
            parameters[param] = value;

            if (parameters.Count > 0)
            {
                result.Append('?');

                foreach (string parameterName in parameters)
                    result.AppendFormat("{0}={1}&", parameterName, parameters[parameterName]);

                result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }

        private static SortDirection GetSortDirection(Paging paging, string propertyName)
        {
            var sortDirection = SortDirection.Asc;

            if (paging != null
                && propertyName.Equals(paging.SortColumn)
                && paging.SortDirection == SortDirection.Asc)
            {
                sortDirection = SortDirection.Desc;
            }

            return sortDirection;
        }

        private static int[] GetPagingIndexes(int currentIndex, int totalPages)
        {
            var result = new HashSet<int>();

            for (int i = 0; i < 2; i++)
            {
                if (i <= totalPages)
                    result.Add(i);
            }

            var current = currentIndex - 3;

            while (current <= currentIndex + 3)
            {
                if (current > 0 && current < totalPages)
                {
                    result.Add(current);
                }

                current++;
            }

            for (int i = totalPages - 2; i < totalPages; i++)
                result.Add(i);

            return result.ToArray();
        }
    }
}