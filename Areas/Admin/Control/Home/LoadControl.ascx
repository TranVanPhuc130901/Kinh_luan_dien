<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadControl.ascx.cs" Inherits="Areas_Admin_Control_Home_LoadControl" %>
<%@ Import Namespace="RevosJsc.Extension" %>
<%@ Register Src="~/Areas/Admin/Control/Component/BillsNewest.ascx" TagPrefix="uc1" TagName="BillsNewest" %>
<%@ Register Src="~/Areas/Admin/Control/Component/ContactNewest.ascx" TagPrefix="uc1" TagName="ContactNewest" %>


<div id="page-content">
    <div class="row">
        <div class="col-sm-6 col-lg-3">
            <!-- Widget -->
            <a href="javascript:void(0);" class="widget widget-hover-effect1">
                <div class="widget-simple">
                    <div class="widget-icon pull-left themed-background-autumn animation-fadeIn">
                        <i class="fa fa-shopping-cart"></i>
                    </div>
                    <h3 class="widget-content text-right animation-pullDown"><%=CountBills() %>
                        <small>Đơn hàng</small>
                    </h3>
                </div>
            </a>
            <!-- END Widget -->
        </div>
        <div class="col-sm-6 col-lg-3">
            <!-- Widget -->
            <a href="javascript:void(0);" class="widget widget-hover-effect1">
                <div class="widget-simple">
                    <div class="widget-icon pull-left themed-background-spring animation-fadeIn">
                        <i class="fa fa-newspaper-o"></i>
                    </div>
                    <h3 class="widget-content text-right animation-pullDown"><%=CountNews() %>
                        <small>Bài viết</small>
                    </h3>
                </div>
            </a>
            <!-- END Widget -->
        </div>
        <div class="col-sm-6 col-lg-3">
            <!-- Widget -->
            <a href="javascript:void(0);" class="widget widget-hover-effect1">
                <div class="widget-simple">
                    <div class="widget-icon pull-left themed-background-amethyst animation-fadeIn">
                        <i class="fa fa-user"></i>
                    </div>
                    <h3 class="widget-content text-right animation-pullDown"><%=NumberExtension.FormatNumber(SettingsExtension.GetSettingKey(SettingsExtension.KeyTotalView, "1")) %>
                        <small>Lượt truy cập</small>
                    </h3>
                </div>
            </a>
            <!-- END Widget -->
        </div>
        <div class="col-sm-6 col-lg-3">
            <!-- Widget -->
            <a href="javascript:void(0);" class="widget widget-hover-effect1">
                <div class="widget-simple">
                    <div class="widget-icon pull-left themed-background-fire animation-fadeIn">
                        <i class="fa fa-users"></i>
                    </div>
                    <h3 class="widget-content text-right animation-pullDown"><%=OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.UsersCount %>
                        <small>Đang online</small>
                    </h3>
                </div>
            </a>
            <!-- END Widget -->
        </div>
    </div>
    <uc1:BillsNewest runat="server" ID="BillsNewest" />
    <uc1:ContactNewest runat="server" ID="ContactNewest" />
    <!-- FullCalendar Content -->
    <div class="block block-alt-noborder full d-none">
        <div class="row">
            <div class="col-md-2">
                <div class="block-section">
                    <!-- Add event functionality (initialized in js/pages/compCalendar.js) -->
                    <form>
                        <div class="input-group">
                            <input type="text" id="add-event" name="add-event" class="form-control" placeholder="Add Event">
                            <div class="input-group-btn">
                                <button type="submit" id="add-event-btn" class="btn btn-primary"><i class="gi gi-plus"></i></button>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="block-section">
                    <div class="block-section text-center text-muted">
                        <small><em><i class="fa fa-arrows"></i>Drag and Drop Events on the Calendar</em></small>
                    </div>
                    <ul class="calendar-events">
                        <li style="background-color: #1abc9c">Work</li>
                        <li style="background-color: #9b59b6">Vacation</li>
                        <li style="background-color: #3498db">Cinema</li>
                        <li style="background-color: #e74c3c">Gym</li>
                        <li style="background-color: #f39c12">Shopping</li>
                    </ul>
                </div>
            </div>
            <div class="col-md-10">
                <!-- FullCalendar (initialized in js/pages/compCalendar.js), for more info and examples you can check out http://arshaw.com/fullcalendar/ -->
                <div id="calendar"></div>
            </div>
        </div>
    </div>
    <!-- END FullCalendar Block -->
    <!-- END FullCalendar Content -->
</div>

<script src="/Areas/Admin/js/pages/compCalendar.js" defer></script>
<script defer>
    document.addEventListener("DOMContentLoaded", function (event) {
        CompCalendar.init();
    });
</script>
