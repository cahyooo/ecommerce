#pragma checksum "C:\Users\asus\source\repos\umkm_webapp\umkm_webapp\Areas\Admin\Views\Shared\_sideBar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5bea10cb8c698cc0133ac78ed5919a20123238b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Shared__sideBar), @"mvc.1.0.view", @"/Areas/Admin/Views/Shared/_sideBar.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5bea10cb8c698cc0133ac78ed5919a20123238b3", @"/Areas/Admin/Views/Shared/_sideBar.cshtml")]
    public class Areas_Admin_Views_Shared__sideBar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@" <!-- Sidebar -->
<ul class=""navbar-nav bg-gradient-primary sidebar sidebar-dark accordion"" id=""accordionSidebar"">

    <!-- Sidebar - Brand -->
    <a class=""sidebar-brand d-flex align-items-center justify-content-center"" href=""index.html"">
        <div class=""sidebar-brand-icon rotate-n-15"">
            <i class=""fas fa-laugh-wink""></i>
        </div>
        <div class=""sidebar-brand-text mx-3"">SB Admin <sup>2</sup></div>
    </a>

    <!-- Divider -->
    <hr class=""sidebar-divider my-0"">

    <!-- Nav Item - Dashboard -->
    <li class=""nav-item"">
        <a class=""nav-link"" asp-controller=""Home"" asp-action=""Index"">
            <i class=""fas fa-fw fa-tachometer-alt""></i>
            <span>Home</span>

        </a>
    </li>

    <!-- Divider -->
    <hr class=""sidebar-divider"">

    <!-- Heading -->
    <div class=""sidebar-heading"">
        Addons
    </div>

    <!-- Nav Item - Pages Collapse Menu -->
    <!-- Nav Item - Charts -->
    <li class=""nav-item"">
        <a c");
            WriteLiteral(@"lass=""nav-link"" asp-controller=""Province"" asp-action=""Index"">
            <i class=""fas fa-fw fa-chart-area""></i>
            <span>Province</span>
        </a>
    </li>

    <!-- Nav Item - Tables -->
    <li class=""nav-item"">
        <a class=""nav-link"" asp-controller=""Region"" asp-action=""Index"">
            <i class=""fas fa-fw fa-table""></i>
            <span>Region</span>
        </a>
    </li>

    <!-- Nav Item - Tables -->
    <li class=""nav-item"">
        <a class=""nav-link"" asp-controller=""Home"" asp-action=""pokedex"">
            <i class=""fas fa-fw fa-table""></i>
            <span>Pokedex</span>
        </a>
    </li>

    <!-- Divider -->
    <hr class=""sidebar-divider d-none d-md-block"">

    <!-- Sidebar Toggler (Sidebar) -->
    <div class=""text-center d-none d-md-inline"">
        <button class=""rounded-circle border-0"" id=""sidebarToggle""></button>
    </div>

    <!-- Sidebar Message -->


</ul>
<!-- End of Sidebar -->");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
