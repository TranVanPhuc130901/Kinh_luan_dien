using RevosJsc.AdminControl;

namespace RevosJsc.FAQControl
{
  public class Link
  {
    public static string LnkMnFAQ()
    {
      return LinkAdmin.GoAdminControl("FAQ");
    }

    public static string LnkMnFAQCategory()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "Category");
    }

    public static string LnkMnFAQCategoryCreate()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "CreateCategory");
    }

    public static string LnkMnFAQCategoryRec()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "RecycleCategory");
    }

    public static string LnkMnFAQItem()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "Item");
    }

    public static string LnkMnFAQItemCreate()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "CreateItem");
    }

    public static string LnkMnFAQItemRec()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "RecycleItem");
    }

    public static string LnkMnFAQGroupItem()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "GroupItem");
    }

    public static string LnkMnFAQGroupItemCreate()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "CreateGroupItem");
    }

    public static string LnkMnFAQGroupItemRec()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "RecycleGroupItem");
    }

    public static string LnkMnFAQGroupCategory()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "GroupCategory");
    }

    public static string LnkMnFAQGroupCategoryCreate()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "CreateGroupCategory");
    }

    public static string LnkMnFAQGroupCategoryRec()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "RecycleGroupCategory");
    }

    public static string LnkMnFAQProperty()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "Property");
    }

    public static string LnkMnFAQPropertyCreate()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "CreateProperty");
    }

    public static string LnkMnFAQPropertyRec()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "RecycleProperty");
    }

    public static string LnkMnFAQConfig()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "Configuration");
    }

    public static string LnkMnFAQComment()
    {
      return LinkAdmin.GoAdminSubControl("FAQ", "Comment");
    }
  }
}
