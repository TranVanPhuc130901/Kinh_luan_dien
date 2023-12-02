using System;

namespace RevosJsc.Extension
{
    public class VideoExtension
    {
        /// <summary>
        /// Lấy mã video từ các trường hợp mã nhúng video youtube như sau        
        ///1. Link xem:  https://www.youtube.com/watch?v=YCl-0lu0vHM
        ///2. Link chia sẻ: https://youtu.be/YCl-0lu0vHM
        ///3. Mã nhúng: <iframe width="420" height="315" src="https://www.youtube.com/embed/YCl-0lu0vHM" frameborder="0" allowfullscreen></iframe>        
        /// </summary>
        /// <param name="videoCode">Mã nhúng video</param>
        /// <returns></returns>
        public static string GetVideoKey(string videoCode)
        {
            if (videoCode.IndexOf("iframe", StringComparison.Ordinal) > -1)
            {
                var i1 = videoCode.IndexOf("embed/", StringComparison.Ordinal) + "embed/".Length;
                videoCode = videoCode.Substring(i1, videoCode.IndexOf("\"", i1, StringComparison.Ordinal) - i1);
            }
            else if (videoCode.IndexOf("?v=", StringComparison.Ordinal) > -1) videoCode = videoCode.Substring(videoCode.IndexOf("?v=", StringComparison.Ordinal) + "?v=".Length);
            else videoCode = videoCode.Substring(videoCode.LastIndexOf("/", StringComparison.Ordinal) + 1);
            return videoCode;
        }

        /// <summary>
        /// Lấy ảnh đại diện của video từ youtube
        /// </summary>
        /// <param name="videoId">Mã video: vd: https://youtu.be/abc Thì mã là abc</param>
        /// <param name="cssClass"></param>
        /// <param name="imageName">Tên của ảnh, bỏ trống nếu muốn lấy ảnh mặc định . Thường một video có các ảnh theo các kích thước: 0:480x360px, 1:120x90px, 2:120x90px, 3:120x90px</param>
        /// <returns></returns>
        public static string GetYouTubeVideoImage(string videoId, string cssClass, string imageName)
        {
            //return GetYouTubeVideoImage(videoId, cssClass, imageName, videoId);
            if (imageName.Length < 1) imageName = "0";
            return "<img src=\"https://img.youtube.com/vi/" + videoId + "/" + imageName + ".jpg\" alt=\"" + videoId + "\" class=\"" + cssClass + "\"/>";
        }

        /// <summary>
        /// Lấy ảnh đại diện của video từ youtube
        /// </summary>
        /// <param name="videoId">Mã video: vd: https://youtu.be/abc Thì mã là abc</param>
        /// <param name="cssClass"></param>
        /// <param name="imageName">Tên của ảnh, bỏ trống nếu muốn lấy ảnh mặc định . Thường một video có các ảnh theo các kích thước: 0:480x360px, 1:120x90px, 2:120x90px, 3:120x90px</param>
        /// <param name="altImage">alt cho thẻ image</param>
        /// <returns></returns>
        public static string GetYouTubeVideoImage(string videoId, string cssClass, string imageName, string altImage)
        {
            if (imageName.Length < 1) imageName = "0";
            return "<img src=\"https://img.youtube.com/vi/" + videoId + "/" + imageName + ".jpg\" alt=\"" + altImage + "\" class=\"" + cssClass + "\"/>";
        }
        /// <summary>
        /// Lấy ảnh đại diện của video từ youtube
        /// </summary>
        /// <param name="videoId">Mã video: vd: https://youtu.be/abc Thì mã là abc</param>
        /// <param name="cssClass"></param>
        /// <param name="imageName">Tên của ảnh, bỏ trống nếu muốn lấy ảnh mặc định . Thường một video có các ảnh theo các kích thước: 0:480x360px, 1:120x90px, 2:120x90px, 3:120x90px</param>
        /// <param name="altImage">alt cho thẻ image</param>
        /// <param name="useDataSrc">Sử dụng data-src phục vụ lazy load</param>
        /// <returns></returns>
        public static string GetYouTubeVideoImage(string videoId, string cssClass, string imageName, string altImage, bool useDataSrc)
        {
            if (imageName.Length < 1) imageName = "0";
            return "<img src='data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==' data-src=\"https://img.youtube.com/vi/" + videoId + "/" + imageName + ".jpg\" alt=\"" + altImage + "\" class=\"" + cssClass + "\"/>";
        }

        /// <summary>
        /// Trả về thẻ iframe chứa video theo mã của video truyền vào
        /// </summary>
        /// <param name="videoId">Mã video Youtube</param>
        /// <param name="width">Độ rộng</param>
        /// <param name="height">Độ cao</param>
        /// <param name="autoPlay">true: tự động chạy</param>
        /// <returns></returns>
        public static string GetYouTubeVideoEmbedCode(string videoId, string width, string height, bool autoPlay)
        {
            var autoPlayString = "0";
            if (autoPlay) autoPlayString = "1";
            if (width.Length < 1 || height.Length < 1)
            {
                width = "640";
                height = "480";
            }
            var s = "<iframe width='" + width + "' height='" + height + "' src='https://www.youtube.com/embed/" + videoId + "?autoplay=" + autoPlayString + "&cc_load_policy=1' frameborder='0' allowfullscreen></iframe>";
            return s;
        }
    }
}