using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using DeepAncestry.Models;
using DeepAncestry.Classes;

namespace DeepAncestry.Controllers
{
    public class SearchController : Controller
    {
        private List<Places> _places;
        private List<People> _people;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            string file = Server.MapPath(@"~/App_Data/data_large.json");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = int.MaxValue;
            dynamic jsonData = ser.Deserialize(json, typeof(object));
            var data = ser.Serialize(jsonData);
            data = ser.Deserialize<JsonData>(data);

            _places = data.Places;

            _people = data.People;

        }

        // GET: Search
        public ActionResult Index(SearchViewModel model)
        {
            var filters = new List<Expression<Func<People, bool>>>();

            if (!String.IsNullOrEmpty(model.Name))
                filters.Add(x => x.Name != null && x.Name.ToLower().Contains(model.Name.ToLower()));
            if (model.Male && !model.Female)
                filters.Add(x => x.Gender == "M");
            if (model.Female && !model.Male)
                filters.Add(x => x.Gender == "F");

            var query = _people.AsQueryable();
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
            var searchResult = query.Take(10).ToList();

            var mappedSearchResults = new List<PeopleView>();

            foreach (People people in searchResult)
            {
                mappedSearchResults.Add(Map(people));
            }

            model.SearchResult = mappedSearchResults;
            return View(model);
        }

        public ActionResult AdvancedSearch(SearchViewModel model)
        {
            return View("AdvancedSearch", model);
        }

        public PeopleView Map(People people)
        {
            char[] testChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var birthPlace = _places.Where(x => x.Id == people.Place_Id).FirstOrDefault();
            var birthPlaceName = "";

            if (birthPlace != null)
            {
                var postCodeIndex = birthPlace.Name.IndexOfAny(testChars);
                if (postCodeIndex > 0)
                {
                    birthPlaceName = birthPlace.Name.Substring(0, postCodeIndex - 1);
                }
                else
                    birthPlaceName = birthPlace.Name;
            }

            var peopleView = new PeopleView
            {
                ID = people.Id,
                Name = people.Name,
                Gender = people.Gender == "F" ? "Female" : "Male",
                BirthPlace = birthPlaceName
            };

            return peopleView;
        }
    }
}