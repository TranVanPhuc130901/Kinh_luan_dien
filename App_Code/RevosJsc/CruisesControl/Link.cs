using RevosJsc.AdminControl;

namespace RevosJsc.CruisesControl
{
    public class Link
    {
        public static string LnkMnCruises()
        {
            return LinkAdmin.GoAdminControl("Cruises");
        }

        public static string LnkMnCruisesCategory()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "Category");
        }

        public static string LnkMnCruisesCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "CreateCategory");
        }

        public static string LnkMnCruisesCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "RecycleCategory");
        }

        public static string LnkMnCruisesItem()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "Item");
        }

        public static string LnkMnCruisesItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "CreateItem");
        }

        public static string LnkMnCruisesItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "RecycleItem");
        }

        public static string LnkMnCruisesGroupItem()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "GroupItem");
        }

        public static string LnkMnCruisesGroupItemCreate()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "CreateGroupItem");
        }

        public static string LnkMnCruisesGroupItemRec()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "RecycleGroupItem");
        }

        public static string LnkMnCruisesGroupCategory()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "GroupCategory");
        }

        public static string LnkMnCruisesGroupCategoryCreate()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "CreateGroupCategory");
        }

        public static string LnkMnCruisesGroupCategoryRec()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "RecycleGroupCategory");
        }

        public static string LnkMnCruisesProperty()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "Property");
        }

        public static string LnkMnCruisesPropertyCreate()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "CreateProperty");
        }

        public static string LnkMnCruisesPropertyRec()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "RecycleProperty");
        }

        public static string LnkMnCruisesFacility()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "Facility");
        }

        public static string LnkMnCruisesFacilityCreate()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "CreateFacility");
        }

        public static string LnkMnCruisesFacilityRec()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "RecycleFacility");
        }

        public static string LnkMnCruisesConfig()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "Configuration");
        }

        public static string LnkMnCruisesComment()
        {
            return LinkAdmin.GoAdminSubControl("Cruises", "Comment");
        }
    }
}
