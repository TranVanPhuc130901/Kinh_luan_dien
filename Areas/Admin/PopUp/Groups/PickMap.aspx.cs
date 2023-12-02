using System;
using System.Web.UI;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.DestinationControl;
using RevosJsc.TSql;

public partial class Areas_Admin_PopUp_Groups_PickMap : System.Web.UI.Page
{
    private readonly string _pic = FolderPic.Destination;
    private string _igid = "";
    private string _parent = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["igid"] != null) _igid = QueryStringExtension.GetQueryString("igid");
        if (Request.QueryString["parent"] != null) _parent = QueryStringExtension.GetQueryString("parent");
        if (IsPostBack) return;
        GetImageMap();
        GetData();
    }

    private void GetData()
    {
        var dt = Groups.GetData("1", "*", GroupsTSql.GetById(_igid), "");
        if (dt.Rows.Count <= 0) return;
        ltrHead.Text = dt.Rows[0][GroupsColumns.VgName].ToString();
        txtTitle.Text = dt.Rows[0][GroupsColumns.VgParam].ToString();
    }

    private void GetImageMap()
    {
        var dt = Groups.GetData("1", "*", GroupsTSql.GetById(_parent), "");
        if (dt.Rows.Count <= 0) return;
        var image = dt.Rows[0][GroupsColumns.VgImage].ToString();
        if (image.Length > 0)
        {
            btSubmit.Visible = true;
            ltrImg.Text += "<div class='text-primary'>Hướng dẫn: Chấm các điểm trên bản đồ để lấy tọa độ</div>";
            ltrImg.Text += "<div class='p_relative'>" + ImagesExtension.GetImage(_pic, image, "", "", false, false, "", false) + "</div>";
        }
        else ltrImg.Text = "<div class='text-danger'>Bạn chưa thêm bản đồ cho danh mục gốc<br/>Vui lòng thêm bản đồ và thử lại</div>";
    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        Groups.UpdateValues("vgParam = '" + txtTitle.Text.Trim(',') + "'", GroupsTSql.GetById(_igid));
        ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Cập nhật thành công', {type: 'success'});});", true);
    }
}