namespace PerfumeShop.Web.Extensions;

public static class AuthenticationListHelper
{
    public static HtmlString GetExternalAuthProviders(this IHtmlHelper html, IList<AuthenticationScheme> authShemes)
    {
        var div = new TagBuilder("div");

        foreach (var provider in authShemes)
        {
            var button = new TagBuilder("button");

            button.MergeAttribute("type", "submit");
            button.MergeAttribute("role", "button");
            button.MergeAttribute("name", "provider");
            button.MergeAttribute("value", provider.Name);
            button.MergeAttribute("title", $"Log in using your {provider.DisplayName}");

            string btnSignIn = provider.Name switch
            {
                "Google" => "btn google-signin",
                "Facebook" => "btn facebook-signin",
                _ => "btn btn-primary"
            };
            button.MergeAttribute("class", btnSignIn);

            div.InnerHtml.AppendHtml(button);
        }
        var writer = new System.IO.StringWriter();
        div.WriteTo(writer, HtmlEncoder.Default);

        return new HtmlString(writer.ToString());
    }
}
