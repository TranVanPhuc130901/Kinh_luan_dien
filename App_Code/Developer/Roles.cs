using Developer.Keyword;

namespace Developer
{
    public class Roles
    {
        private readonly string[] _listvalue;
        private readonly string[] _listtext;

        public string AboutUs { get { return _listvalue[0]; } }
        public string Advertising { get { return _listvalue[1]; } }
        public string Blog { get { return _listvalue[2]; } }
        public string ContactUs { get { return _listvalue[3]; } }
        public string Cruises { get { return _listvalue[4]; } }
        public string Customer { get { return _listvalue[5]; } }
        public string Reviews { get { return _listvalue[6]; } }
        public string Destination { get { return _listvalue[7]; } }
        public string FAQ { get { return _listvalue[8]; } }
        public string FileLibrary { get { return _listvalue[9]; } }
        public string Hotel { get { return _listvalue[10]; } }
        public string Language { get { return _listvalue[11]; } }
        public string Member { get { return _listvalue[12]; } }
        public string Menu { get { return _listvalue[13]; } }
        public string News { get { return _listvalue[14]; } }
        public string PhotoAlbum { get { return _listvalue[15]; } }
        public string Product { get { return _listvalue[16]; } }
        public string Project { get { return _listvalue[17]; } }
        public string Service { get { return _listvalue[18]; } }
        public string OurTeam { get { return _listvalue[19]; } }
        public string Systemwebsite { get { return _listvalue[20]; } }
        public string Tour { get { return _listvalue[21]; } }
        public string Users { get { return _listvalue[22]; } }
        public string Video { get { return _listvalue[23]; } }
        public string Website { get { return _listvalue[24]; } }
        public string Redirect { get { return _listvalue[25]; } }
        public string[] Text { get { return _listtext; } }
        public string[] Values { get { return _listvalue; } }
        public Roles()
        {
            _listvalue = new string[26]
            {
                "0"
                ,"1"
                ,"2"
                ,"3"
                ,"4"
                ,"5"
                ,"6"
                ,"7"
                ,"8"
                ,"9"
                ,"10"
                ,"11"
                ,"12"
                ,"13"
                ,"14"
                ,"15"
                ,"16"
                ,"17"
                ,"18"
                ,"19"
                ,"20"
                ,"21"
                ,"22"
                ,"23"
                ,"24"
                ,"25"
            };
            _listtext = new string[26]
            {
                AboutUsKeyword.AboutUs //0
                ,AdvertistmentsKeyword.Advertistments1 //1
                ,BlogKeyword.Blog //2
                ,ContactKeyword.Contact //3
                ,CruisesKeyword.Cruises //4
                ,CustomerKeyword.Customer //5
                ,ReviewsKeyword.Reviews //6
                ,DestinationKeyword.Destination //7
                ,FAQKeyword.FAQ //8
                ,FileLibraryKeyword.FileLibrary //9
                ,HotelKeyword.Hotel //10
                ,"Ngôn ngữ" //11
                ,MemberKeyword.Member //12
                ,MenusKeyword.Menus //13
                ,NewsKeyword.News //14
                ,PhotoAlbumKeyword.PhotoAlbum //15
                ,ProductKeyword.Product //16
                ,ProjectKeyword.Project //17
                ,ServiceKeyword.Service //18
                ,OurTeamKeyword.OurTeam //19
                ,"Hệ thống" //20
                ,TourKeyword.Tour //21
                ,"Tài khoản" //22
                ,VideoKeyword.Video //23
                ,WebsiteKeyword.Website //24
                ,"Chuyển hướng 301" //25
            };
        }
    }
}
