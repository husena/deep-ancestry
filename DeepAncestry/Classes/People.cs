using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeepAncestry.Classes
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int? Father_Id { get; set; }
        public int? Mother_Id { get; set; }
        public int Place_Id { get; set; }
        public int Level { get; set; }
    }

    public class Places
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class JsonData
    {
        public List<Places> Places { get; set; }
        public List<People> People { get; set; }
    }

    public class PeopleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public PeopleEntity Father { get; set; }
        public PeopleEntity Mother { get; set; }
        public Places Place { get; set; }
        public int Level { get; set; }
    }

}