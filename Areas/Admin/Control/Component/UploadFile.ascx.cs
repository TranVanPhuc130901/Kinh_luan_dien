using System;
using System.IO;
using Developer.Extension;
using RevosJsc.Extension;

public partial class Areas_Admin_Control_Component_UploadFile : System.Web.UI.UserControl
{
    /*
    Hướng dẫn sử dụng
    ============Tại PageLoad gọi===========================
    #region Gán app, pic cho user control upload tệp đại diện
    UploadFile.Pic = pic;
    #endregion

    ============Gọi hàm để hiện thị tệp khi update danh mục, sản phẩm=============
    UploadFile.Load(dt.Rows[0][GroupsColumns.VgimageColumn].ToString());

    ============Gọi hàm lúc click nút insert, update để lưu và lấy ra tên tệp được lưu.
    Insert: string image = UploadFile.Save(false);
    Update: string image = UploadFile.Save(true);

    ============Gọi hàm reset sau khi insert xong
    UploadFile.Reset();
    */

    // Các thuộc tính được truyền vào từ ngoài
    #region Tên hiển thị control
    /// <summary>
    /// Tên hiển thị
    /// </summary>
    public string Text { set { ltrText.Text = value; } }
    #endregion

    #region Pic
    private string _pic = "";
    /// <summary>
    ///  Cần gán ngay tại Page_Load
    /// </summary>
    public string Pic { set { _pic = value; } }
    #endregion

    #region Tệp cũ - hdFile.Value
    public string HdFile { set { hdFile.Value = value; } }
    #endregion

    #region Link

    public string Link
    {
        get { return txtLink.Text; }
        set { txtLink.Text = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Kiểm tra xem pic, app đã được gán chưa
        if (_pic == "") throw new Exception("Chưa khởi tạo Pic. Vui lòng xem hướng dẫn tại control Admin\\Control\\Component\\UploadFile.ascx.cs");
        #endregion
    }

    #region Phương thức lưu tệp, cần gọi khi insert, update
    /// <summary>
    /// Phương thức lưu tệp khi insert, update
    /// </summary>
    /// <param name="update">True: đang ở hành động update</param>
    /// <returns></returns>
    public string Save(bool update)
    {
        #region Image
        var vImg = "";

        if (flFile.PostedFile.ContentLength > 0)
        {
            var filename = flFile.FileName;
            var fileEx = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal));
            var path = Request.PhysicalApplicationPath + "/" + _pic + "/";

            #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục
            var dri = new DirectoryInfo(path);
            if (!dri.Exists) dri.Create();
            #endregion

            #region Lưu tệp.
            if (FileLibraryExtension.ValidType(fileEx))
            {
                var fileNotEx = StringExtension.ReplateTitle(filename.Remove(filename.LastIndexOf(".", StringComparison.Ordinal)));
                if (fileNotEx.Length > 55) fileNotEx = fileNotEx.Remove(55);
                var ticks = Guid.NewGuid();
                vImg = fileNotEx + "_" + ticks + fileEx;
                flFile.SaveAs(path + vImg);
            }
            #endregion
        }

        #endregion

        if (!update) return vImg;
        if (vImg == "") vImg = hdFile.Value;
        else ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdFile.Value);
        return vImg;
    }
    #endregion

    #region Phương thức load ra tệp khi hiển thị thông tin cập nhật

    /// <summary>
    /// Phương thức load ra thông tin tệp khi cập nhật
    /// </summary>
    /// <param name="filename">Tên tệp, thường là giá trị trường vgImage hoặc viImage</param>
    /// <param name="link">Link download nếu không có tệp</param>
    public new void Load(string filename, string link)
    {
        txtLink.Text = link;
        if (filename.Length <= 0) return;
        ltimg.Text = "<a href='" + UrlExtension.WebsiteUrl + _pic + "/" + filename + "'>" + filename + "</a>";
        btnDeleteCurrentFile.Visible = true;
        hdFile.Value = filename;
    }
    #endregion

    #region Phương thức reset sau khi tạo xong
    /// <summary>
    /// Phương thức reset sau khi tạo xong
    /// </summary>
    public void Reset()
    {
        ltimg.Text = "";
        hdFile.Value = "";
    }
    #endregion

    protected void btnDeleteCurrentFile_Click(object sender, EventArgs e)
    {
        ImagesExtension.DeleteImageWhenDeleteItem(_pic, hdFile.Value);
        hdFile.Value = "";
        btnDeleteCurrentFile.Visible = false;
        ltimg.Text = "";
    }
}