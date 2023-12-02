using System;
using RevosJsc.Columns;
using RevosJsc.Database;
using RevosJsc.Extension;
using RevosJsc.TSql;

namespace Developer.Extension
{
    public class TourExtension
    {
        public static string ShowTourItinerary(string itinerary)
        {
            if (itinerary.StartsWith("text-")) return itinerary.Substring("text-".Length);
            return itinerary.StartsWith("id-") ? GetListItinerary(itinerary.Substring("id-".Length, itinerary.Length - ("id-".Length + 1))) : itinerary;
        }

        private static string GetListItinerary(string itinerary)
        {
            var s = "";
            var fields = DataExtension.GetListColumns(
                GroupsColumns.VgName,
                GroupsColumns.VgLink
                );
            var condition = DataExtension.AndConditon(
                GroupsTSql.GetByStatus("1"),
                " igId IN ("+ itinerary +") "
                );
            var dt = Groups.GetData("", fields, condition, "CHARINDEX(',' + CAST(igId as varchar) + ',', ',' + '" + itinerary + "' + ',')");
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                s += "<a target='_blank' href='"+ (UrlExtension.WebsiteUrl + dt.Rows[i][GroupsColumns.VgLink] + RewriteExtension.Extensions).ToLower() + "' title='" + dt.Rows[i][GroupsColumns.VgName] + "'>" + dt.Rows[i][GroupsColumns.VgName] +"</a> - ";
            }
            if (s.EndsWith(" - ")) s = s.Substring(0, s.Length - " - ".Length);
            return s;
        }


        /// <summary>
        /// Hiển thị thời gian tour dạng 2 ngày / 1 đêm.
        /// </summary>
        /// <param name="duration">Ví dụ duration là 2-1 thì kết quả là 2 ngày / 1 đêm</param>
        /// <returns></returns>
        public static string ShowTourDuration(string duration)
        {
            var s = "";
            try
            {
                var day = duration.Remove(duration.IndexOf("-", StringComparison.Ordinal));
                var night = duration.Substring(duration.IndexOf("-", StringComparison.Ordinal) + 1);
                s = day + (day.Equals("1") || day.Equals("01") || day.Equals("0") || day.Equals("00") ? " day " : " days ");
                s += night + (night.Equals("1") || night.Equals("01") || night.Equals("0") || night.Equals("00") ? " night" : " nights");
            }
            catch
            {
                //
            }

            return s;
        }

    }
}