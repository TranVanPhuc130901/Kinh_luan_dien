using RevosJsc.AdminControl;

namespace RevosJsc.HotelControl
{
  public class Link
  {
    public static string LnkMnHotel()
    {
      return LinkAdmin.GoAdminControl("Hotel");
    }

    public static string LnkMnHotelCategory()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Category");
    }

    public static string LnkMnHotelCategoryCreate()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "CreateCategory");
    }

    public static string LnkMnHotelCategoryRec()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "RecycleCategory");
    }

    public static string LnkMnHotelItem()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Item");
    }

    public static string LnkMnHotelItemCreate()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "CreateItem");
    }

    public static string LnkMnHotelItemRec()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "RecycleItem");
    }

    public static string LnkMnHotelFilter()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Filter");
    }

    public static string LnkMnHotelFilterCreate()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "CreateFilter");
    }

    public static string LnkMnHotelFilterRec()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "RecycleFilter");
    }

    public static string LnkMnHotelProperty()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Property");
    }

    public static string LnkMnHotelPropertyCreate()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "CreateProperty");
    }

    public static string LnkMnHotelPropertyRec()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "RecycleProperty");
    }

    public static string LnkMnHotelGroupItem()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "GroupItem");
    }

    public static string LnkMnHotelGroupItemCreate()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "CreateGroupItem");
    }

    public static string LnkMnHotelGroupItemRec()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "RecycleGroupItem");
    }

    public static string LnkMnHotelGroupCategory()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "GroupCategory");
    }

    public static string LnkMnHotelGroupCategoryCreate()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "CreateGroupCategory");
    }

    public static string LnkMnHotelGroupCategoryRec()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "RecycleGroupCategory");
    }

    public static string LnkMnHotelProvider()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Provider");
    }

    public static string LnkMnHotelProviderCreate()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "CreateProvider");
    }

    public static string LnkMnHotelProviderRec()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "RecycleProvider");
    }

    public static string LnkMnHotelCart()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Cart");
    }

    public static string LnkMnHotelConfig()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Configuration");
    }

    public static string LnkMnHotelComment()
    {
      return LinkAdmin.GoAdminSubControl("Hotel", "Comment");
    }
  }
}
