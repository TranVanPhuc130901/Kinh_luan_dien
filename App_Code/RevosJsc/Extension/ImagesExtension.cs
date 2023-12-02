using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;
using RevosJsc.AdvertistmentsControl;

namespace RevosJsc.Extension
{
    public class ImagesExtension
    {
        public static string GetFolderByApp(string app)
        {
            var folder = "";
            switch (app.ToLower())
            {
                case CodeApplications.Advertistments:
                    folder = "pic/banner";
                    break;
                default:
                    folder = "pic/" + app;
                    break;
            }
            return folder.ToLower();
        }

        public static string GetImageLink(string folderName, string imageName)
        {
            return folderName + "/" + imageName;
        }

        #region IMAGE

        /// <summary>
        /// Lấy ảnh đại diện. Nếu ảnh đại diện có tên dạng Tên_ảnh_HasThumb.phần_mở_rộng thì sẽ tự động lấy ảnh nhỏ có tên Tên_ảnh_HasThumb_Thumb.phần mở rộng
        /// </summary>
        /// <param name="folderName">Tên folder ảnh theo app</param>
        /// <param name="titleImage">Tên ảnh</param>
        /// <param name="altImage">Nội dung cho thuộc tính alt</param>
        /// <param name="cssImage">class định dạng cho ảnh</param>
        /// <param name="defaultImage">true: lấy ảnh mặc định (pic/icon/no_image.svg) nếu không có ảnh đại diện</param>
        /// <param name="getFirstImageInContent">lấy ảnh đầu tiên trong nội dung (vd: lấy ảnh trong nội dung của tin tức làm ảnh đại diện nếu tin đó không có ảnh đại diện)</param>
        /// <param name="content">Nội dung chứa ảnh để lấy nếu không có ảnh đại diện</param>
        /// <returns></returns>
        public static string GetImage(string folderName, string titleImage, string altImage, string cssImage, bool defaultImage, bool getFirstImageInContent, string content)
        {
            if (altImage == null) altImage = "";
            altImage = altImage.Replace("\"", "").Replace("\'", "");
            var s = "";
            if (!string.IsNullOrEmpty(titleImage))
            {
                if (titleImage.IndexOf("_HasThumb", StringComparison.Ordinal) > -1) titleImage = titleImage.Replace("_HasThumb", "_HasThumb_Thumb");
                s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + "/" + folderName + "/" + titleImage + "\" />";
            }
            else
            {
                if (getFirstImageInContent)
                {
                    if (GetFirstImageInContent(content).Length > 0)
                    {
                        s = "<img class=\"" + cssImage + "\" src=\"" + GetFirstImageInContent(content) + "\" />";
                    }
                    else
                    {
                        if (defaultImage)
                        {
                            s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + "/pic/icon/no_image.svg\" />";
                        }
                        else
                        {
                            s = "";
                        }
                    }
                }
                else
                {
                    if (defaultImage)
                    {
                        s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + "/pic/icon/no_image.svg\" />";
                    }
                    else
                    {
                        s = "";
                    }
                }
            }
            return s;
        }

        /// <summary>
        /// Lấy ảnh đại diện. Nếu ảnh đại diện có tên dạng Tên_ảnh_HasThumb.phần_mở_rộng thì sẽ tự động lấy ảnh nhỏ có tên Tên_ảnh_HasThumb_Thumb.phần mở rộng
        /// </summary>
        /// <param name="folderName">Tên folder ảnh theo app</param>
        /// <param name="titleImage">Tên ảnh</param>
        /// <param name="altImage">Nội dung cho thuộc tính alt</param>
        /// <param name="cssImage">class định dạng cho ảnh</param>
        /// <param name="defaultImage">true: lấy ảnh mặc định (pic/icon/no_image.svg) nếu không có ảnh đại diện</param>
        /// <param name="getFirstImageInContent">lấy ảnh đầu tiên trong nội dung (vd: lấy ảnh trong nội dung của tin tức làm ảnh đại diện nếu tin đó không có ảnh đại diện)</param>
        /// <param name="content">Nội dung chứa ảnh để lấy nếu không có ảnh đại diện</param>
        /// <param name="getThumbImage">True: lấy ảnh nhỏ, False: lấy ảnh gốc theo tên truyền vào</param>
        /// <returns></returns>
        public static string GetImage(string folderName, string titleImage, string altImage, string cssImage, bool defaultImage, bool getFirstImageInContent, string content, bool getThumbImage)
        {
            if (titleImage == null) titleImage = "";
            altImage = altImage.Replace("\"", "").Replace("'", "");
            var s = "";
            if (titleImage.Length > 0)
            {
                if (getThumbImage) if (titleImage.IndexOf("_HasThumb", StringComparison.Ordinal) > -1) titleImage = titleImage.Replace("_HasThumb", "_HasThumb_Thumb");
                s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/" + folderName + "/" + titleImage + "\" />";
            }
            else
            {
                if (getFirstImageInContent)
                {
                    if (GetFirstImageInContent(content).Length > 0)
                    {
                        s = "<img class=\"" + cssImage + "\" src=\"" + GetFirstImageInContent(content) + "\" />";
                    }
                    else
                    {
                        if (defaultImage)
                        {
                            s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/pic/icon/no_image.svg\" />";
                        }
                        else
                        {
                            s = "";
                        }
                    }
                }
                else
                {
                    if (defaultImage)
                    {
                        s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/pic/icon/no_image.svg\" />";
                    }
                    else
                    {
                        s = "";
                    }
                }
            }
            return s;
        }

        /// <summary>
        /// Lấy ảnh đại diện. Nếu ảnh đại diện có tên dạng Tên_ảnh_HasThumb.phần_mở_rộng thì sẽ tự động lấy ảnh nhỏ có tên Tên_ảnh_HasThumb_Thumb.phần mở rộng. Cho phép tùy chọn có dùng thuộc tính data-src hay không
        /// </summary>
        /// <param name="folderName">Tên folder ảnh theo app</param>
        /// <param name="titleImage">Tên ảnh</param>
        /// <param name="altImage">Nội dung cho thuộc tính alt</param>
        /// <param name="cssImage">class định dạng cho ảnh</param>
        /// <param name="defaultImage">true: lấy ảnh mặc định (pic/icon/no_image.svg) nếu không có ảnh đại diện</param>
        /// <param name="getFirstImageInContent">lấy ảnh đầu tiên trong nội dung (vd: lấy ảnh trong nội dung của tin tức làm ảnh đại diện nếu tin đó không có ảnh đại diện)</param>
        /// <param name="content">Nội dung chứa ảnh để lấy nếu không có ảnh đại diện</param>
        /// <param name="getThumbImage">True: lấy ảnh nhỏ, False: lấy ảnh gốc theo tên truyền vào</param>
        /// <param name="useDataOriginalAttr">True: tự động thêm thuộc tính data-orginal= giá trị của src, và gán src="pic/icon/grey.gif" nhằm phục vụ xử lý lazy images loading</param>
        /// <returns></returns>
        public static string GetImage(string folderName, string titleImage, string altImage, string cssImage, bool defaultImage, bool getFirstImageInContent, string content, bool getThumbImage, bool useDataOriginalAttr)
        {
            altImage = altImage.Replace("\"", "").Replace("'", "");
            var s = "";
            if (titleImage.Length > 0)
            {
                if (getThumbImage) if (titleImage.IndexOf("_HasThumb", StringComparison.Ordinal) > -1) titleImage = titleImage.Replace("_HasThumb", "_HasThumb_Thumb");
                if (useDataOriginalAttr)
                    s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/" + folderName + "/" + titleImage + "\" data-src=\"/" + folderName + "/" + titleImage + "\" />";
                else
                    s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + folderName + "/" + titleImage + "\" />";
            }
            else
            {
                if (getFirstImageInContent)
                {
                    if (GetFirstImageInContent(content).Length > 0)
                    {
                        if (useDataOriginalAttr) s = "<img class=\"" + cssImage + "\" src=\"" + GetFirstImageInContent(content) + "\" data-src=\"" + GetFirstImageInContent(content) + "\" />";
                        else s = "<img class=\"" + cssImage + "\" src=\"" + GetFirstImageInContent(content) + "\" />";
                    }
                    else
                    {
                        if (defaultImage)
                        {
                            if (useDataOriginalAttr) s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/pic/icon/no_image.svg\" data-src=\"/pic/icon/no_image.svg\" />";
                            else s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/pic/icon/no_image.svg\" />";
                        }
                        else
                        {
                            s = "";
                        }
                    }
                }
                else
                {
                    if (defaultImage)
                    {
                        if (useDataOriginalAttr)
                            s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/pic/icon/no_image.svg\" data-src=\"/pic/icon/no_image.svg\" />";
                        else s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/pic/icon/no_image.svg\" />";
                    }
                    else
                    {
                        s = "";
                    }
                }
            }
            return s;
        }

