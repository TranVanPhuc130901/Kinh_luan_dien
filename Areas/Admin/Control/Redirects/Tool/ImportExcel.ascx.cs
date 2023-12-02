using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.TSql;

public partial class Areas_Admin_Control_Redirect_Tool_ImportExcel : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btSubmit_OnClick(object sender, EventArgs e)
    {
        //Dựa vào kết quả trả về để đưa ra thông báo
        /*
         * 0: Nhập dữ liệu thành công, không xuất hiện lỗi
         * 1: Nhập dữ liệu thất bại, hãy chọn tệp Excel(*.xls)
         */
        switch (UploadExcel())
        {
            case 0:
                ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Import thành công', {type: 'success'});});", true);
                break;
            case 1:
                ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Import thất bại! Vui lòng chọn đúng tệp Excel(*.xls hoặc *.xlsx)', {type: 'success'});});", true);
                break;
            default:
                ScriptManager.RegisterStartupScript(this, GetType(), "", "document.addEventListener('DOMContentLoaded', function () {$.bootstrapGrowl('Hệ thống đang bận, vui lòng thử lại.', {type: 'success'});});", true);
                break;
        }
    }

    private int UploadExcel()
    {
        if (fuExcel.FileName.Length <= 3) return 1;
        var excelFilePath = Path.GetTempFileName();
        fuExcel.SaveAs(excelFilePath);
        IWorkbook workbook;
        using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            if (fuExcel.FileName.ToLower().EndsWith(".xlsx")) workbook = new XSSFWorkbook(stream);
            else workbook = new HSSFWorkbook(stream);
        }
        var sheet = workbook.GetSheetAt(0); // zero-based index of your target sheet
        var dtExcel = new DataTable(sheet.SheetName);

        // write header row
        var headerRow = sheet.GetRow(0);
        foreach (var headerCell in headerRow)
        {
            dtExcel.Columns.Add(headerCell.ToString());
        }

        // write the rest
        var rowIndex = 0;
        foreach (IRow row in sheet)
        {
            // skip header row
            if (rowIndex++ == 0) continue;
            var dataRow = dtExcel.NewRow();
            dataRow.ItemArray = row.Cells.Select(c => c.ToString()).ToArray();
            dtExcel.Rows.Add(dataRow);
        }

        var dtNew = new DataTable();
        dtNew.Columns.Add(RedirectsColumns.VrLink);
        dtNew.Columns.Add(RedirectsColumns.VrLinkDestination);
        for (var i = 0; i < dtExcel.Rows.Count; i++)
        {
            var dt = Redirects.GetData("1", "*", RedirectsTSql.GetByLink(dtExcel.Rows[i][0].ToString()), "");
            if (dt.Rows.Count > 0) Redirects.Update(dt.Rows[0][RedirectsColumns.VrLink].ToString(), dtExcel.Rows[i][1].ToString(), dt.Rows[0][RedirectsColumns.DrDateCreated].ToString(), DateTime.Now.ToString(), "1", dt.Rows[0][RedirectsColumns.IrId].ToString());
            else Redirects.Insert(dtExcel.Rows[i][0].ToString(), dtExcel.Rows[i][1].ToString(), DateTime.Now.ToString(), DateTime.Now.ToString(), "1");
        }

        return 0;
    }
}