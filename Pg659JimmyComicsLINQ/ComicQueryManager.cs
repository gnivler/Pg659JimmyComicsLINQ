using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Pg659JimmyComicsLINQ
{
    class ComicQueryManager
    {
        public ObservableCollection<ComicQuery> AvailableQueries { get; private set; }
        public ObservableCollection<object> CurrentQueryResults { get; private set; }
        public string Title { get; set; }
        public ComicQueryManager()
        {
            UpdateAvailableQueries();
            CurrentQueryResults = new ObservableCollection<object>();
        }

        public void UpdateAvailableQueries()
        {
            AvailableQueries = new ObservableCollection<ComicQuery>
            {
                new ComicQuery("Linq makes queries easy", "A sample query", "Let's show Jimmy how flexible LINQ is", "Assets/purple_250x250.jpg"),
                new ComicQuery("Expensive comics", "Comics over $500", "Comics who value is over $500." + " Jimmy can use this to figure out which comcis are most coveted.", "Assets/captain_amazing_250x250.jpg"),
            };
        }

        public void UpdateQueryResults(ComicQuery query)
        {
            Title = query.Title;

            switch (query.Title)
            {
                case "LINQ makes queries easy":
                    LinqMakesQueriesEasy(); break;
                case "Expensive comics":
                    ExpensiveComics(); break;
            }
        }

        private void LinqMakesQueriesEasy()
        {
            int[] values = new int[] { 0, 12, 44, 36, 92, 54, 13, 8 };
            var result = from v in values
                         where v < 37
                         orderby v
                         select v;

            foreach (int i in result)
            {
                CurrentQueryResults.Add(
                    new
                        {
                            Title = i.ToString(),
                            ImagePath = "Assets/purple_250x250.jpg",
                        }
                    );
            }
        }

        private void ExpensiveComics()
        {
            IEnumerable<Comic> comics = BuildCatalog();
            Dictionary<int, decimal> values = GetPrices();

            var mostExpensive = from comic in comics
                                where values[comic.Issue] > 500
                                orderby values[comic.Issue] descending
                                select comic;

            foreach (Comic comic in mostExpensive)
            {
                CurrentQueryResults.Add(
                    new
                    {
                        Title = string.Format($"{comic.Name} is worth {values[comic.Issue]:c}"),
                        ImagePath = "Assets/captain_amazing_250x250.jpg",
                    }
                );
            }
        }

        private void CreateImageFromAssets() { }

        public static IEnumerable<Comic> BuildCatalog()
        {
            return new List<Comic>
            {
                new Comic { Name = "Johnny America vs. The Pinko", Issue = 6 },
                new Comic { Name = "Rock and Roll (limited edition)", Issue = 19 },
                new Comic { Name = "Revenge of the New Wave Freak (damaged)", Issue = 68 },
                new Comic { Name = "Black Monday", Issue = 74 },
            };
        }

        private static Dictionary<int, decimal> GetPrices()
        {
            return new Dictionary<int, decimal> {
                { 6, 3600M},
                { 19, 500M},
                { 68, 250M},
                { 74, 75M},
            };
        }
    }
}
