using System;
using System.Collections.Generic;

#nullable disable

namespace IMBDTgBot
{
    public partial class Title
    {
        public int Id { get; set; }
        public string Tconst { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? RuntimeMinutes { get; set; }
        public string Genres { get; set; }
    }
}
