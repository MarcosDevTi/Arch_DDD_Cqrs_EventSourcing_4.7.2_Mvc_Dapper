using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Arch.Mvc.Helpers
{
    public static class EventSourcingHelper
    {
        public static MvcHtmlString GetHistory(this HtmlHelper html, IEnumerable<dynamic> Model)
        {
            var table = new TagBuilder("table");
            table.AddCssClass("table");

            IEnumerable<JToken> props = (JObject)(IEnumerable<object>)Model.FirstOrDefault();
            var listTh = props?.Select(_ => ((JProperty)_).Name);

            var trHead = new TagBuilder("tr");
            var trHeadStr = "";
            listTh?.Select(_ =>
            {
                var tg = new TagBuilder("th");
                tg.InnerHtml += _;
                return tg;
            })
            .ToList()
            .ForEach(_ => trHeadStr += _.ToString());

            var thTable = new TagBuilder("tr");

            var trs = new List<string>();
            foreach (JObject item in (IEnumerable<object>)Model)
            {
                var result = "";
                listTh.Select(th =>
                {
                    var tdB = new TagBuilder("td");
                    var td = item[th];
                    if (td != null)
                    {
                        tdB.InnerHtml += td;
                    }
                    return tdB;
                })
                .ToList()
                .ForEach(_ => result += _);

                var trRes = new TagBuilder("tr");
                trRes.InnerHtml += result;
                trs.Add(trRes.ToString());
            }

            var trTableStr = "";
            trs.ForEach(_ => trTableStr += _);
            table.InnerHtml = trHeadStr + trTableStr;

            return MvcHtmlString.Create(table.ToString());
        }
    }
}