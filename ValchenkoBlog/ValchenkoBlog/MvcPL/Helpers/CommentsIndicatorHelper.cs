using System.Web.Mvc;

namespace MvcPL.Helpers
{
    public static class CommentsIndicatorHelper
    {
        /// <summary>
        /// This is an extension method for creatind indicator for comments.
        /// </summary>
        /// <param name="html">Extension parameter.</param>
        /// <param name="numberOfComments">Number of comments.</param>
        /// <returns>Returns indicator for comments.</returns>
        public static MvcHtmlString CommentsIndicator(this HtmlHelper html, int numberOfComments)
        {
            var span = new TagBuilder("span");
            span.Attributes.Add("id", "comments");

            var i = new TagBuilder("i");
            i.Attributes.Add("class", "fa fa-comment");
            span.InnerHtml += i;

            var innerSpan = new TagBuilder("span");
            innerSpan.Attributes.Add("id", "number_of_comments_container");
            innerSpan.InnerHtml += numberOfComments;

            span.InnerHtml += innerSpan;

            return new MvcHtmlString(span.ToString());
        }
    }
}