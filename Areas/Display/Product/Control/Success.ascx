<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Success.ascx.cs" Inherits="Areas_Display_Product_Control_Success" %>
<%@ Import Namespace="Developer.Extension" %>

<div class="wrp_cart">
    <div class="bar-top">
        <a href="<%=UrlExtension.WebsiteUrl + RewriteExtension.Product + RewriteExtension.Extensions %>" class="buymore">Mua thêm sản phẩm khác</a>
    </div>
    <div class="emptyResult">
        <div class="emptyResult_box">
            <div class="success_icon">
                <svg version="1.1"  width="60" height="60" x="0" y="0" viewBox="0 0 367.805 367.805" style="enable-background:new 0 0 512 512" xml:space="preserve" class="">
                    <g>
                        <path d="M183.903.001c101.566 0 183.902 82.336 183.902 183.902s-82.336 183.902-183.902 183.902S.001 285.469.001 183.903C-.288 82.625 81.579.29 182.856.001h1.047z" style="" fill="#3bb54a" data-original="#3bb54a" class=""></path>
                        <path d="M285.78 133.225 155.168 263.837l-73.143-72.62 29.78-29.257 43.363 42.841 100.833-100.833z" style="" fill="#d4e1f4" data-original="#d4e1f4" class=""></path>
                    </g>
                </svg>
            </div>
            <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
        </div>
                    
        <a href="/" class="btnGoHome">Về trang chủ</a>
    </div>
</div>