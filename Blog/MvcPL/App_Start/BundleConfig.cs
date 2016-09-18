using System.Web.Optimization;

namespace MvcPL
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/new_elements.css",
                      "~/Content/style.css",
                      "~/Content/home.css",
                      "~/Content/popup.css"));

            bundles.Add(new StyleBundle("~/Content/searchByTag").Include(
                        "~/Content/blog.css",
                        "~/Content/search_by_tag.css"));

            bundles.Add(new StyleBundle("~/Content/createPost").Include(
                        "~/Content/create_post.css",
                        "~/Content/select.css"));

            bundles.Add(new StyleBundle("~/Content/userProfile").Include(
                        "~/Content/user_profile.css"));

            bundles.Add(new StyleBundle("~/Content/error").Include(
                        "~/Content/error.css"));

            bundles.Add(new StyleBundle("~/Content/blog").Include(
                        "~/Content/blog.css"));

            bundles.Add(new StyleBundle("~/Content/editPost").Include(
                        "~/Content/create_post.css",
                        "~/Content/select.css"));

            bundles.Add(new StyleBundle("~/Content/deletePost").Include(
                        "~/Content/delete_post.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                        "~/Content/registration.css",
                        "~/Content/login.css"));

            bundles.Add(new StyleBundle("~/Content/registration").Include(
                        "~/Content/registration.css"));

            // Scripts

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/like").Include(
                        "~/scripts/jquery.unobtrusive-ajax.js",
                        "~/scripts/Custom/like.js"));

            bundles.Add(new ScriptBundle("~/bundles/comment").Include(
                        "~/scripts/Custom/base64js.min.js",
                        "~/scripts/Custom/comment.js"));

            bundles.Add(new ScriptBundle("~/bundles/popup").Include(
                        "~/scripts/Custom/popup_login.js"));

            bundles.Add(new ScriptBundle("~/bundles/registrationValidation").Include(
                        "~/scripts/CustomValidation/registration_validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/loginValidation").Include(
                        "~/scripts/CustomValidation/login_validation.js"));
        }
    }
}
