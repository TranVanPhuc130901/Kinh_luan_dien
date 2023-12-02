using RevosJsc.AdminControl;

namespace RevosJsc.FileLibraryControl
{
  public class Link
  {
    public static string LnkMnFileLibrary()
    {
      return LinkAdmin.GoAdminControl("FileLibrary");
    }

    public static string LnkMnFileLibraryCategory()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "Category");
    }

    public static string LnkMnFileLibraryCategoryCreate()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "CreateCategory");
    }

    public static string LnkMnFileLibraryCategoryRec()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "RecycleCategory");
    }

    public static string LnkMnFileLibraryItem()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "Item");
    }

    public static string LnkMnFileLibraryItemCreate()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "CreateItem");
    }

    public static string LnkMnFileLibraryItemRec()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "RecycleItem");
    }

    public static string LnkMnFileLibraryGroupItem()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "GroupItem");
    }

    public static string LnkMnFileLibraryGroupItemCreate()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "CreateGroupItem");
    }

    public static string LnkMnFileLibraryGroupItemRec()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "RecycleGroupItem");
    }

    public static string LnkMnFileLibraryGroupCategory()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "GroupCategory");
    }

    public static string LnkMnFileLibraryGroupCategoryCreate()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "CreateGroupCategory");
    }

    public static string LnkMnFileLibraryGroupCategoryRec()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "RecycleGroupCategory");
    }

    public static string LnkMnFileLibraryProperty()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "Property");
    }

    public static string LnkMnFileLibraryPropertyCreate()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "CreateProperty");
    }

    public static string LnkMnFileLibraryPropertyRec()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "RecycleProperty");
    }

    public static string LnkMnFileLibraryConfig()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "Configuration");
    }

    public static string LnkMnFileLibraryComment()
    {
      return LinkAdmin.GoAdminSubControl("FileLibrary", "Comment");
    }
  }
}
