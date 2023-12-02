<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Areas_Display_Component_Header" %>
<%@ Import Namespace="Developer.Extension" %>
<%@ Register Src="~/Areas/Display/Adv/AdvLogo.ascx" TagPrefix="uc1" TagName="AdvLogo" %>
<%@ Register Src="~/Areas/Display/Component/MenuMain.ascx" TagPrefix="uc1" TagName="MenuMain" %>
<%@ Register Src="~/Areas/Display/Component/Promotion.ascx" TagPrefix="uc1" TagName="Promotion" %>
<uc1:Promotion runat="server" ID="Promotion" />
<header class="<%=Rewrite.Length > 0 ? "hold":"" %>">
    <div class="search-area">
        <div class="box-search">
            <div class="search-inner">
                <form id="main-search" action="/">
                    <div class="search_icon"><svg version="1.1" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" height="35" viewBox="0 0 50 50" enable-background="new 0 0 50 50" xml:space="preserve">
                        <g>
                            <path d="M33.178,32.157c1.473-2.113,2.343-4.677,2.343-7.448c0-7.204-5.84-13.045-13.045-13.045 S9.43,17.506,9.43,24.71c0,7.205,5.84,13.045,13.045,13.045c2.838,0,5.457-0.917,7.598-2.456l6.291,6.534l3.181-3.062 L33.178,32.157z M22.475,32.531c-4.319,0-7.821-3.501-7.821-7.821c0-4.32,3.502-7.821,7.821-7.821c4.319,0,7.821,3.501,7.821,7.821 C30.296,29.03,26.795,32.531,22.475,32.531z" />
                        </g></svg></div>
                    
                    <div class="prev_search"> <svg xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:svgjs="http://svgjs.com/svgjs" width="16" height="16" x="0" y="0" viewBox="0 0 492 492" style="enable-background:new 0 0 512 512" xml:space="preserve" class=""><g><path d="M198.608 246.104 382.664 62.04c5.068-5.056 7.856-11.816 7.856-19.024 0-7.212-2.788-13.968-7.856-19.032l-16.128-16.12C361.476 2.792 354.712 0 347.504 0s-13.964 2.792-19.028 7.864L109.328 227.008c-5.084 5.08-7.868 11.868-7.848 19.084-.02 7.248 2.76 14.028 7.848 19.112l218.944 218.932c5.064 5.072 11.82 7.864 19.032 7.864 7.208 0 13.964-2.792 19.032-7.864l16.124-16.12c10.492-10.492 10.492-27.572 0-38.06L198.608 246.104z" fill="#000000" data-original="#000000" class=""></path></g></svg> </div>
                    <input type="hidden" name="rewrite" value="search" />
                    <input type="hidden" name="page" value="product" />
                    <input placeholder="Từ khóa tìm kiếm ... " type="text" name="keyword" value="<%=Key %>">
                    <button type="submit">Tìm Kiếm</button>
                </form>
               
        </div>
        <div class="search-close-btn">
            <svg xmlns="http://www.w3.org/2000/svg" version="1.1" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:svgjs="http://svgjs.com/svgjs" height="16" x="0" y="0" viewBox="0 0 320.591 320.591" style="enable-background:new 0 0 512 512" xml:space="preserve" class=""><g><path d="M30.391 318.583a30.37 30.37 0 0 1-21.56-7.288c-11.774-11.844-11.774-30.973 0-42.817L266.643 10.665c12.246-11.459 31.462-10.822 42.921 1.424 10.362 11.074 10.966 28.095 1.414 39.875L51.647 311.295a30.366 30.366 0 0 1-21.256 7.288z" fill="#fff" data-original="#000000" class=""></path><path d="M287.9 318.583a30.37 30.37 0 0 1-21.257-8.806L8.83 51.963C-2.078 39.225-.595 20.055 12.143 9.146c11.369-9.736 28.136-9.736 39.504 0l259.331 257.813c12.243 11.462 12.876 30.679 1.414 42.922-.456.487-.927.958-1.414 1.414a30.368 30.368 0 0 1-23.078 7.288z" fill="#fff" data-original="#000000" class=""></path></g>

            </svg>
        </div>
        </div>
        <div class="popular-search">
            <div class="text-popular">popular searches</div>
            <div class="group-popular">
                <asp:Literal runat="server" ID="ltrPopularSearch" />
            </div>
        </div>
    </div>
    <div class="wrp">
        <uc1:AdvLogo runat="server" ID="AdvLogo" />
        <uc1:MenuMain runat="server" ID="MenuMain" />
        <div class="naviagation_icon navigation_search">
            <svg version="1.1" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" height="35" viewBox="0 0 50 50" enable-background="new 0 0 50 50" xml:space="preserve">
                <g>
                    <path d="M33.178,32.157c1.473-2.113,2.343-4.677,2.343-7.448c0-7.204-5.84-13.045-13.045-13.045 S9.43,17.506,9.43,24.71c0,7.205,5.84,13.045,13.045,13.045c2.838,0,5.457-0.917,7.598-2.456l6.291,6.534l3.181-3.062 L33.178,32.157z M22.475,32.531c-4.319,0-7.821-3.501-7.821-7.821c0-4.32,3.502-7.821,7.821-7.821c4.319,0,7.821,3.501,7.821,7.821 C30.296,29.03,26.795,32.531,22.475,32.531z" />
                </g></svg>
        </div>
        <div class="naviagation_icon navigation_cart">
            <a href="<%=UrlExtension.WebsiteUrl + RewriteExtension.Cart + RewriteExtension.Extensions %>">
            <svg height="24" version="1.1" xmlns="http://www.w3.org/2000/svg" x="0px" y="0px" viewBox="0 0 32 32">
                <path d="M25,8.9C24.9,8.4,24.5,8,24,8h-3V7c0-2.8-2.2-5-5-5s-5,2.2-5,5v1H8C7.5,8,7.1,8.4,7,8.9l-2,20c0,0.3,0.1,0.6,0.3,0.8 S5.7,30,6,30h20c0.3,0,0.6-0.1,0.7-0.3s0.3-0.5,0.3-0.8L25,8.9z M13,7c0-1.7,1.3-3,3-3s3,1.3,3,3v1h-6V7z" />
            </svg>
            </a>
            <span id="minicart-quantity">0</span>
        </div>
        <div class="naviagation_icon navigation_expanded">
            <svg height="24" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" stroke="currentColor" stroke-width="4" stroke-linecap="round" stroke-linejoin="round">
                <line x1="3" y1="12" x2="21" y2="12"></line><line x1="3" y1="6" x2="21" y2="6"></line><line x1="3" y1="18" x2="21" y2="18"></line></svg>
        </div>
        <div class="navigation_close">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16.1 16.1">
                <path d="M10.9 8.1l4.9-4.9c.4-.4.4-1 0-1.4L14.4.4c-.4-.4-1-.4-1.4 0L8.1 5.2 3.1.3c-.4-.4-1-.4-1.4 0L.3 1.7c-.4.4-.4 1 0 1.4L5.2 8 .3 13c-.4.4-.4 1 0 1.4l1.4 1.4c.4.4 1 .4 1.4 0L8 10.9l4.9 4.9c.4.4 1 .4 1.4 0l1.4-1.4c.4-.4.4-1 0-1.4l-4.8-4.9z"></path></svg>
        </div>
    </div>
</header>



