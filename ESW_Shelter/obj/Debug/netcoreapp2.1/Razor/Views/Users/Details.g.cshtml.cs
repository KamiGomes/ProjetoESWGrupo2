#pragma checksum "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e0fb1703e400947b7f70931c78aae83dbce34791"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_Details), @"mvc.1.0.view", @"/Views/Users/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Users/Details.cshtml", typeof(AspNetCore.Views_Users_Details))]
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
#line 1 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\_ViewImports.cshtml"
using ESW_Shelter;

#line default
#line hidden
#line 2 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\_ViewImports.cshtml"
using ESW_Shelter.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0fb1703e400947b7f70931c78aae83dbce34791", @"/Views/Users/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9c96b5989d543400a707958686f16ea21aca77f2", @"/Views/_ViewImports.cshtml")]
    public class Views_Users_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ESW_Shelter.Models.Users>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(33, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/_BackLayout.cshtml";

#line default
#line hidden
            BeginContext(129, 6, true);
            WriteLiteral("\r\n<h2>");
            EndContext();
            BeginContext(136, 10, false);
#line 8 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
Write(Model.Name);

#line default
#line hidden
            EndContext();
            BeginContext(146, 86, true);
            WriteLiteral("</h2>\r\n\r\n<div>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(233, 41, false);
#line 14 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
            EndContext();
            BeginContext(274, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(318, 37, false);
#line 17 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
            EndContext();
            BeginContext(355, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(399, 40, false);
#line 20 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(439, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(483, 36, false);
#line 23 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(519, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(563, 44, false);
#line 26 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Password));

#line default
#line hidden
            EndContext();
            BeginContext(607, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(651, 40, false);
#line 29 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.Password));

#line default
#line hidden
            EndContext();
            BeginContext(691, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(735, 42, false);
#line 32 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Street));

#line default
#line hidden
            EndContext();
            BeginContext(777, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(821, 38, false);
#line 35 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.Street));

#line default
#line hidden
            EndContext();
            BeginContext(859, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(903, 46, false);
#line 38 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.PostalCode));

#line default
#line hidden
            EndContext();
            BeginContext(949, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(993, 42, false);
#line 41 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.PostalCode));

#line default
#line hidden
            EndContext();
            BeginContext(1035, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1079, 40, false);
#line 44 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.City));

#line default
#line hidden
            EndContext();
            BeginContext(1119, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1163, 36, false);
#line 47 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.City));

#line default
#line hidden
            EndContext();
            BeginContext(1199, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1243, 41, false);
#line 50 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Phone));

#line default
#line hidden
            EndContext();
            BeginContext(1284, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1328, 37, false);
#line 53 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.Phone));

#line default
#line hidden
            EndContext();
            BeginContext(1365, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1409, 47, false);
#line 56 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DateOfBirth));

#line default
#line hidden
            EndContext();
            BeginContext(1456, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1500, 43, false);
#line 59 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.DateOfBirth));

#line default
#line hidden
            EndContext();
            BeginContext(1543, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1587, 44, false);
#line 62 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.RoleName));

#line default
#line hidden
            EndContext();
            BeginContext(1631, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1675, 40, false);
#line 65 "C:\Users\David Afonso\Desktop\ESW\ProjetoESWGrupo2\ESW_Shelter\Views\Users\Details.cshtml"
       Write(Html.DisplayFor(model => model.RoleName));

#line default
#line hidden
            EndContext();
            BeginContext(1715, 49, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n\r\n    ");
            EndContext();
            BeginContext(1764, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c5de8be42baa47b5b08f9cda5a392f5a", async() => {
                BeginContext(1786, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1802, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ESW_Shelter.Models.Users> Html { get; private set; }
    }
}
#pragma warning restore 1591