        /// <summary>
        /// Lấy ảnh đại diện. Nếu ảnh đại diện có tên dạng Tên_ảnh_HasThumb.phần_mở_rộng thì sẽ tự động lấy ảnh nhỏ có tên Tên_ảnh_HasThumb_Thumb.phần mở rộng. Cho phép tùy chọn có dùng thuộc tính data-src hay không
        /// </summary>
        /// <param name="folderName">Tên folder ảnh theo app</param>
        /// <param name="titleImage">Tên ảnh</param>
        /// <param name="altImage">Nội dung cho thuộc tính alt</param>
        /// <param name="cssImage">class định dạng cho ảnh</param>
        /// <param name="defaultImage">true: lấy ảnh mặc định (pic/icon/no_image.svg) nếu không có ảnh đại diện</param>
        /// <param name="getFirstImageInContent">lấy ảnh đầu tiên trong nội dung (vd: lấy ảnh trong nội dung của tin tức làm ảnh đại diện nếu tin đó không có ảnh đại diện)</param>
        /// <param name="content">Nội dung chứa ảnh để lấy nếu không có ảnh đại diện</param>
        /// <param name="getThumbImage">True: lấy ảnh nhỏ, False: lấy ảnh gốc theo tên truyền vào</param>
        /// <param name="useDataSrcAttr">True: tự động thêm thuộc tính data-orginal= giá trị của src, và gán src="pic/icon/grey.gif" nhằm phục vụ xử lý lazy images loading</param>
        /// <param name="imageSrc"></param>
        /// <returns></returns>
        public static string GetImage(string folderName, string titleImage, string altImage, string cssImage, bool defaultImage, bool getFirstImageInContent, string content, bool getThumbImage, bool useDataSrcAttr, string imageSrc)
        {
            var noImageUrl = "/pic/icon/no_image.svg";
            //var greyImageSrc = "/pic/icon/grey.gif";
            altImage = altImage.Replace("\"", "").Replace("'", "");
            var s = "";
            if (titleImage.Length > 0)
            {
                if (getThumbImage) if (titleImage.IndexOf("_HasThumb", StringComparison.Ordinal) > -1) titleImage = titleImage.Replace("_HasThumb", "_HasThumb_Thumb");
                if (useDataSrcAttr)
                    s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + (imageSrc.Length > 0 ? imageSrc : "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==") + "\" data-src=\"/" + folderName + "/" + titleImage + "\" />";
                else s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/" + folderName + "/" + titleImage + "\" />";
            }
            else
            {
                if (getFirstImageInContent)
                {
                    if (GetFirstImageInContent(content).Length > 0)
                    {
                        if (useDataSrcAttr) s = "<img class=\"" + cssImage + "\" src=\"" + imageSrc + "\" data-src=\"" + GetFirstImageInContent(content) + "\" />";
                        else s = "<img class=\"" + cssImage + "\" src=\"" + GetFirstImageInContent(content) + "\" />";
                    }
                    else
                    {
                        if (defaultImage)
                        {
                            if (useDataSrcAttr) s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + imageSrc + "\" data-src=\"" + noImageUrl + "\" />";
                            else s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + noImageUrl + "\" />";
                        }
                        else
                        {
                            s = "";
                        }
                    }
                }
                else
                {
                    if (defaultImage)
                    {
                        if (useDataSrcAttr) s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + imageSrc + "\" data-src=\"" + noImageUrl + "\" />";
                        else s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + noImageUrl + "\" />";
                    }
                    else
                    {
                        s = "";
                    }
                }
            }
            return s;
        }

        /// <summary>
        /// Lấy ảnh đại diện. Nếu ảnh đại diện có tên dạng Tên_ảnh_HasThumb.phần_mở_rộng thì sẽ tự động lấy ảnh nhỏ có tên Tên_ảnh_HasThumb_Thumb.phần mở rộng. Cho phép tùy chọn có dùng thuộc tính data-src hay không
        /// </summary>
        /// <param name="folderName">Tên folder ảnh theo app</param>
        /// <param name="titleImage">Tên ảnh</param>
        /// <param name="altImage">Nội dung cho thuộc tính alt</param>
        /// <param name="cssImage">class định dạng cho ảnh</param>
        /// <param name="defaultImage">true: lấy ảnh mặc định (pic/icon/no_image.svg) nếu không có ảnh đại diện</param>
        /// <param name="getFirstImageInContent">lấy ảnh đầu tiên trong nội dung (vd: lấy ảnh trong nội dung của tin tức làm ảnh đại diện nếu tin đó không có ảnh đại diện)</param>
        /// <param name="content">Nội dung chứa ảnh để lấy nếu không có ảnh đại diện</param>
        /// <param name="getThumbImage">True: lấy ảnh nhỏ, False: lấy ảnh gốc theo tên truyền vào</param>
        /// <param name="useDataOriginalAttr">True: tự động thêm thuộc tính data-orginal= giá trị của src, và gán src="pic/icon/grey.gif" nhằm phục vụ xử lý lazy images loading</param>
        /// <param name="imageSrc">Đường dẫn ảnh giả</param>
        /// <param name="size">VD: 0</param>
        /// <returns></returns>
        public static string GetImage(string folderName, string titleImage, string altImage, string cssImage, bool defaultImage, bool getFirstImageInContent, string content, bool getThumbImage, bool useDataOriginalAttr, string imageSrc, string size)
        {
            var noImageUrl = "/pic/icon/no_image.svg";
            //var greyImageSrc = "/pic/icon/grey.gif";
            altImage = altImage.Replace("\"", "").Replace("'", "");
            var s = "";
            if (titleImage.Length > 0)
            {
                if (getThumbImage) if (titleImage.IndexOf("_HasThumb", StringComparison.Ordinal) > -1) titleImage = titleImage.Replace("_HasThumb", "_HasThumb_Thumb" + size);
                if (useDataOriginalAttr)
                    s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + imageSrc + "\" data-src=\"/" + folderName + "/" + titleImage + "\" />";
                else
                    s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"/" + folderName + "/" + titleImage + "\" />";
            }
            else
            {
                if (getFirstImageInContent)
                {
                    if (GetFirstImageInContent(content).Length > 0)
                    {
                        if (useDataOriginalAttr) s = "<img class=\"" + cssImage + "\" src=\"" + imageSrc + "\" data-src=\"" + GetFirstImageInContent(content) + "\" />";
                        else s = "<img class=\"" + cssImage + "\" src=\"" + GetFirstImageInContent(content) + "\" />";
                    }
                    else
                    {
                        if (defaultImage)
                        {
                            if (useDataOriginalAttr) s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + imageSrc + "\" data-src=\"" + noImageUrl + "\" />";
                            else s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + noImageUrl + "\" />";
                        }
                        else
                        {
                            s = "";
                        }
                    }
                }
                else
                {
                    if (defaultImage)
                    {
                        if (useDataOriginalAttr) s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + imageSrc + "\" data-src=\"" + noImageUrl + "\" />";
                        else s = "<img alt=\"" + altImage + "\" class=\"" + cssImage + "\" src=\"" + noImageUrl + "\" />";
                    }
                    else
                    {
                        s = "";
                    }
                }
            }
            return s;
        }

        #endregion IMAGE

        #region Customize

