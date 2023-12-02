
using System.Text;

namespace Developer.Extension
{
    public class PagingCollection
    {
        // Phần này kỹ thuật được chỉnh sửa để phù hợp với html/css, tuy nhiên nên trao đổi với bộ phận làm html/css để quy ước một mẫu html phân trang chung, như vậy sẽ tiết kiệm thời gian cho kỹ thuật khi làm phân trang.
        #region Phân trang trang hiển thị
        /// <summary>
        /// Phân trang hiển thị
        /// </summary>
        /// <param name="numofitems"></param>
        /// <param name="numitems"></param>
        /// <param name="curpage"></param>
        /// <param name="link"></param>
        /// <param name="currentPageCss">Stylesheet cho nút trang hiện tại</param>
        /// <param name="otherPageCss">Stylesheet cho các nút các trang khác</param>
        /// <param name="firstPageCss">Stylesheet cho nút trang đầu (truyền kí tự trống nếu không muốn hiển thị)</param>
        /// <param name="lastPageCss">Stylesheet cho nút trang cuối (truyền kí tự trống nếu không muốn hiển thị)</param>
        /// <param name="previewPageCss">Stylesheet cho nút trang trước (truyền kí tự trống nếu không muốn hiển thị)</param>
        /// <param name="nextPageCss">Stylesheet cho nút trang sau (truyền kí tự trống nếu không muốn hiển thị)</param>
        /// <returns></returns>
        public static string SplitPages(int numofitems, int numitems, int curpage, string link, string currentPageCss, string otherPageCss, string firstPageCss, string lastPageCss, string previewPageCss, string nextPageCss)
        {
            var s = new StringBuilder();
            var numpages = 0;
            numpages = numofitems / numitems;
            if (numofitems % numitems > 0) numpages += 1;
            if (curpage < 0) curpage = 0;
            string prvpage = "", nxtpage = "";
            prvpage = "<li class='" + previewPageCss + "'><a href='" + (curpage == 1 ? "javascript://" : "/" + link + "/p-" + (curpage - 1) + RewriteExtension.Extensions) + "'>«</a></li>";
            nxtpage = "<li class='" + nextPageCss + "'><a href='" + (curpage == numpages ? "javascript://" : "/" + link + "/p-" + (curpage + 1) + RewriteExtension.Extensions) + "'>»</a></li>";
            string firstPage = "", endPage = "";
            firstPage = "<li class='" + firstPageCss + "'><a href='/" + link + RewriteExtension.Extensions + "'>««</a></li>";
            endPage = "<li class='" + lastPageCss + "'><a href='/" + link + "/p-" + numpages + RewriteExtension.Extensions + "'>»»</a></li>";

            if (numpages <= 1) return "";
            var strPage = "";
            if (numpages < 10)
            {
                for (var i = 0; i < numpages; i++)
                {
                    if (curpage == i + 1)
                    {
                        s.Append("<li class='" + currentPageCss + "'><a href='javascript://'>" + (i + 1) + "</a></li>");
                    }
                    else
                    {
                        s.Append("<li class='" + otherPageCss + "'><a href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "'>" + (i + 1) + "</a></li>");
                    }
                }
            }
            else
            {
                if (curpage < 10)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (curpage == i + 1)
                        {
                            s.Append("<li class='" + currentPageCss + "'><a href='javascript://'>" + (i + 1) + "</a></li>");
                        }
                        else
                        {
                            s.Append("<li class='" + otherPageCss + "'><a  href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "'>" + (i + 1) + "</a></li>");
                        }
                    }
                }
                else if (curpage > numpages - 5)
                {
                    for (var i = numpages - 5; i < numpages; i++)
                    {
                        if (curpage == i + 1)
                        {
                            s.Append("<li class='" + currentPageCss + "'><a href='javascript://'>" + (i + 1) + "</a></li>");
                        }
                        else
                        {
                            s.Append("<li class='" + otherPageCss + "'><a href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "'>" + (i + 1) + "</a></li>");
                        }
                    }
                }
                else
                {
                    for (var i = curpage - 5; i < curpage + 5; i++)
                    {
                        if (curpage == i + 1)
                        {
                            s.Append("<li class='" + currentPageCss + "'><a href='javascript://'>" + (i + 1) + "</a></li>");
                        }
                        else
                        {
                            s.Append("<li class='" + otherPageCss + "'><a href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "'>" + (i + 1) + "</a></li>");
                        }
                    }
                }
            }
            strPage += "<ul class='pagination'>" + (curpage != 1 ? firstPage + prvpage : "") + s + (curpage != numpages ? nxtpage + endPage : "") + "</ul>";
            return strPage.Replace("/p-1.htm", ".htm");
        }
        public static string SplitPagesNoRewriteDisplay(int numofitems, int numitems, int curpage, string link, string currentPageCss, string otherPageCss, string firstPageCss, string lastPageCss, string previewPageCss, string nextPageCss, string filter)
        {
            var s = new StringBuilder();
            var pageNo = numofitems / numitems;
            if (numofitems % numitems > 0) pageNo += 1;
            if (curpage < 0) curpage = 0;
            var prvpage = "<li class='" + previewPageCss + "'><a title='Previous' href='" + (curpage == 1 ? "javascript://" : "/" + link + "/p-" + (curpage - 1) + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems) + "'>«</a></li>";
            var nxtpage = "<li class='" + nextPageCss + "'><a title='Next' href='" + (curpage == pageNo ? "javascript://" : "/" + link + "/p-" + (curpage + 1) + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems) + "'>»</a></li>";
            //if (curpage > 1) prvpage = "<a class='" + previewPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + (curpage - 1) + "'><i class='fa fa-backward'></i></a>";
            //if (numpages > 2 && curpage < numpages) nxtpage = "<a class='" + nextPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + (curpage + 1) + "'><i class='fa fa-forward'></i></a>";

            var firstPage = "<li class='" + firstPageCss + "'><a href='/" + link + "/p-1" + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems + "'>««</a></li>";
            var endPage = "<li class='" + lastPageCss + "'><a href='/" + link + "/p-" + pageNo + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems + "'>»»</a></li>";
            if (pageNo <= 1) return "";
            var strPage = "";
            if (pageNo < 10)
            {
                for (var i = 0; i < pageNo; i++)
                {
                    if (curpage == i + 1)
                    {
                        s.Append("<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>");
                    }
                    else
                    {
                        s.Append("<li class='" + otherPageCss + "'><a href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems + "'>" + (i + 1) + "</a></li>");
                    }
                }
            }
            else
            {
                if (curpage < 10)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (curpage == i + 1)
                        {
                            s.Append("<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>");
                        }
                        else
                        {
                            s.Append("<li class='" + otherPageCss + "'><a href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems + "'>" + (i + 1) + "</a></li>");
                        }
                    }
                }
                else if (curpage > pageNo - 5)
                {
                    for (var i = pageNo - 5; i < pageNo; i++)
                    {
                        if (curpage == i + 1)
                        {
                            s.Append("<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>");
                        }
                        else
                        {
                            s.Append("<li class='" + otherPageCss + "'><a href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems + "'>" + (i + 1) + "</a></li>");
                        }
                    }
                }
                else
                {
                    for (var i = curpage - 5; i < curpage + 5; i++)
                    {
                        if (curpage == i + 1)
                        {
                            s.Append("<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>");
                        }
                        else
                        {
                            s.Append("<li class='" + otherPageCss + "'><a href='/" + link + "/p-" + (i + 1) + RewriteExtension.Extensions + "?filter=" + filter + "&ni=" + numofitems + "'>" + (i + 1) + "</a></li>");
                        }
                    }
                }
            }
            strPage += "<ul class='pagination'>" + (curpage != 1 ? firstPage + prvpage : "") + s + (curpage != pageNo ? nxtpage + endPage : "") + "</ul>";
            return strPage.Replace("/p-1.htm", ".htm");
        }

        public static string SpilitPagesNoRewriteDisplay(int numofitems, int numitems, int curpage, string link, string currentPageCss, string otherPageCss, string firstPageCss, string lastPageCss, string previewPageCss, string nextPageCss)
        {
            var numpages = 0;
            numpages = numofitems / numitems;
            if (numofitems % numitems > 0) numpages += 1;
            if (curpage < 0) curpage = 0;
            string prvpage = "", nxtpage = "";
            if (curpage > 1) prvpage = "<li><a class='" + previewPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + (curpage - 1) + "'>«</a></li>";
            if (numpages > 2 && curpage < numpages) nxtpage = "<li><a class='" + nextPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + (curpage + 1) + "'>»</a></li>";

            var firstPage = "<li><a class='" + firstPageCss + "' href='" + link + "&ni=" + numofitems + "&p=1'>««</a></li>";
            var endPage = "<li><a class='" + lastPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + numpages + "'>»»</a></li>";
            if (numpages <= 1) return "";
            var strPage = "";
            if (numpages < 10)
            {
                for (var i = 0; i < numpages; i++)
                {
                    if (curpage == i + 1)
                    {
                        strPage += "<li><a class='" + currentPageCss + "' href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                    }
                    else
                    {
                        strPage += "<li><a class='" + otherPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                    }
                }
            }
            else
            {
                if (curpage < 10)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (curpage == (i + 1))
                        {
                            strPage += "<li><a class='" + currentPageCss + "' href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                        }
                        else
                        {
                            strPage += "<li><a class='" + otherPageCss + "'  href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                        }
                    }
                }
                else if (curpage > numpages - 5)
                {
                    for (var i = numpages - 5; i < numpages; i++)
                    {
                        if (curpage == (i + 1))
                        {
                            strPage += "<li><a class='" + currentPageCss + "' href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                        }
                        else
                        {
                            strPage += "<li><a class='" + otherPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                        }
                    }
                }
                else
                {
                    for (var i = curpage - 5; i < curpage + 5; i++)
                    {
                        if (curpage == (i + 1))
                        {
                            strPage += "<li><a class='" + currentPageCss + "' href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                        }
                        else
                        {
                            strPage += "<li><a class='" + otherPageCss + "' href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                        }
                    }
                }
            }
            strPage = "<ul class='pagination'> " + (curpage != 1 ? firstPage + prvpage : "") + strPage + (curpage < numpages ? nxtpage + endPage : "") + " </ul>";
            return strPage;
        }

        #endregion

        // Phân trang trong trang quản trị, phần này bắt buộc để lại, kỹ thuật không nên chỉnh sửa
        #region Phân trang quản trị

        public static string SpilitPagesNoRewrite(int numofitems, int numitems, int curpage, string link, string currentPageCss, string otherPageCss, string firstPageCss, string lastPageCss, string previewPageCss, string nextPageCss)
        {
            var numpages = 0;
            numpages = numofitems / numitems;
            if (numofitems % numitems > 0) numpages += 1;
            if (curpage < 0) curpage = 0;
            string prvpage = "", nxtpage = "";
            if (curpage > 1) prvpage = "<li class='" + previewPageCss + "'><a href='" + link + "&ni=" + numofitems + "&p=" + (curpage - 1) + "'><i class='hi hi-backward'></i></a></li>";
            if (numpages > 2 && curpage < numpages) nxtpage = "<li class='" + nextPageCss + "'><a href='" + link + "&ni=" + numofitems + "&p=" + (curpage + 1) + "'><i class='hi hi-forward'></i></a></li>";

            string firstPage = "", endPage = "";
            if (curpage > 1) firstPage = "<li class='" + firstPageCss + "'><a href='" + link + "&ni=" + numofitems + "&p=1'><i class='hi hi-fast-backward'></i></a></li>";
            if (numpages > 2 && curpage < numpages) endPage = "<li class='" + lastPageCss + "'><a href='" + link + "&ni=" + numofitems + "&p=" + numpages + "'><i class='hi hi-fast-forward'></i></a></li>";
            if (numpages <= 1) return "";
            var strPage = "";
            if (numpages < 10)
            {
                for (var i = 0; i < numpages; i++)
                {
                    if (curpage == i + 1)
                    {
                        strPage += "<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                    }
                    else
                    {
                        strPage += "<li class='" + otherPageCss + "'><a href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                    }
                }
            }
            else
            {
                if (curpage < 10)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (curpage == i + 1)
                        {
                            strPage += "<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                        }
                        else
                        {
                            strPage += "<li class='" + otherPageCss + "'><a  href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                        }
                    }
                }
                else if (curpage > numpages - 5)
                {
                    for (var i = numpages - 5; i < numpages; i++)
                    {
                        if (curpage == i + 1)
                        {
                            strPage += "<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                        }
                        else
                        {
                            strPage += "<li class='" + otherPageCss + "'><a href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                        }
                    }
                }
                else
                {
                    for (var i = curpage - 5; i < curpage + 5; i++)
                    {
                        if (curpage == i + 1)
                        {
                            strPage += "<li class='" + currentPageCss + "'><a href='javascript:void(0);'>" + (i + 1) + "</a></li>";
                        }
                        else
                        {
                            strPage += "<li class='" + otherPageCss + "'><a href='" + link + "&ni=" + numofitems + "&p=" + (i + 1) + "'>" + (i + 1) + "</a></li>";
                        }
                    }
                }
            }
            strPage = "<ul class='pagination pagination-sm remove-margin'> " + firstPage + prvpage + strPage + nxtpage + endPage + " </ul>";
            return strPage;
        }

        #endregion
    }
}
