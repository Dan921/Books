#pragma checksum "C:\Users\Kupry\source\repos\BooksWebApplication\BooksWebAPI\Views\Books\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "572a8edb2a832fbed4754532c2f1b5575c189595"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Books_Create), @"mvc.1.0.view", @"/Views/Books/Create.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"572a8edb2a832fbed4754532c2f1b5575c189595", @"/Views/Books/Create.cshtml")]
    public class Views_Books_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Data.Entities.Book>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Kupry\source\repos\BooksWebApplication\BooksWebAPI\Views\Books\Create.cshtml"
  
    ViewData["Title"] = "Create";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Create</h1>

<h4>Book</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Create"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <div class=""form-group"">
                <label asp-for=""Name"" class=""control-label""></label>
                <input asp-for=""Name"" class=""form-control"" />
                <span asp-validation-for=""Name"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""ShortDescription"" class=""control-label""></label>
                <input asp-for=""ShortDescription"" class=""form-control"" />
                <span asp-validation-for=""ShortDescription"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""CoverImage"" class=""control-label""></label>
                <input asp-for=""CoverImage"" class=""form-control"" />
                <span asp-validation-for=""CoverImage"" class=""t");
            WriteLiteral(@"ext-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""PublishingYear"" class=""control-label""></label>
                <input asp-for=""PublishingYear"" class=""form-control"" />
                <span asp-validation-for=""PublishingYear"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""Author"" class=""control-label""></label>
                <input asp-for=""Author"" class=""form-control"" />
                <span asp-validation-for=""Author"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Create"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Back to List</a>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 52 "C:\Users\Kupry\source\repos\BooksWebApplication\BooksWebAPI\Views\Books\Create.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Data.Entities.Book> Html { get; private set; }
    }
}
#pragma warning restore 1591