        /// <summary>
        /// Xoá ảnh trên thư mục của ổ cứng.
        /// Tự động xoá ảnh nhỏ nếu tên ảnh lớn có dạng Tên_ảnh_HasThumb.phần_mở_rộng
        /// Khi đó ảnh nhỏ bị xoá sẽ là: Tên_ảnh_HasThumb_Thumb.phần_mở_rộng
        /// </summary>
        /// <param name="pathFolderName">Tên folder ảnh</param>
        /// <param name="titleImage">Tên ảnh</param>
        public static void DeleteImageWhenDeleteItem(string pathFolderName, string titleImage)
        {
            try
            {
                if (titleImage.Length > 0) File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "/" + pathFolderName + "/" + titleImage);
                if (titleImage.IndexOf("_HasThumb", StringComparison.Ordinal) <= -1) return;
                File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "/" + pathFolderName + "/" + titleImage.Replace("_HasThumb", "_HasThumb_Thumb"));
                File.Delete(HttpContext.Current.Request.PhysicalApplicationPath + "/" + pathFolderName + "/" + titleImage.Replace("_HasThumb", "_HasThumb_Thumb0"));
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Lấy ảnh đầu tiên trong một đoạn nội dung
        /// </summary>
        /// <param name="content">Nội dung chứa ảnh</param>
        /// <returns></returns>
        public static string GetFirstImageInContent(string content)
        {
            if (content == null)
            {
                return "";
            }
            content = content.Replace("<IMG", "<img");
            var ifound = 1111;
            while (ifound > -1)
            {
                if (ifound != 1111)
                    for (var i = ifound; i < content.Length; i++)
                    {
                        if (!content.Substring(i, 1).Equals(">")) continue;
                        var oldstr = content.Substring(ifound, i - ifound + 1);
                        var isrc1 = oldstr.IndexOf("src=\"", StringComparison.Ordinal);
                        var isrc2 = -1;
                        for (var j = isrc1 + 5; j < oldstr.Length; j++)
                        {
                            if (!oldstr.Substring(j, 1).Equals("\"")) continue;
                            isrc2 = j;
                            break;
                        }
                        var strSrc = oldstr.Substring(isrc1 + 5, isrc2 - isrc1 - 5);
                        return strSrc;
                    } // end for
                ifound = content.IndexOf("<img ", StringComparison.Ordinal);
            }
            return "";
        }

        /// <summary>
        /// Lấy ảnh đại điện cho loại file. Kết quả trả về là thẻ img có src dạng: pic/logo/TenPhanMoRong.gif
        /// </summary>
        /// <param name="imageName">Tên của ảnh. vd: image1.jpg -> kết quả là thẻ img có src: pic/logo/jpg.gif</param>
        /// <param name="cssImage">Class định dạng cho ảnh</param>
        /// <param name="defaultImage">true: trả về thẻ img có src: pic/logo/noImage.gif</param>
        /// <returns></returns>
        public static string GetIconForFile(string imageName, string cssImage, bool defaultImage)
        {
            var s = "";
            if (imageName.Length < 1 && defaultImage)
            {
                s = "<img class=\"" + cssImage + "\" src=\"pic/logo/noImage.gif\" />";
            }
            else
            {
                var fileExtension = imageName.Substring(imageName.LastIndexOf(".", StringComparison.Ordinal) + 1, imageName.Length - imageName.LastIndexOf(".", StringComparison.Ordinal));
                s = "<img src=\"pic/logo/" + fileExtension + ".gif\" class=\"" + cssImage + "\" />";
            }
            return s;
        }

        #endregion Customize

        #region Lưu và xoá ảnh trong chi tiết khi nhập chi tiết cho Items

        /// <summary>
        /// Lấy tất cả các thẻ img trong nội dung. Kết quả trả về là các thẻ img cách nhau bởi dấu ,
        /// </summary>
        /// <param name="content">nội dung chứa thẻ img</param>
        /// <returns></returns>
        public static string GetImageTagsFromContent(string content)
        {
            var s = "";
            var tempContent = content.Replace("IMG", "img");
            var startIndex = -1;
            var endIndex = -1;
            //Duyệt trong khi nội dung vẫn chứa thẻ img
            while (tempContent.IndexOf("<img", startIndex + 1, StringComparison.Ordinal) > -1)
            {
                //Dánh dấu vị trí đầu tiên tìm thấy thẻ img đầu tiên
                startIndex = tempContent.IndexOf("<img", startIndex + 1, StringComparison.Ordinal);
                //Duyệt từ vị trí đầu để tìm vị trí kết thúc của thẻ img đầu tiên
                for (var i = startIndex; i < tempContent.Length; i++)
                {
                    if (tempContent[i] != '>') continue;
                    endIndex = i;
                    break;
                }
                if (endIndex > startIndex) s += content.Substring(startIndex, endIndex - startIndex + 1) + "*!<=*_*=>*!";
            }
            return s;
        }

        /// <summary>
        /// Lấy url ảnh từ thẻ img
        /// </summary>
        /// <param name="imgTag">Thẻ img</param>
        /// <returns></returns>
        public static string GetUrlFromImgTag(string imgTag)
        {
            var s = "";
            var tempImgTag = imgTag.Replace("SRC", "src");
            //Thực hiện khi thuộc tính src dùng với dấu "
            if (tempImgTag.IndexOf("src=\"", StringComparison.Ordinal) > -1)
            {
                var startIndex = tempImgTag.IndexOf("src=\"", StringComparison.Ordinal) + "src=\"".Length;
                var endIdext = 0;
                for (var i = startIndex; i < tempImgTag.Length; i++)
                {
                    if (tempImgTag[i].ToString() != "\"") continue;
                    endIdext = i;
                    break;
                }
                if (endIdext > startIndex)
                    s = imgTag.Substring(startIndex, endIdext - startIndex);
            }
            else //Thực hiện khi thuộc tính src dùng với dấu '
            {
                var startIndex = tempImgTag.IndexOf("src='", StringComparison.Ordinal) + "src='".Length;
                var endIdext = 0;
                for (var i = startIndex; i < tempImgTag.Length; i++)
                {
                    if (tempImgTag[i].ToString() != "'") continue;
                    endIdext = i;
                    break;
                }
                if (endIdext > startIndex) s = imgTag.Substring(startIndex, endIdext - startIndex);
            }
            return s;
        }

