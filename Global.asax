<%@ Application Language="C#" %>
<%@ Import Namespace="RevosJsc.Database" %>
<%@ Import Namespace="RevosJsc.Extension" %>
<%@ Import Namespace="RevosJsc.TSql" %>

<script runat="server">

    private void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        SqlDatabase.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
    }

    private void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    private void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    private void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        var dt = Settings.GetData("", "*", SettingsTSql.GetBykey(SettingsExtension.KeyTotalView), "");
        if (dt.Rows.Count > 0)
        {
            Settings.UpdateValues("vsValue = vsValue + 1", SettingsTSql.GetBykey(SettingsExtension.KeyTotalView));
        }
        else
        {
            Settings.Insert(SettingsExtension.KeyTotalView, "1", "1");
        }
    }

    private void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.UpdateForUserLeave();
    }

</script>
