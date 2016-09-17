using System.Web.Mvc;

namespace MvcPL.Helpers
{
    public static class LikeButtonHelper
    {
        /// <summary>
        /// This extension method creates like button with number of likes.
        /// </summary>
        /// <param name="html">Extension parameter.</param>
        /// <param name="id">ID of current post.</param>
        /// <param name="numberOfLikes">Number of likes.</param>
        /// <returns>Returns string representation of like button.</returns>
        public static MvcHtmlString LikeButton(this HtmlHelper html, int id, int numberOfLikes, bool isLiked)
        {
            var button = new TagBuilder("button");
            button.Attributes.Add("type", "submit");
            button.Attributes.Add("id", id.ToString());
            button.Attributes.Add("class", "like-button");

            if (isLiked)
                button.AddCssClass("active"); 

            var i = new TagBuilder("i");
            i.Attributes.Add("class", "fa fa-heart");

            button.InnerHtml += i;

            var span = new TagBuilder("span");
            span.Attributes.Add("id", "like" + id);
            span.InnerHtml += numberOfLikes;

            button.InnerHtml += span;

            return new MvcHtmlString(button.ToString());
        }
    }
}
