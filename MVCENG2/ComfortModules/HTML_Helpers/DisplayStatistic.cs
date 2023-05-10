using Microsoft.AspNetCore.Html;        // для HtmlString
using Microsoft.AspNetCore.Mvc.Rendering;   // для IHtmlHelper
using System.Text.Encodings.Web;

namespace MVCENG2
{
    public static class DisplayStatistic
    {
        public static HtmlString DislpayStatisticsCondition(this IHtmlHelper html, string statisticResult)
        {
            if (statisticResult == "Ok")
            {
                TagBuilder tr = new TagBuilder("tr");

                foreach (string item in items)
                {
                    TagBuilder li = new TagBuilder("li");
                    // добавляем текст в li
                    li.InnerHtml.Append(item);
                    // добавляем li в ul
                    ul.InnerHtml.AppendHtml(li);
                }
                ul.Attributes.Add("class", "itemsList");
                using var writer = new StringWriter();
                ul.WriteTo(writer, HtmlEncoder.Default);
                return new HtmlString(writer.ToString());

               











            string result = "<ul>";
            foreach (string item in items)
            {
                result = $"{result}<li>{item}</li>";
            }
            result = $"{result}</ul>";
            return new HtmlString(result);
        }
    }
}