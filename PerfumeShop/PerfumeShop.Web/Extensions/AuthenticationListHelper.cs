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

            switch (provider.Name)
            {
                case "Google": button.MergeAttribute("class", "btn google-signin"); break;
                case "Facebook": button.MergeAttribute("class", "btn facebook-signin"); break;
                case "Twitter": button.MergeAttribute("class", "btn twitter-signin"); break;
                default: 
                    button.MergeAttribute("class", "btn btn-primary"); 
                    button.InnerHtml.Append(provider.DisplayName);
                    break;
            }

            div.InnerHtml.AppendHtml(button);
        }

        var writer = new System.IO.StringWriter();
        div.WriteTo(writer, HtmlEncoder.Default);

        return new HtmlString(writer.ToString());
    }
}
