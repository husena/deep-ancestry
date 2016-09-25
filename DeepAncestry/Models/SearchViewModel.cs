using System.Collections.Generic;

namespace DeepAncestry.Models
{
    public class SearchViewModel
    {
        public string Name { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool AdvancedSearch { get; set; }
        public string Direction { get; set; }
        public List<PeopleView> SearchResult { get; set; }
    }

    public class PeopleView
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string BirthPlace { get; set; }
    }

}