        /// <summary>
        /// Lưu ảnh từ một url vào ổ đĩa server, trả về phẩn mở rộng của file được lưu (vd: .jpg), trả về rỗng nếu lưu thất bại
        /// </summary>
        /// <param name="fileName">Đường dẫn file cần lưu (không chứa phần mởi rộng) (vd: D:\MyFolder\FileName)</param>
        /// <param name="url">vd: http://revos.vn/pic/news/sample.jpg hoặc /pic/news/sample.jpg</param>
        public static string SaveImageFromUrl(string fileName, string url)
        {
            try
            {
                if (!url.ToLower().StartsWith("http")) //Nếu url không phải link đầy đủ
                {
                    var picIndex = url.IndexOf("pic/", StringComparison.Ordinal);
                    if (picIndex > -1) url = url.Substring(picIndex);
                }
                byte[] content;

                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();

                using (var br = new BinaryReader(stream))
                {
                    content = br.ReadBytes(500000);
                    br.Close();
                }
                response.Close();
                var fileExtension = url.Substring(url.LastIndexOf(".", StringComparison.Ordinal), url.Length - url.LastIndexOf(".", StringComparison.Ordinal));
                fileName += fileExtension;

                var fs = new FileStream(fileName, FileMode.Create);
                var bw = new BinaryWriter(fs);
                try
                {
                    bw.Write(content);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }

                return fileExtension;
            }
            catch
            {
                return "";
            }
        }
        public static void SaveImageFromUrl2(string fileName, string url)
        {
            try
            {
                if (!url.ToLower().StartsWith("http")) //Nếu url không phải link đầy đủ
                {
                    var picIndex = url.IndexOf("pic/", StringComparison.Ordinal);
                    if (picIndex > -1) url = url.Substring(picIndex);
                }
                byte[] content;

                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = request.GetResponse();
                var stream = response.GetResponseStream();

                using (var br = new BinaryReader(stream))
                {
                    content = br.ReadBytes(500000);
                    br.Close();
                }
                response.Close();
                var fileExtension = url.Substring(url.LastIndexOf(".", StringComparison.Ordinal), url.Length - url.LastIndexOf(".", StringComparison.Ordinal));
                fileName += fileExtension;

                var fs = new FileStream(fileName, FileMode.Create);
                var bw = new BinaryWriter(fs);
                try
                {
                    bw.Write(content);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }

            }
            catch
            {
                //nothing
            }
        }
        /// <summary>
        /// Lưu ảnh từ một url kiểu data: vào ổ đĩa server, trả về phẩn mở rộng của file được lưu (vd: .jpg), trả về rỗng nếu lưu thất bại
        /// </summary>
        /// <param name="fileName">Đường dẫn file cần lưu (không chứa phần mởi rộng) (vd: D:\MyFolder\FileName)</param>
        /// <param name="url">vd: data:...</param>
        public static string SaveImageFromData(string fileName, string url)
        {
            try
            {
                var imageBytes = Convert.FromBase64String(url.Substring(url.IndexOf("base64,", StringComparison.Ordinal) + "base64,".Length));

                var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                var image = Image.FromStream(ms, true);

                image.Save(fileName + ".jpg");

                return ".jpg";
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// Lưu lại nội dung với việc thay thế các thẻ image có src dạng data:image/ bằng thẻ image chuẩn
        /// </summary>
        /// <param name="content">Nội dung chứa ảnh</param>
        /// <param name="pathFolderPic">Đường dẫn tới thư mục lưu ảnh. VD FolderPic.News</param>
        public static string SaveContentImage(string pathFolderPic, string content)
        {
            foreach (var s in GetImageTagsFromContent(content).Split(new[] { StringExtension.SpecialCharactersKeyword.ParamsSpilitItems }, StringSplitOptions.None))
            {
                if (s.Length <= 0) continue;
                var url = "";
                var newFileName = Guid.NewGuid().ToString();
                url = GetUrlFromImgTag(s);
                if (url.IndexOf("data:", StringComparison.Ordinal) <= -1) continue;
                var newFileExtension = SaveImageFromData(HttpContext.Current.Request.PhysicalApplicationPath + "/" + pathFolderPic + "/" + newFileName, url);
                if (newFileExtension.Length <= 0) continue;
                newFileName += newFileExtension;
                content = content.Replace(url, "/" + pathFolderPic + "/" + newFileName);
            }
            return content;
        }


        /// <summary>
        /// Lưu các ảnh từ nội dung vào ổ đĩa trên server và cập nhật lại đường dẫn ảnh. Phương thức này dùng để xử lý khi nhập nội dung chi tiết cho tin tức, sản phẩm...
        /// </summary>
        /// <param name="content">Nội dung chứa ảnh</param>
        /// <param name="pathFolderPic">Đường dẫn tới thư mục lưu ảnh. VD FolderPic.News</param>
        public static string SaveContentFromOtherWebsite(string pathFolderPic, string content)
        {
            var path = HttpContext.Current.Request.PhysicalApplicationPath + "/" + pathFolderPic;

            #region Kiểm tra xem thư mục đã tồn tại chưa, nếu chưa -> tạo mới thư mục

            var dri = new DirectoryInfo(path);
            if (!dri.Exists) dri.Create();

            #endregion

            foreach ( var s in GetImageTagsFromContent(content).Split(new[] { StringExtension.SpecialCharactersKeyword.ParamsSpilitItems }, StringSplitOptions.None))
            {
                if (s.Length <= 0) continue;
                var newFileName = Guid.NewGuid().ToString();
                var url = GetUrlFromImgTag(s);
                if (ImageInOurServer(url)) continue;
                
                var newFileExtension = SaveImageFromUrl(path + "/" + newFileName, url);
                if (newFileExtension.Length <= 0) continue;
                content = content.Replace(url, "/" + pathFolderPic + "/" + newFileName + newFileExtension + "");
            }
            return content;
        }

        /// <summary>
        /// Kiểm tra xem url có thuộc server hiện tại hay không
        /// </summary>
        /// <param name="url">url của ảnh</param>
        /// <returns></returns>
        public static bool ImageInOurServer(string url)
        {
            //Nếu url ảnh chứa url website hoặc không chứa http (dạng pic/news/pic1.jpg) ---> ảnh thuộc server của ta
            return url.IndexOf(WebsiteUrl, StringComparison.Ordinal) > -1 || url.IndexOf("http", StringComparison.Ordinal) < 0;
        }
        private static string WebsiteUrl
        {
            get
            {
                var url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                var right = HttpContext.Current.Request.Url.AbsolutePath.Remove(HttpContext.Current.Request.Url.AbsolutePath.IndexOf("/", 1, StringComparison.Ordinal) + 1);
                if (right.StartsWith("/Areas"))
                {
                    right = right.Substring("/Areas".Length);
                    if (right.Trim('/').Length > 0) right = "/" + right;
                }
                url += right;
                var dotIndex = url.IndexOf(".", StringComparison.Ordinal);
                if (dotIndex > -1)
                {
                    var splitIndex = url.IndexOf("/", dotIndex, StringComparison.Ordinal);
                    if (splitIndex > -1) url = url.Remove(splitIndex);
                }
                if (!url.EndsWith("/")) url = url + "/";
                return url;
            }
        }
        /// <summary>
        /// Xoá các ảnh xuất hiện trong nội dung nếu ảnh đó thuộc server của mình. Thực hiện phương thức này khi xoá Items như tin tức, sản phẩm
        /// </summary>
        /// <param name="content">Nội dung chứa ảnh (thường là chi tiết tin tức, sản phẩm)</param>
        /// <param name="pathFolderPic">Đường dẫn tới thư mục lưu ảnh. VD FolderPic.News</param>
        public static void DeleteImageFromContent(string pathFolderPic, string content)
        {
            foreach ( var s in GetImageTagsFromContent(content).Split(new[] { StringExtension.SpecialCharactersKeyword.ParamsSpilitItems }, StringSplitOptions.None))
            {
                if (s.Length <= 0) continue;
                var url = GetUrlFromImgTag(s);
                if (!ImageInOurServer(url)) continue;
                url = url.Substring(url.LastIndexOf("/", StringComparison.Ordinal) + 1, url.Length - url.LastIndexOf("/", StringComparison.Ordinal) - 1);
                DeleteImageWhenDeleteItem(pathFolderPic + "/images", url);
                DeleteImageWhenDeleteItem(pathFolderPic + "/thumbs/images", url);
            }
        }

        /// <summary>
        /// Xoá các ảnh xuất hiện trong oldContent mà không xuất hiện trong newContent.
        /// <br></br>
        /// Thực hiện phương thức này khi thêm mới hoặc cập nhật tin tức, sản phẩm. 
        /// <br></br>vd:
        /// <c>
        /// <code>
        /// string oldConent=tbContent.Text;
        /// string newContent=SaveContentFromOtherWebsite(FolderPic.News, tbContent.Text); 
        /// DeleteImageFromContent(oldContent, newContent); //--> thêm newContent vào DB
        /// </code>
        /// </c>
        /// </summary>
        /// <param name="oldContent">Nội dung trước khi được chỉnh sửa</param>
        /// <param name="newContent">Nội dung sau khi đã chỉnh sửa</param>
        /// <param name="pathFolderPic">Đường dẫn tới thư mục lưu ảnh. VD FolderPic.News</param>
        public static void DeleteImageFromContent(string pathFolderPic, string oldContent, string newContent)
        {
            var newListImgtag = GetImageTagsFromContent(newContent);
            foreach ( var s in GetImageTagsFromContent(oldContent).Split(new[] { StringExtension.SpecialCharactersKeyword.ParamsSpilitItems }, StringSplitOptions.None))
            {
                if (s.Length <= 0) continue;
                var url = GetUrlFromImgTag(s);
                //Xoá nếu ảnh này không xuất hiện trong danh sách ảnh mới
                if (!ImageInOurServer(url) || newListImgtag.IndexOf(url, StringComparison.Ordinal) >= 0) continue;
                url = url.Substring(url.LastIndexOf("/", StringComparison.Ordinal) + 1, url.Length - url.LastIndexOf("/", StringComparison.Ordinal) - 1);
                DeleteImageWhenDeleteItem(pathFolderPic, url);
            }
        }
        #endregion Lưu và xoá ảnh trong chi tiết khi nhập chi tiết cho Items

        #region Kiểm tra đuôi ảnh

        /// <summary>
        /// Kiểm tra xem đuôi ảnh có hợp lệ hay không
        /// </summary>
        /// <param name="testType">đuôi ảnh cần kiểm tra(vd: .jpg)</param>
        /// <returns></returns>
        public static bool ValidType(string testType)
        {
            const string listType = ",.gif,.pjp,.jpg,.pjpeg,.jpeg,.jfif,.png,.svgz,.svg,.bmp,";
            testType = testType.ToLower();
            testType = "." + testType;
            testType = testType.Substring(testType.LastIndexOf(".", StringComparison.Ordinal));
            testType = "," + testType + ",";
            return listType.IndexOf(testType, StringComparison.Ordinal) > -1;
        }

        /// <summary>
        /// Kiểm tra xem đuôi ảnh có hợp lệ hay không
        /// </summary>
        /// <param name="testType">đuôi ảnh cần kiểm tra (vd: .jpg)</param>
        /// <param name="listType">danh sách đuôi ảnh được tính là hợp lệ (vd: .jpg,.jpeg,.png,.gif,.bmp,.JPG,.JPEG,.PNG,.GIF,.BMP)</param>
        /// <returns></returns>
        public static bool ValidType(string testType, string listType)
        {
            testType = "." + testType;
            testType = testType.Substring(testType.LastIndexOf(".", StringComparison.Ordinal));
            testType = "," + testType + ",";
            return listType.IndexOf(testType, StringComparison.Ordinal) > -1;
        }

        #endregion Kiểm tra đuôi ảnh

        #region Xử lý ảnh: thu nhỏ, đóng dấu

        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        private static Dictionary<string, ImageCodecInfo> _encoders;

        /// <summary>
        /// A quick lookup for getting image encoders
        /// </summary>
        public static Dictionary<string, ImageCodecInfo> Encoders
        {
            //get accessor that creates the dictionary on demand
            get
            {
                //if the quick lookup isn't initialised, initialise it
                if (_encoders == null)
                {
                    _encoders = new Dictionary<string, ImageCodecInfo>();
                }

                //if there are no codecs, try loading them
                if (_encoders.Count != 0) return _encoders;
                //get all the codecs
                foreach (var codec in ImageCodecInfo.GetImageEncoders())
                {
                    //add each codec to the quick lookup
                    _encoders.Add(codec.MimeType.ToLower(), codec);
                }

                //return the lookup
                return _encoders;
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            //a holder for the result
            var result = new Bitmap(width, height);
            // set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (var graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            //return the resulting bitmap
            return result;
        }

        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path">Path to which the image would be saved.</param>
        /// <param name="image"></param>
        /// <param name="quality">An integer from 0 to 100, with 100 being the 
        /// highest quality</param> 
        /// <exception cref="ArgumentOutOfRangeException">
        /// An invalid value was entered for image quality.
        /// </exception>
        public static void SaveJpeg(string path, Image image, int quality)
        {
            //ensure the quality is within the correct range
            if (quality < 0 || quality > 100)
            {
                //create the error message
                var error = string.Format("Jpeg image quality must be between 0 and 100, with 100 being the highest quality.  A value of {0} was specified.", quality);
                //throw a helpful exception
                throw new ArgumentOutOfRangeException(error);
            }

            //create an encoder parameter for the image quality
            var qualityParam = new EncoderParameter(Encoder.Quality, quality);
            //get the jpeg codec
            var jpegCodec = GetEncoderInfo(GetMimeType(path.Substring(path.LastIndexOf(".", StringComparison.Ordinal))));

            //create a collection of all parameters that we will pass to the encoder
            var encoderParams = new EncoderParameters(1);
            //set the quality parameter for the codec
            encoderParams.Param[0] = qualityParam;
            //save the image using the codec and the parameters
            image.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            //do a case insensitive search for the mime type
            var lookupKey = mimeType.ToLower();

            //the codec to return, default to null
            ImageCodecInfo foundCodec = null;

            //if we have the encoder, get it to return
            if (Encoders.ContainsKey(lookupKey))
            {
                //pull the codec from the lookup
                foundCodec = Encoders[lookupKey];
            }

            return foundCodec;
        }

        /// <summary>
        /// Thay đổi kích thước ảnh. Chú ý: ảnh sẽ ko bị méo khi đổi kích thước, hệ thống tự động tính toán để thay đổi theo width hoặc height sau đó tự tính chiều còn lại.
        /// </summary>
        /// <param name="sourceFile">Đường dẫn tuyệt đối tới tệp ảnh nguồn: vd: Request.PhysicalApplicationPath+"pic/news/pic1.jpg"</param>
        /// <param name="destFile">Đường dẫn tuyệt đối tới tệp ảnh đích, bỏ trống nếu muốn ghi đè lên ảnh nguồn"</param>
        /// <param name="newWidth">Chiều rộng mới</param>
        /// <param name="newHeight">Chiều cao mới.</param>
        public static void ResizeImage(string sourceFile, string destFile, string newWidth, string newHeight)
        {
            if (destFile.Length < 1) destFile = sourceFile;
            var sourceImage = Image.FromFile(sourceFile);
            var width = 0;
            var height = 0;
            try
            {
                width = int.Parse(newWidth);
            }
            catch
            {
                width = sourceImage.Width;
            }
            try
            {
                height = int.Parse(newHeight);
            }
            catch (Exception)
            {
                height = sourceImage.Height;
            }

            #region Tính toán width,height mới sao cho vẫn đảm bảo tỷ lệ ảnh

            if (sourceImage.Width > width || sourceImage.Height > height)
            {
                if ((double)sourceImage.Width / sourceImage.Height > (double)width / height) height = (int)(sourceImage.Height * (double)width / sourceImage.Width);
                else width = (int)(sourceImage.Width * (double)height / sourceImage.Height);
            }
            else
            {
                height = sourceImage.Height;
                width = sourceImage.Width;
            }

            #endregion

            var saveImage = ResizeImage(sourceImage, width, height);

            sourceImage.Dispose();
            SaveJpeg(destFile, saveImage, 100);
        }

        /// <summary>
        /// Đóng dấu ảnh, tệp ảnh gif sẽ ko bị đóng dấu để ko làm mất hiệu ứng động của ảnh
        /// </summary>
        /// <param name="tepNguon">Đường dẫn tuyệt đối tới tệp ảnh nền</param>
        /// <param name="tepConDau">Đường dẫn tuyệt đối tới tệp ảnh nền làm con dấu</param>
        /// <param name="viTri">Vị trí đóng dấu(0:giữa-giữa, 1:trên-trái, 2:trên-phải, 3:dưới-trái, 4:dưới-phải)</param>
        /// <param name="leNgang">Khoảng cách từ con dấu tới lề ngang</param>
        /// <param name="leDoc">Khoảng cách từ con dấu tới lề dọc</param>
        /// <param name="tyLeConDau">Tỷ lệ ảnh con dấu/ảnh nền. Bỏ trống nếu không muốn co giãn ảnh con dấu</param>
        /// <param name="trongSuot">Nhập từ 0-100. Bỏ trống nếu muốn để nguyên độ trong suốt (Đã hủy tính năng này, nếu muốn trong suốt -> làm ảnh dấu .png)</param>
        public static void CreateWatermark(string tepNguon, string tepConDau, string viTri, string leNgang, string leDoc, string tyLeConDau, string trongSuot)
        {
            try
            {
                var fileEx = tepNguon.Substring(tepNguon.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                if (fileEx == ".gif") return;

                var image = Image.FromFile(tepNguon);
                var logoImage = Image.FromFile(tepConDau);

                //a holder for the result
                var result = new Bitmap(image.Width, image.Height);
                // set the resolutions the same to avoid cropping due to resolution differences
                result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                //use a graphics object to draw the resized image into the bitmap
                using (var graphics = Graphics.FromImage(result))
                {
                    //set the resize quality modes to high quality
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //draw the image into the target bitmap
                    graphics.DrawImage(image, 0, 0, result.Width, result.Height);

                    var logoWidth = logoImage.Width;
                    var logoHeight = logoImage.Height;

                    var x = 0;
                    var y = 0;
                    try
                    {
                        x = int.Parse(leDoc);
                        y = int.Parse(leNgang);
                    }
                    catch
                    {
                        //
                    }

                    #region Giãn lại tỷ lệ con dấu

                    var tyLe = 0.0;
                    try
                    {
                        tyLe = double.Parse(tyLeConDau) / 100;
                    }
                    catch
                    {
                        //
                    }
                    if (tyLe > 0)
                    {
                        logoWidth = (int)(image.Width * tyLe);
                        logoHeight = (int)(logoImage.Height * (double)logoWidth / logoImage.Width);
                    }
                    if (logoWidth >= image.Width)
                    {

                        logoWidth = (int)(image.Width - x * 2) - 100;
                        logoHeight = (int)(logoImage.Height * (double)logoWidth / logoImage.Width);
                    }
                    if (logoHeight >= image.Height)
                    {
                        logoHeight = image.Height - y * 2;
                        logoWidth = (int)(logoImage.Width * (double)logoHeight / logoImage.Height);
                    }

                    #endregion

                    #region Độ trong suốt con dấu

                    //int transparency = 255;
                    //try
                    //{
                    //    transparency = int.Parse(trongSuot);
                    //    if (transparency > 100) transparency = 100;
                    //    if (transparency < 0) transparency = 0;
                    //    transparency = transparency * 255 / 100;
                    //}
                    //catch { }
                    //Bitmap bitmapImage = logoImage;
                    ////set transparency
                    //for (int i = 0; i < bitmapImage.Width; i++)
                    //    for (int j = 0; j < bitmapImage.Height; j++)
                    //    {
                    //        Color logoImagePixel = bitmapImage.GetPixel(i, j);
                    //        if (logoImagePixel.A != 0)
                    //            logoImage.SetPixel(i, j, Color.FromArgb(transparency, logoImagePixel));
                    //    }

                    #endregion

                    #region Vị trí đóng dấu

                    //0:center, 1: top-left, 2: top-right, 3: bottom-left, 4: bottom-right
                    switch (viTri)
                    {
                        case "0":
                            x = (image.Width - logoWidth) / 2 - x;
                            y = (image.Height - logoHeight) / 2 - y;
                            break;
                        case "1":
                            break;
                        case "2":
                            x = image.Width - logoWidth - x;
                            break;
                        case "3":
                            y = image.Height - logoHeight - y;
                            break;
                        default:
                            x = image.Width - logoWidth - x;
                            y = image.Height - logoHeight - y;
                            break;
                    }

                    #endregion

                    graphics.DrawImage(logoImage, x, y, logoWidth, logoHeight);
                }
                image.Dispose();
                logoImage.Dispose();

                //Tra ve doi tuong result bitmap   
                SaveJpeg(tepNguon, result, 100);

                //Dispose objects                        
                result.Dispose();

            }
            catch (Exception)
            {
                //
            }
        }

        #endregion

        #region Lưu dữ liệu dạng base64 vào bảng ImagesBackup

        private static readonly IDictionary<string, string> Mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            #region Big freaking list of mime types
            // combination of values from Windows 7 Registry and 
            // from C:\Windows\System32\inetsrv\config\applicationHost.config
            // some added, including .7z and .dat
            {".323", "text/h323"},
            {".3g2", "video/3gpp2"},
            {".3gp", "video/3gpp"},
            {".3gp2", "video/3gpp2"},
            {".3gpp", "video/3gpp"},
            {".7z", "application/x-7z-compressed"},
            {".aa", "audio/audible"},
            {".AAC", "audio/aac"},
            {".aaf", "application/octet-stream"},
            {".aax", "audio/vnd.audible.aax"},
            {".ac3", "audio/ac3"},
            {".aca", "application/octet-stream"},
            {".accda", "application/msaccess.addin"},
            {".accdb", "application/msaccess"},
            {".accdc", "application/msaccess.cab"},
            {".accde", "application/msaccess"},
            {".accdr", "application/msaccess.runtime"},
            {".accdt", "application/msaccess"},
            {".accdw", "application/msaccess.webapplication"},
            {".accft", "application/msaccess.ftemplate"},
            {".acx", "application/internet-property-stream"},
            {".AddIn", "text/xml"},
            {".ade", "application/msaccess"},
            {".adobebridge", "application/x-bridge-url"},
            {".adp", "application/msaccess"},
            {".ADT", "audio/vnd.dlna.adts"},
            {".ADTS", "audio/aac"},
            {".afm", "application/octet-stream"},
            {".ai", "application/postscript"},
            {".aif", "audio/x-aiff"},
            {".aifc", "audio/aiff"},
            {".aiff", "audio/aiff"},
            {".air", "application/vnd.adobe.air-application-installer-package+zip"},
            {".amc", "application/x-mpeg"},
            {".application", "application/x-ms-application"},
            {".art", "image/x-jg"},
            {".asa", "application/xml"},
            {".asax", "application/xml"},
            {".ascx", "application/xml"},
            {".asd", "application/octet-stream"},
            {".asf", "video/x-ms-asf"},
            {".asi", "application/octet-stream"},
            {".asm", "text/plain"},
            {".asmx", "application/xml"},
            {".aspx", "application/xml"},
            {".asr", "video/x-ms-asf"},
            {".asx", "video/x-ms-asf"},
            {".atom", "application/atom+xml"},
            {".au", "audio/basic"},
            {".avi", "video/x-msvideo"},
            {".axs", "application/olescript"},
            {".bas", "text/plain"},
            {".bcpio", "application/x-bcpio"},
            {".bin", "application/octet-stream"},
            {".bmp", "image/bmp"},
            {".c", "text/plain"},
            {".cab", "application/octet-stream"},
            {".caf", "audio/x-caf"},
            {".calx", "application/vnd.ms-office.calx"},
            {".cat", "application/vnd.ms-pki.seccat"},
            {".cc", "text/plain"},
            {".cd", "text/plain"},
            {".cdda", "audio/aiff"},
            {".cdf", "application/x-cdf"},
            {".cer", "application/x-x509-ca-cert"},
            {".chm", "application/octet-stream"},
            {".class", "application/x-java-applet"},
            {".clp", "application/x-msclip"},
            {".cmx", "image/x-cmx"},
            {".cnf", "text/plain"},
            {".cod", "image/cis-cod"},
            {".config", "application/xml"},
            {".contact", "text/x-ms-contact"},
            {".coverage", "application/xml"},
            {".cpio", "application/x-cpio"},
            {".cpp", "text/plain"},
            {".crd", "application/x-mscardfile"},
            {".crl", "application/pkix-crl"},
            {".crt", "application/x-x509-ca-cert"},
            {".cs", "text/plain"},
            {".csdproj", "text/plain"},
            {".csh", "application/x-csh"},
            {".csproj", "text/plain"},
            {".css", "text/css"},
            {".csv", "text/csv"},
            {".cur", "application/octet-stream"},
            {".cxx", "text/plain"},
            {".dat", "application/octet-stream"},
            {".datasource", "application/xml"},
            {".dbproj", "text/plain"},
            {".dcr", "application/x-director"},
            {".def", "text/plain"},
            {".deploy", "application/octet-stream"},
            {".der", "application/x-x509-ca-cert"},
            {".dgml", "application/xml"},
            {".dib", "image/bmp"},
            {".dif", "video/x-dv"},
            {".dir", "application/x-director"},
            {".disco", "text/xml"},
            {".dll", "application/x-msdownload"},
            {".dll.config", "text/xml"},
            {".dlm", "text/dlm"},
            {".doc", "application/msword"},
            {".docm", "application/vnd.ms-word.document.macroEnabled.12"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".dot", "application/msword"},
            {".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
            {".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {".dsp", "application/octet-stream"},
            {".dsw", "text/plain"},
            {".dtd", "text/xml"},
            {".dtsConfig", "text/xml"},
            {".dv", "video/x-dv"},
            {".dvi", "application/x-dvi"},
            {".dwf", "drawing/x-dwf"},
            {".dwp", "application/octet-stream"},
            {".dxr", "application/x-director"},
            {".eml", "message/rfc822"},
            {".emz", "application/octet-stream"},
            {".eot", "application/octet-stream"},
            {".eps", "application/postscript"},
            {".etl", "application/etl"},
            {".etx", "text/x-setext"},
            {".evy", "application/envoy"},
            {".exe", "application/octet-stream"},
            {".exe.config", "text/xml"},
            {".fdf", "application/vnd.fdf"},
            {".fif", "application/fractals"},
            {".filters", "Application/xml"},
            {".fla", "application/octet-stream"},
            {".flr", "x-world/x-vrml"},
            {".flv", "video/x-flv"},
            {".fsscript", "application/fsharp-script"},
            {".fsx", "application/fsharp-script"},
            {".generictest", "application/xml"},
            {".gif", "image/gif"},
            {".group", "text/x-ms-group"},
            {".gsm", "audio/x-gsm"},
            {".gtar", "application/x-gtar"},
            {".gz", "application/x-gzip"},
            {".h", "text/plain"},
            {".hdf", "application/x-hdf"},
            {".hdml", "text/x-hdml"},
            {".hhc", "application/x-oleobject"},
            {".hhk", "application/octet-stream"},
            {".hhp", "application/octet-stream"},
            {".hlp", "application/winhlp"},
            {".hpp", "text/plain"},
            {".hqx", "application/mac-binhex40"},
            {".hta", "application/hta"},
            {".htc", "text/x-component"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".htt", "text/webviewhtml"},
            {".hxa", "application/xml"},
            {".hxc", "application/xml"},
            {".hxd", "application/octet-stream"},
            {".hxe", "application/xml"},
            {".hxf", "application/xml"},
            {".hxh", "application/octet-stream"},
            {".hxi", "application/octet-stream"},
            {".hxk", "application/xml"},
            {".hxq", "application/octet-stream"},
            {".hxr", "application/octet-stream"},
            {".hxs", "application/octet-stream"},
            {".hxt", "text/html"},
            {".hxv", "application/xml"},
            {".hxw", "application/octet-stream"},
            {".hxx", "text/plain"},
            {".i", "text/plain"},
            {".ico", "image/x-icon"},
            {".ics", "application/octet-stream"},
            {".idl", "text/plain"},
            {".ief", "image/ief"},
            {".iii", "application/x-iphone"},
            {".inc", "text/plain"},
            {".inf", "application/octet-stream"},
            {".inl", "text/plain"},
            {".ins", "application/x-internet-signup"},
            {".ipa", "application/x-itunes-ipa"},
            {".ipg", "application/x-itunes-ipg"},
            {".ipproj", "text/plain"},
            {".ipsw", "application/x-itunes-ipsw"},
            {".iqy", "text/x-ms-iqy"},
            {".isp", "application/x-internet-signup"},
            {".ite", "application/x-itunes-ite"},
            {".itlp", "application/x-itunes-itlp"},
            {".itms", "application/x-itunes-itms"},
            {".itpc", "application/x-itunes-itpc"},
            {".IVF", "video/x-ivf"},
            {".jar", "application/java-archive"},
            {".java", "application/octet-stream"},
            {".jck", "application/liquidmotion"},
            {".jcz", "application/liquidmotion"},
            {".jfif", "image/pjpeg"},
            {".jnlp", "application/x-java-jnlp-file"},
            {".jpb", "application/octet-stream"},
            {".jpe", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".jsx", "text/jscript"},
            {".jsxbin", "text/plain"},
            {".latex", "application/x-latex"},
            {".library-ms", "application/windows-library+xml"},
            {".lit", "application/x-ms-reader"},
            {".loadtest", "application/xml"},
            {".lpk", "application/octet-stream"},
            {".lsf", "video/x-la-asf"},
            {".lst", "text/plain"},
            {".lsx", "video/x-la-asf"},
            {".lzh", "application/octet-stream"},
            {".m13", "application/x-msmediaview"},
            {".m14", "application/x-msmediaview"},
            {".m1v", "video/mpeg"},
            {".m2t", "video/vnd.dlna.mpeg-tts"},
            {".m2ts", "video/vnd.dlna.mpeg-tts"},
            {".m2v", "video/mpeg"},
            {".m3u", "audio/x-mpegurl"},
            {".m3u8", "audio/x-mpegurl"},
            {".m4a", "audio/m4a"},
            {".m4b", "audio/m4b"},
            {".m4p", "audio/m4p"},
            {".m4r", "audio/x-m4r"},
            {".m4v", "video/x-m4v"},
            {".mac", "image/x-macpaint"},
            {".mak", "text/plain"},
            {".man", "application/x-troff-man"},
            {".manifest", "application/x-ms-manifest"},
            {".map", "text/plain"},
            {".master", "application/xml"},
            {".mda", "application/msaccess"},
            {".mdb", "application/x-msaccess"},
            {".mde", "application/msaccess"},
            {".mdp", "application/octet-stream"},
            {".me", "application/x-troff-me"},
            {".mfp", "application/x-shockwave-flash"},
            {".mht", "message/rfc822"},
            {".mhtml", "message/rfc822"},
            {".mid", "audio/mid"},
            {".midi", "audio/mid"},
            {".mix", "application/octet-stream"},
            {".mk", "text/plain"},
            {".mmf", "application/x-smaf"},
            {".mno", "text/xml"},
            {".mny", "application/x-msmoney"},
            {".mod", "video/mpeg"},
            {".mov", "video/quicktime"},
            {".movie", "video/x-sgi-movie"},
            {".mp2", "video/mpeg"},
            {".mp2v", "video/mpeg"},
            {".mp3", "audio/mpeg"},
            {".mp4", "video/mp4"},
            {".mp4v", "video/mp4"},
            {".mpa", "video/mpeg"},
            {".mpe", "video/mpeg"},
            {".mpeg", "video/mpeg"},
            {".mpf", "application/vnd.ms-mediapackage"},
            {".mpg", "video/mpeg"},
            {".mpp", "application/vnd.ms-project"},
            {".mpv2", "video/mpeg"},
            {".mqv", "video/quicktime"},
            {".ms", "application/x-troff-ms"},
            {".msi", "application/octet-stream"},
            {".mso", "application/octet-stream"},
            {".mts", "video/vnd.dlna.mpeg-tts"},
            {".mtx", "application/xml"},
            {".mvb", "application/x-msmediaview"},
            {".mvc", "application/x-miva-compiled"},
            {".mxp", "application/x-mmxp"},
            {".nc", "application/x-netcdf"},
            {".nsc", "video/x-ms-asf"},
            {".nws", "message/rfc822"},
            {".ocx", "application/octet-stream"},
            {".oda", "application/oda"},
            {".odc", "text/x-ms-odc"},
            {".odh", "text/plain"},
            {".odl", "text/plain"},
            {".odp", "application/vnd.oasis.opendocument.presentation"},
            {".ods", "application/oleobject"},
            {".odt", "application/vnd.oasis.opendocument.text"},
            {".one", "application/onenote"},
            {".onea", "application/onenote"},
            {".onepkg", "application/onenote"},
            {".onetmp", "application/onenote"},
            {".onetoc", "application/onenote"},
            {".onetoc2", "application/onenote"},
            {".orderedtest", "application/xml"},
            {".osdx", "application/opensearchdescription+xml"},
            {".p10", "application/pkcs10"},
            {".p12", "application/x-pkcs12"},
            {".p7b", "application/x-pkcs7-certificates"},
            {".p7c", "application/pkcs7-mime"},
            {".p7m", "application/pkcs7-mime"},
            {".p7r", "application/x-pkcs7-certreqresp"},
            {".p7s", "application/pkcs7-signature"},
            {".pbm", "image/x-portable-bitmap"},
            {".pcast", "application/x-podcast"},
            {".pct", "image/pict"},
            {".pcx", "application/octet-stream"},
            {".pcz", "application/octet-stream"},
            {".pdf", "application/pdf"},
            {".pfb", "application/octet-stream"},
            {".pfm", "application/octet-stream"},
            {".pfx", "application/x-pkcs12"},
            {".pgm", "image/x-portable-graymap"},
            {".pic", "image/pict"},
            {".pict", "image/pict"},
            {".pkgdef", "text/plain"},
            {".pkgundef", "text/plain"},
            {".pko", "application/vnd.ms-pki.pko"},
            {".pls", "audio/scpls"},
            {".pma", "application/x-perfmon"},
            {".pmc", "application/x-perfmon"},
            {".pml", "application/x-perfmon"},
            {".pmr", "application/x-perfmon"},
            {".pmw", "application/x-perfmon"},
            {".png", "image/png"},
            {".pnm", "image/x-portable-anymap"},
            {".pnt", "image/x-macpaint"},
            {".pntg", "image/x-macpaint"},
            {".pnz", "image/png"},
            {".pot", "application/vnd.ms-powerpoint"},
            {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"},
            {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
            {".ppa", "application/vnd.ms-powerpoint"},
            {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
            {".ppm", "image/x-portable-pixmap"},
            {".pps", "application/vnd.ms-powerpoint"},
            {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
            {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {".ppt", "application/vnd.ms-powerpoint"},
            {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".prf", "application/pics-rules"},
            {".prm", "application/octet-stream"},
            {".prx", "application/octet-stream"},
            {".ps", "application/postscript"},
            {".psc1", "application/PowerShell"},
            {".psd", "application/octet-stream"},
            {".psess", "application/xml"},
            {".psm", "application/octet-stream"},
            {".psp", "application/octet-stream"},
            {".pub", "application/x-mspublisher"},
            {".pwz", "application/vnd.ms-powerpoint"},
            {".qht", "text/x-html-insertion"},
            {".qhtm", "text/x-html-insertion"},
            {".qt", "video/quicktime"},
            {".qti", "image/x-quicktime"},
            {".qtif", "image/x-quicktime"},
            {".qtl", "application/x-quicktimeplayer"},
            {".qxd", "application/octet-stream"},
            {".ra", "audio/x-pn-realaudio"},
            {".ram", "audio/x-pn-realaudio"},
            {".rar", "application/octet-stream"},
            {".ras", "image/x-cmu-raster"},
            {".rat", "application/rat-file"},
            {".rc", "text/plain"},
            {".rc2", "text/plain"},
            {".rct", "text/plain"},
            {".rdlc", "application/xml"},
            {".resx", "application/xml"},
            {".rf", "image/vnd.rn-realflash"},
            {".rgb", "image/x-rgb"},
            {".rgs", "text/plain"},
            {".rm", "application/vnd.rn-realmedia"},
            {".rmi", "audio/mid"},
            {".rmp", "application/vnd.rn-rn_music_package"},
            {".roff", "application/x-troff"},
            {".rpm", "audio/x-pn-realaudio-plugin"},
            {".rqy", "text/x-ms-rqy"},
            {".rtf", "application/rtf"},
            {".rtx", "text/richtext"},
            {".ruleset", "application/xml"},
            {".s", "text/plain"},
            {".safariextz", "application/x-safari-safariextz"},
            {".scd", "application/x-msschedule"},
            {".sct", "text/scriptlet"},
            {".sd2", "audio/x-sd2"},
            {".sdp", "application/sdp"},
            {".sea", "application/octet-stream"},
            {".searchConnector-ms", "application/windows-search-connector+xml"},
            {".setpay", "application/set-payment-initiation"},
            {".setreg", "application/set-registration-initiation"},
            {".settings", "application/xml"},
            {".sgimb", "application/x-sgimb"},
            {".sgml", "text/sgml"},
            {".sh", "application/x-sh"},
            {".shar", "application/x-shar"},
            {".shtml", "text/html"},
            {".sit", "application/x-stuffit"},
            {".sitemap", "application/xml"},
            {".skin", "application/xml"},
            {".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12"},
            {".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide"},
            {".slk", "application/vnd.ms-excel"},
            {".sln", "text/plain"},
            {".slupkg-ms", "application/x-ms-license"},
            {".smd", "audio/x-smd"},
            {".smi", "application/octet-stream"},
            {".smx", "audio/x-smd"},
            {".smz", "audio/x-smd"},
            {".snd", "audio/basic"},
            {".snippet", "application/xml"},
            {".snp", "application/octet-stream"},
            {".sol", "text/plain"},
            {".sor", "text/plain"},
            {".spc", "application/x-pkcs7-certificates"},
            {".spl", "application/futuresplash"},
            {".src", "application/x-wais-source"},
            {".srf", "text/plain"},
            {".SSISDeploymentManifest", "text/xml"},
            {".ssm", "application/streamingmedia"},
            {".sst", "application/vnd.ms-pki.certstore"},
            {".stl", "application/vnd.ms-pki.stl"},
            {".sv4cpio", "application/x-sv4cpio"},
            {".sv4crc", "application/x-sv4crc"},
            {".svc", "application/xml"},
            {".swf", "application/x-shockwave-flash"},
            {".t", "application/x-troff"},
            {".tar", "application/x-tar"},
            {".tcl", "application/x-tcl"},
            {".testrunconfig", "application/xml"},
            {".testsettings", "application/xml"},
            {".tex", "application/x-tex"},
            {".texi", "application/x-texinfo"},
            {".texinfo", "application/x-texinfo"},
            {".tgz", "application/x-compressed"},
            {".thmx", "application/vnd.ms-officetheme"},
            {".thn", "application/octet-stream"},
            {".tif", "image/tiff"},
            {".tiff", "image/tiff"},
            {".tlh", "text/plain"},
            {".tli", "text/plain"},
            {".toc", "application/octet-stream"},
            {".tr", "application/x-troff"},
            {".trm", "application/x-msterminal"},
            {".trx", "application/xml"},
            {".ts", "video/vnd.dlna.mpeg-tts"},
            {".tsv", "text/tab-separated-values"},
            {".ttf", "application/octet-stream"},
            {".tts", "video/vnd.dlna.mpeg-tts"},
            {".txt", "text/plain"},
            {".u32", "application/octet-stream"},
            {".uls", "text/iuls"},
            {".user", "text/plain"},
            {".ustar", "application/x-ustar"},
            {".vb", "text/plain"},
            {".vbdproj", "text/plain"},
            {".vbk", "video/mpeg"},
            {".vbproj", "text/plain"},
            {".vbs", "text/vbscript"},
            {".vcf", "text/x-vcard"},
            {".vcproj", "Application/xml"},
            {".vcs", "text/plain"},
            {".vcxproj", "Application/xml"},
            {".vddproj", "text/plain"},
            {".vdp", "text/plain"},
            {".vdproj", "text/plain"},
            {".vdx", "application/vnd.ms-visio.viewer"},
            {".vml", "text/xml"},
            {".vscontent", "application/xml"},
            {".vsct", "text/xml"},
            {".vsd", "application/vnd.visio"},
            {".vsi", "application/ms-vsi"},
            {".vsix", "application/vsix"},
            {".vsixlangpack", "text/xml"},
            {".vsixmanifest", "text/xml"},
            {".vsmdi", "application/xml"},
            {".vspscc", "text/plain"},
            {".vss", "application/vnd.visio"},
            {".vsscc", "text/plain"},
            {".vssettings", "text/xml"},
            {".vssscc", "text/plain"},
            {".vst", "application/vnd.visio"},
            {".vstemplate", "text/xml"},
            {".vsto", "application/x-ms-vsto"},
            {".vsw", "application/vnd.visio"},
            {".vsx", "application/vnd.visio"},
            {".vtx", "application/vnd.visio"},
            {".wav", "audio/wav"},
            {".wave", "audio/wav"},
            {".wax", "audio/x-ms-wax"},
            {".wbk", "application/msword"},
            {".wbmp", "image/vnd.wap.wbmp"},
            {".wcm", "application/vnd.ms-works"},
            {".wdb", "application/vnd.ms-works"},
            {".wdp", "image/vnd.ms-photo"},
            {".webarchive", "application/x-safari-webarchive"},
            {".webtest", "application/xml"},
            {".wiq", "application/xml"},
            {".wiz", "application/msword"},
            {".wks", "application/vnd.ms-works"},
            {".WLMP", "application/wlmoviemaker"},
            {".wlpginstall", "application/x-wlpg-detect"},
            {".wlpginstall3", "application/x-wlpg3-detect"},
            {".wm", "video/x-ms-wm"},
            {".wma", "audio/x-ms-wma"},
            {".wmd", "application/x-ms-wmd"},
            {".wmf", "application/x-msmetafile"},
            {".wml", "text/vnd.wap.wml"},
            {".wmlc", "application/vnd.wap.wmlc"},
            {".wmls", "text/vnd.wap.wmlscript"},
            {".wmlsc", "application/vnd.wap.wmlscriptc"},
            {".wmp", "video/x-ms-wmp"},
            {".wmv", "video/x-ms-wmv"},
            {".wmx", "video/x-ms-wmx"},
            {".wmz", "application/x-ms-wmz"},
            {".wpl", "application/vnd.ms-wpl"},
            {".wps", "application/vnd.ms-works"},
            {".wri", "application/x-mswrite"},
            {".wrl", "x-world/x-vrml"},
            {".wrz", "x-world/x-vrml"},
            {".wsc", "text/scriptlet"},
            {".wsdl", "text/xml"},
            {".wvx", "video/x-ms-wvx"},
            {".x", "application/directx"},
            {".xaf", "x-world/x-vrml"},
            {".xaml", "application/xaml+xml"},
            {".xap", "application/x-silverlight-app"},
            {".xbap", "application/x-ms-xbap"},
            {".xbm", "image/x-xbitmap"},
            {".xdr", "text/plain"},
            {".xht", "application/xhtml+xml"},
            {".xhtml", "application/xhtml+xml"},
            {".xla", "application/vnd.ms-excel"},
            {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
            {".xlc", "application/vnd.ms-excel"},
            {".xld", "application/vnd.ms-excel"},
            {".xlk", "application/vnd.ms-excel"},
            {".xll", "application/vnd.ms-excel"},
            {".xlm", "application/vnd.ms-excel"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
            {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".xlt", "application/vnd.ms-excel"},
            {".xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
            {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
            {".xlw", "application/vnd.ms-excel"},
            {".xml", "text/xml"},
            {".xmta", "application/xml"},
            {".xof", "x-world/x-vrml"},
            {".XOML", "text/plain"},
            {".xpm", "image/x-xpixmap"},
            {".xps", "application/vnd.ms-xpsdocument"},
            {".xrm-ms", "text/xml"},
            {".xsc", "application/xml"},
            {".xsd", "text/xml"},
            {".xsf", "text/xml"},
            {".xsl", "text/xml"},
            {".xslt", "text/xml"},
            {".xsn", "application/octet-stream"},
            {".xss", "application/xml"},
            {".xtp", "application/octet-stream"},
            {".xwd", "image/x-xwindowdump"},
            {".z", "application/x-compress"},
            {".zip", "application/x-zip-compressed"},

            #endregion
        };

        public static string GetMimeType(string extension)
        {
            if (extension == null) throw new ArgumentNullException("extension");

            if (!extension.StartsWith(".")) extension = "." + extension;

            string mime;

            return Mappings.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }
        #endregion
    }
}