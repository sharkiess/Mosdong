#pragma checksum "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40ab55d0d06c4595d17dfb6a39cb05693175bc9b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Layout_NavList), @"mvc.1.0.view", @"/Views/Layout/NavList.cshtml")]
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
#nullable restore
#line 1 "C:\Users\Admin\Mosdong\Views\_ViewImports.cshtml"
using Mosdong;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\Mosdong\Views\_ViewImports.cshtml"
using Mosdong.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\Mosdong\Views\_ViewImports.cshtml"
using System.ComponentModel.DataAnnotations;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Admin\Mosdong\Views\_ViewImports.cshtml"
using System.ComponentModel.DataAnnotations.Schema;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Admin\Mosdong\Views\_ViewImports.cshtml"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40ab55d0d06c4595d17dfb6a39cb05693175bc9b", @"/Views/Layout/NavList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5b2722ce51c13a8e00c23efbd532089ca66b24e", @"/Views/_ViewImports.cshtml")]
    public class Views_Layout_NavList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SubCategory>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
 foreach (var item in Model.OrderBy(p => p.CategoryId).ToList())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <li class=\"nav-item dropdown\">\r\n        <a class=\"nav-link dropdown-toggle\" href=\"#\"");
            BeginWriteAttribute("id", " id=\"", 216, "\"", 270, 2);
            WriteAttributeValue("", 221, "dropdown0", 221, 9, true);
#nullable restore
#line 8 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
WriteAttributeValue(" ", 230, Html.DisplayFor(m => item.Category.Id), 231, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"><i class=\"fas fa-store col-1\"></i>  ");
#nullable restore
#line 8 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
                                                                                                                                                                                                             Write(Html.DisplayFor(m => item.Category.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n        <div class=\"dropdown-menu\"");
            BeginWriteAttribute("id", " id=\"", 455, "\"", 503, 2);
            WriteAttributeValue("", 460, "nav", 460, 3, true);
#nullable restore
#line 9 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
WriteAttributeValue(" ", 463, Html.DisplayFor(m => item.Category.Id), 464, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("aria-labelledby", " aria-labelledby=\"", 504, "\"", 571, 2);
            WriteAttributeValue("", 522, "dropdown0", 522, 9, true);
#nullable restore
#line 9 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
WriteAttributeValue(" ", 531, Html.DisplayFor(m => item.Category.Id), 532, 39, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n");
#nullable restore
#line 11 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
             foreach (var sub in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a class=\"dropdown-item\" href=\"#\">");
#nullable restore
#line 13 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
                                             Write(Html.DisplayFor(m => sub.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n");
#nullable restore
#line 14 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </li>\r\n");
#nullable restore
#line 18 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
 
}
else { 

#line default
#line hidden
#nullable disable
            WriteLiteral("<p>NavBar List 오류</p>\r\n");
#nullable restore
#line 22 "C:\Users\Admin\Mosdong\Views\Layout\NavList.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SubCategory>> Html { get; private set; }
    }
}
#pragma warning restore 1591