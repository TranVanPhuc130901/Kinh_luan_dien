<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Error404.ascx.cs" Inherits="Areas_Display_Error_Control_Error404" %>
<%--<div id="Search404">
    <div class="wrp">
        <div class="container">
            <img class="lazy" data-src="/css/icon/404.png" alt="404" src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="/>
            <noscript>
                <img src="/css/icon/404.png" alt="404"/>
            </noscript>
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
</div>--%>
<style>
    #Error404{ text-align: center;padding: 200px 0;}
    .error-image {position: relative;}
    .error-image img{display: inline-block}
    /*.error-image:after {content: "";position: absolute;width: 60px;height: 1px;background: #1e73be;bottom: 0;left: 50%;margin-left: -30px;}*/
    .backtohome {line-height: 40px;display: table;margin: auto;border-radius: 4px;background: #1e73be;color: #fff;padding: 5px 20px;text-decoration:none}
    .error-label{margin:30px 0}
    @media(max-width:991px){
        #Error404{padding:100px 0}
    }
</style>
<div id="Error404">
    <div class="error-image pt30">
        <img src="../../../../pic/icon/404.png" alt="404" />
        <noscript>
            <img src="../../../../pic/icon/404.png" alt="404"/>
        </noscript>
    </div>
    <div class="error-label">OOPS! Trang bạn tìm kiếm không tồn tại!</div>
    <div class="pt30 ">
        <a href="/" class="backtohome">Trở về trang chủ</a>
    </div>
</div>