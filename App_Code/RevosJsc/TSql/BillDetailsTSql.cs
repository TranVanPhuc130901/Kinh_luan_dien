
namespace RevosJsc.TSql
{
    public class BillDetailsTSql
    {
        public static string GetByIbId(string ibId)
        {
            return " ibId = N'" + Extension.RemoveSqlInjectionChars(ibId) + "' ";
        }

        public static string GetByIbdId(string ibdId)
        {
            return " ibdId = N'" + Extension.RemoveSqlInjectionChars(ibdId) + "' ";
        }

        public static string GetByTitle(string vbdTitle)
        {
            return " vbdTitle = N'" + Extension.RemoveSqlInjectionChars(vbdTitle) + "' ";
        }

        public static string GetByQuantity(string ibdQuantity)
        {
            return " ibdQuantity = N'" + Extension.RemoveSqlInjectionChars(ibdQuantity) + "' ";
        }

        public static string GetByPriceOld(string fbdPriceOld)
        {
            return " fbdPriceOld = N'" + Extension.RemoveSqlInjectionChars(fbdPriceOld) + "' ";
        }

        public static string GetByPriceNew(string fbdPriceNew)
        {
            return " fbdPriceNew = N'" + Extension.RemoveSqlInjectionChars(fbdPriceNew) + "' ";
        }

        public static string GetByParam(string vbdParam)
        {
            return " vbdParam = N'" + Extension.RemoveSqlInjectionChars(vbdParam) + "' ";
        }
        public static string GetByDateCreated(string dbdDateCreated)
        {
            return " dbdDateCreated = N'" + Extension.RemoveSqlInjectionChars(dbdDateCreated) + "' ";
        }
        public static string GetByDateModified(string dbdDateModified)
        {
            return " dbdDateModified = N'" + Extension.RemoveSqlInjectionChars(dbdDateModified) + "' ";
        }
    }
}
