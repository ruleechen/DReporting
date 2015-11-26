using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.Web.Mvc.UI;

namespace DReporting.Web.Mvc.Controllers
{
    public class GridDemoController : ControllerBase
    {
        public ActionResult Index()
        {
            return View(QuotesProvider.GetQuotes());
        }

        public ActionResult Grid()
        {
            return PartialView("Grid", QuotesProvider.GetQuotes());
        }
    }

    public class Quote
    {
        static readonly Random random = new Random();

        public string Symbol { get; set; }
        public Decimal Price { get; set; }
        public Decimal Change { get; set; }
        public Decimal DayMax { get; set; }
        public Decimal DayMin { get; set; }
        public DateTime LastUpdated { get; set; }

        public void Update()
        {
            if (LastUpdated.Day != DateTime.Now.Day)
            {
                DayMax = 0;
                DayMin = 0;
            }
            Change = (Decimal)((0.5 - random.NextDouble()) / 5.0);
            Decimal newPrice = Price + Price * Change;
            if (newPrice < 0) newPrice = 0;
            if (Price > 0)
                Change = (newPrice - Price) / Price;
            else
                Change = 0;
            Price = newPrice;
            LastUpdated = DateTime.Now;
            if (Price > DayMax || DayMax == 0)
                DayMax = Price;
            if (Price < DayMin || DayMin == 0)
                DayMin = Price;
        }
    }

    public class QuotesProvider
    {
        static string[] symbolsList = new string[] { "MSFT", "INTC", "CSCO", "SIRI", "AAPL", "HOKU", "ORCL", "AMAT", "YHOO", "LVLT", "DELL", "GOOG" };
        static string yahooUrl = "http://finance.yahoo.com/d/quotes.csv?s={0}&f=s0l1h0g0v0d1";
        static readonly Random random = new Random();

        static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public static List<Quote> GetQuotes()
        {
            if (Session["Quotes"] == null)
                Session["Quotes"] = LoadQuotes();
            var quotes = (List<Quote>)Session["Quotes"];
            UpdateQuotes(quotes);
            return quotes;
        }

        public static List<Quote> LoadQuotes()
        {
            try
            {
                return LoadQuotesFromYahoo();
            }
            catch
            {
                return GenerateQuotes();
            }
        }

        static List<Quote> LoadQuotesFromYahoo()
        {
            var quotes = new List<Quote>();
            var url = string.Format(yahooUrl, string.Join("+", symbolsList));
            var request = HttpWebRequest.Create(url);
            using (var stream = request.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    while (!reader.EndOfStream)
                    {
                        var values = reader.ReadLine().Replace("\"", "").Split(new char[] { ',' });
                        Quote quote = new Quote();
                        quote.Symbol = values[0].Trim();
                        Decimal value;
                        if (Decimal.TryParse(values[1], out value))
                            quote.Price = value;
                        else
                            quote.Price = 0;
                        if (Decimal.TryParse(values[2], out value))
                            quote.DayMax = value;
                        else
                            quote.DayMax = 0;
                        if (Decimal.TryParse(values[3], out value))
                            quote.DayMin = value;
                        else
                            quote.DayMin = 0;
                        DateTime date;
                        if (DateTime.TryParse(values[4], out date))
                            quote.LastUpdated = date;
                        else
                            quote.LastUpdated = DateTime.Now;
                        quotes.Add(quote);
                    }

                }
            }
            return quotes;
        }

        static List<Quote> GenerateQuotes()
        {
            return new List<Quote> {
                new Quote { Symbol = "MSFT", Price = 37.95M, DayMax = 37.95M, DayMin = 36.06M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "INTC", Price = 24.85M, DayMax = 25.80M, DayMin = 24.85M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "CSCO", Price = 22.99M, DayMax = 22.99M, DayMin = 22.64M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "SIRI", Price = 3.71M, DayMax = 3.75M, DayMin = 3.69M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "AAPL", Price = 586.73M, DayMax = 586.73M, DayMin = 540.42M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "HOKU", Price = 0, DayMax = 0, DayMin = 0, LastUpdated = DateTime.Now },
                new Quote { Symbol = "ORCL", Price = 38.11M, DayMax = 38.52M, DayMin = 37.80M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "AMAT", Price = 17.61M, DayMax = 17.69M, DayMin = 17.47M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "YHOO", Price = 40.80M, DayMax = 40.80M, DayMin = 38.86M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "LVLT", Price = 31.85M, DayMax = 34.25M, DayMin = 31.85M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "DELL", Price = 20.63M, DayMax = 26.64M, DayMin = 14.38M, LastUpdated = DateTime.Now },
                new Quote { Symbol = "GOOG", Price = 1163.70M, DayMax = 1164M, DayMin = 1151.30M, LastUpdated = DateTime.Now }
            };
        }

        private static void UpdateQuotes(List<Quote> quotes)
        {
            foreach (Quote quote in quotes)
            {
                if (random.NextDouble() >= 0.7)
                    quote.Update();
            }
        }
    }
}
