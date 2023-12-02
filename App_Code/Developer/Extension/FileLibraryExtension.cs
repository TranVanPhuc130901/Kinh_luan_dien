using System;

namespace Developer.Extension
{
    /// <summary>
    /// Summary description for FileLibraryExtension
    /// </summary>
    public class FileLibraryExtension
    {
        public static bool ValidType(string testType)
        {
            const string listType = ",.pdf,.xls,.xlsx,.doc,.docx,.ppt,.pptx,.pps,.ppsx,.zip,.rar,.7zip,.txt,.xps,.gif,.pjp,.jpg,.pjpeg,.jpeg,.jfif,.png,.svgz,.svg,.bmp,.mp4,.webm,";
            testType = testType.ToLower();
            testType = "." + testType;
            testType = testType.Substring(testType.LastIndexOf(".", StringComparison.Ordinal));
            testType = "," + testType + ",";
            return listType.IndexOf(testType, StringComparison.Ordinal) > -1;
        }
    }
}