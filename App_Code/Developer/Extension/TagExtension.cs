using System;
using System.Linq;

/// <summary>
/// Summary description for TagExtension
/// </summary>
public class TagExtension
{
    /// <summary>
    /// Tác các thẻ tag theo dấu ,
    /// </summary>
    /// <param name="listag"></param>
    /// <returns></returns>
    public static string GetTag(string listag)
    {
        var s = "";
        s = listag.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries).Where(tag => tag.Trim().Length > 0).Aggregate(s, (current, tag) => current + "<a rel='tag' href='/?rewrite=tin-tuc&page=tag&tag="+ tag.Trim() + "'>" + tag.Trim() + "</a>, ");
        if (s.EndsWith(", ")) s = s.Remove(s.Length - ", ".Length);
        return s;
    }

    /// <summary>
    /// Tác các thẻ tag theo dấu ,
    /// </summary>
    /// <param name="listag"></param>
    /// <param name="rewrite"></param>
    /// <returns></returns>
    public static string GetTag(string listag, string rewrite)
    {
        var s = "";
        s = listag.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(tag => tag.Trim().Length > 0).Aggregate(s, (current, tag) => current + "<a rel='tag' href='/?rewrite="+ rewrite + "&page=tag&tag=" + tag.Trim() + "'>" + tag.Trim() + "</a>");
        return s;
    }
}