namespace LearnLanguageSystem.Web.TagHelpers
{
    using System.Text.Encodings.Web;

    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("a")]
    public class AnchorDisableTagHelper : TagHelper
    {
        public bool IsDisabled { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.IsDisabled)
            {
                output.AddClass("disabled", HtmlEncoder.Default);
            }

            base.Process(context, output);
        }
    }
}
