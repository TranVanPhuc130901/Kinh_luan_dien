<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NoResults.ascx.cs" Inherits="Areas_Display_Search_Control_NoResults" %>
<%@ Register Src="~/Areas/Display/Component/Breadcrumb.ascx" TagPrefix="uc1" TagName="Breadcrumb" %>
<uc1:Breadcrumb runat="server" ID="Breadcrumb" />
<div id="Search404">
    <div class="wrp">
        <p class="fs20 pt8 pb17 lh26 dnmobile-l">Rất tiếc, Didonghan không tìm thấy kết quả nào phù hợp với từ khóa <span class="fwb c29a5ff">"<%=Keyword %>"</span></p>
        <div class="container">
            <img class="lazy" data-src="/css/icon/404.png" src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" alt="404"/>
            <noscript>
                <img src="/css/icon/404.png" alt="404"/>
            </noscript>
            <p class="pt8 pb17 lh22 dn dbMobile-l c999 pt30">Rất tiếc, Didonghan không tìm thấy kết quả nào phù hợp với từ khóa <span class="fwb c333">"<%=Keyword %>"</span></p>
            <div class="cont">
                <p class="fs18 fwb">Để tìm được kết quả chính xác hơn, bạn vui lòng:</p>
                <ul>
                    <li>Kiểm tra lỗi chính tả của từ khóa đã nhập</li>
                    <li>Thử lại bằng từ khóa khác</li>
                    <li>Thử lại bằng những từ khóa tổng quát hơn</li>
                    <li>Thử lại bằng những từ khóa ngắn gọn hơn</li>
                </ul>
            </div>
        </div>
    </div>
</div>