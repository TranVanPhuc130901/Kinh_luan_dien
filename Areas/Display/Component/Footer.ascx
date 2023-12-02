<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Areas_Display_Component_Footer" %>
<%@ Register Src="~/Areas/Display/Adv/AdvFooter.ascx" TagPrefix="uc1" TagName="AdvFooter" %>
<%@ Register Src="~/Areas/Display/Component/MenuOther.ascx" TagPrefix="uc1" TagName="MenuOther" %>
<%@ Register Src="~/Areas/Display/Component/MenuFooter.ascx" TagPrefix="uc1" TagName="MenuFooter" %>
<footer>
    <uc1:AdvFooter runat="server" ID="AdvFooter" />
    <div class="columns">
        <div class="wrp">
            <div class="subcribe_column">
                <div class="head">Nhận thông báo</div>
                <form>
                    <p>Đăng ký nhận ưu đãi độc quyền, hoạt động, sự kiện và hơn thế nữa.</p>
                    <p>&nbsp;</p>
                    <input type="email" placeholder="Email của bạn" required />
                    <button type="submit" class="btn">ĐĂNG KÝ NGAY</button>
                </form>
            </div>
            <uc1:MenuOther runat="server" ID="MenuOther" />
            <uc1:MenuFooter runat="server" ID="MenuFooter" />
            
        </div>
    </div>
    <div class="copyright">
        <div class="wrp">
            <%=Copyright %>
            <span>VN</span>
        </div>
    </div>
</footer>