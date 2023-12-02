<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cart.ascx.cs" Inherits="Areas_Display_Product_Control_Cart" %>
<%@ Import Namespace="Developer.Extension" %>
<%@ Import Namespace="RevosJsc.Extension" %>
<div class="wrp_cart">
    <div class="bar-top">
        <a href="<%=UrlExtension.WebsiteUrl + RewriteExtension.Product + RewriteExtension.Extensions %>" class="buymore">Mua thêm sản phẩm khác</a>
        <div class="yourcart">Giỏ hàng của bạn</div>
    </div>
   <div class="container_cart">
       <div id="list_items">
           <asp:Literal runat="server" id="ltrList"></asp:Literal>
       </div>
       <form id="FormDH" class="formCart active" onsubmit="SubmitCart(event)">
           <div class="tt_form">Thông tin đặt hàng</div>
           <div class="note_form">Vui lòng điền đầy đủ thông tin đặt hàng <span>*</span></div>
           <div class="infoForm">
               <div class="left">
                   <input type="text" id="tbName" placeholder="Họ tên" value="<%=CookieExtension.GetCookies(SecurityExtension.BuildPassword("name")) %>" required maxlength="50"/>
                   <div class="item">
                       <input type="email" id="tbEmail" placeholder="Email" value="<%=CookieExtension.GetCookies(SecurityExtension.BuildPassword("email")) %>" maxlength="100" />
                       <input type="text" id="tbPhone" placeholder="Số điện thoại" value="<%=CookieExtension.GetCookies(SecurityExtension.BuildPassword("phone")) %>" minlength="10"  maxlength="11" required/>
                   </div>
                   <input type="text" id="tbAddress" placeholder="Địa chỉ: Số nhà, tên đường, xã, phường hoặc thị trấn" value="<%=CookieExtension.GetCookies(SecurityExtension.BuildPassword("add")) %>" maxlength="300" required />
               </div>
               <textarea id="tbMessage" placeholder="Ghi chú ..." maxlength="300"></textarea>
           </div>
           <asp:Literal runat="server" ID="ltrPaymentMethod"/>
           <div class="freeShip">Miễn phí giao hàng toàn quốc cho đơn hàng từ 1.000.000đ (tối đa 50.000đ)</div>
           <div class="btn_cart">
               <a href="/">Tiếp tục mua hàng</a>
               <button type="submit">Xác nhận</button>
           </div>
       </form>
   </div>
</div>
<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        //GetListCart();
        //GetAllLocal();
        updateQuantityCart();
    });
</script>
