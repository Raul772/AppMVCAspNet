using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RL.ModelApp.Extensions
{
    public class EmailTagHelper : TagHelper
    {

        public string domain { get; set; } = "@gmail.com";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var content = await output.GetChildContentAsync();
            var target = content.GetContent() + "@" + domain;
            output.Attributes.SetAttribute("href", "mailto:" + target);
            output.Attributes.SetAttribute("target", "_blank");

            output.Content.SetContent(target);
        }


    }
}
