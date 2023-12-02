using System;

namespace RevosJsc.Extension
{
    public class DropDownListExtension
    {
        public static string FormatForDdl(string iLevel)
        {
            var s = "";
            int level = Convert.ToInt16(iLevel);
            if (level <= 1) return s;
            for (var i = 1; i < level; i++)
            {
                s += "...";
            }
            return s;
        }
    }
}
