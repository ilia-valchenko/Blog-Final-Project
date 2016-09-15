using System.Web.Mvc;
using MvcPL.Models.Tag;
using System.Collections.Generic;

namespace MvcPL.Helpers
{
    public static class TagListHelper
    {
        /// <summary>
        /// This is an extension method which represents list of hastags of post.
        /// </summary>
        /// <param name="html">Extension parameter.</param>
        /// <param name="tags">Generic collection of tags.</param>
        /// <returns>Returns unsorted list of tags.</returns>
        public static MvcHtmlString TagList(this HtmlHelper html, IEnumerable<TagViewModel> tags)
        {
            var ul = new TagBuilder("ul");
            ul.Attributes.Add("class", "tags");

            foreach(var tag in tags)
            {
                var li = new TagBuilder("li");
                var a = new TagBuilder("a");
                a.Attributes.Add("href", "/Hashtag/" + tag.Name);
                a.InnerHtml += "#" + tag.Name + "&nbsp;";
                li.InnerHtml += a;
                ul.InnerHtml += li;
            }

            return new MvcHtmlString(ul.ToString());
        }
    }
}