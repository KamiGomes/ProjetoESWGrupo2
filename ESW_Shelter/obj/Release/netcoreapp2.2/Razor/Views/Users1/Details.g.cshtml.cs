#pragma checksum "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8039438bdc69e8e0871288c89aeb93aebc0bb4d6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users1_Details), @"mvc.1.0.view", @"/Views/Users1/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Users1/Details.cshtml", typeof(AspNetCore.Views_Users1_Details))]
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
#line 1 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\_ViewImports.cshtml"
using ESW_Shelter;

#line default
#line hidden
#line 2 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\_ViewImports.cshtml"
using ESW_Shelter.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8039438bdc69e8e0871288c89aeb93aebc0bb4d6", @"/Views/Users1/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9c96b5989d543400a707958686f16ea21aca77f2", @"/Views/_ViewImports.cshtml")]
    public class Views_Users1_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ESW_Shelter.Models.Users>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/_BackLayout.cshtml";

#line default
#line hidden
            BeginContext(129, 119, true);
            WriteLiteral("\r\n<h2>Details</h2>\r\n\r\n<div>\r\n    <h4>Users</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(249, 41, false);
#line 15 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
            EndContext();
            BeginContext(290, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(334, 37, false);
#line 18 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
            EndContext();
            BeginContext(371, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(415, 40, false);
#line 21 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(455, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(499, 36, false);
#line 24 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(535, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(579, 44, false);
#line 27 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Password));

#line default
#line hidden
            EndContext();
            BeginContext(623, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(667, 40, false);
#line 30 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.Password));

#line default
#line hidden
            EndContext();
            BeginContext(707, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(751, 50, false);
#line 33 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ConfirmedEmail));

#line default
#line hidden
            EndContext();
            BeginContext(801, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(845, 46, false);
#line 36 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.ConfirmedEmail));

#line default
#line hidden
            EndContext();
            BeginContext(891, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(935, 42, false);
#line 39 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Street));

#line default
#line hidden
            EndContext();
            BeginContext(977, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1021, 38, false);
#line 42 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.Street));

#line default
#line hidden
            EndContext();
            BeginContext(1059, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1103, 46, false);
#line 45 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.PostalCode));

#line default
#line hidden
            EndContext();
            BeginContext(1149, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1193, 42, false);
#line 48 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.PostalCode));

#line default
#line hidden
            EndContext();
            BeginContext(1235, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1279, 40, false);
#line 51 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.City));

#line default
#line hidden
            EndContext();
            BeginContext(1319, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1363, 36, false);
#line 54 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.City));

#line default
#line hidden
            EndContext();
            BeginContext(1399, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1443, 41, false);
#line 57 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Phone));

#line default
#line hidden
            EndContext();
            BeginContext(1484, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1528, 37, false);
#line 60 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.Phone));

#line default
#line hidden
            EndContext();
            BeginContext(1565, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1609, 47, false);
#line 63 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DateOfBirth));

#line default
#line hidden
            EndContext();
            BeginContext(1656, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1700, 43, false);
#line 66 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.DateOfBirth));

#line default
#line hidden
            EndContext();
            BeginContext(1743, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1787, 42, false);
#line 69 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.RoleID));

#line default
#line hidden
            EndContext();
            BeginContext(1829, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1873, 38, false);
#line 72 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
       Write(Html.DisplayFor(model => model.RoleID));

#line default
#line hidden
            EndContext();
            BeginContext(1911, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(1958, 58, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8039438bdc69e8e0871288c89aeb93aebc0bb4d612631", async() => {
                BeginContext(2008, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 77 "C:\Users\Kami\Source\Repos\KamiGomes\ProjetoESWGrupo2\ESW_Shelter\Views\Users1\Details.cshtml"
                           WriteLiteral(Model.UserID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2016, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(2024, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8039438bdc69e8e0871288c89aeb93aebc0bb4d614971", async() => {
                BeginContext(2046, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2062, 10, true);
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
