<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Areas_Display_News_Control_Detail" %>
<%@ Import Namespace="Developer.Extension" %>
<%@ Import Namespace="RevosJsc.Extension" %>
<%@ Register Src="~/Areas/Display/News/SubControl/SubNewsOther.ascx" TagPrefix="uc1" TagName="SubNewsOther" %>
<%--<asp:Literal ID="ltrBanner" runat="server"/>--%>
<section class="gothiar_story">
    <div class="wrp">
        <div class="gothiar_story_top">
            <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>
        </div>
        <div class="contentView">
            <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
        </div>
    </div>
</section>
<uc1:SubNewsOther runat="server" ID="SubNewsOther" />
<script type="application/ld+json">
    {
      "@context": "https://schema.org",
      "@type": "NewsArticle",
      "mainEntityOfPage": {
        "@type": "WebPage",
        "@id": "<%=LinkShare %>"
      },
      "headline": "Article headline",
      "image": ["<%=Image %>"],
      "datePublished": "<%=Published %>",
      "dateModified": "<%=Modified %>",
      "author": {
        "@type": "Person",
        "name": "<%=Author.Length > 0 ? Author: BrandName %>"
      },
       "publisher": {
        "@type": "Organization",
        "name": "Gothiar",
        "logo": {
          "@type": "ImageObject",
          "url": "<%=UrlExtension.WebsiteUrl %>/pic/system/<%=SettingsExtension.GetSettingKey(SettingsExtension.KeyImageShareHomepage, RevosJsc.LanguageControl.Cookie.GetLanguageValueDisplay()) %>"
        }
      },
      "description": "<%=Description %>"
    }
</script>

