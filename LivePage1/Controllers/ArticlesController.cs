using LivePage1.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace LivePage1.Controllers
{
    [Route("api/Articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        //int onlineNumber;

        public static List<Article> articles = new List<Article>()
        {
            new Article { Title = "Britene feiret og sørget over Brexit, men Boris holdt seg hjemme", Link = "vg.no/wetalktogether.html", PublishTime = new DateTime(2015, 10, 03, 13, 45, 34) },
            new Article { Title = "Demokratene tapte dragkamp om vitner", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2019, 10, 03, 13, 50, 34) },
            new Article { Title = "Demokratene tapte dragkamp om vitner", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 50, 34) },
            new Article { Title = "Disse vant Vixen Awards", Link = "vg.no/weplaytogether.html", PublishTime = new DateTime(2019, 10, 03, 13, 55, 34) },
            new Article { Title = "Krimguide 2020: Ti krimbøker å merke seg i vår", Link = "vg.no/wetalktogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 45, 34) },
            new Article { Title = "Ber UD hjelpe flere av IS-kvinnene", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 50, 34) },
            new Article { Title = "Sonos: Ikke mulig å reversere «resirkuleringsmodus»", Link = "vg.no/weplaytogether.html", PublishTime = new DateTime(2019, 10, 03, 13, 55, 34) },
            new Article { Title = "Ute av «Mesternes mester»: – Kjedelig, men litt fortjent", Link = "vg.no/wetalktogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 45, 34) },
            new Article { Title = "Fylkesleder i Trøndelag Ap tar ikke gjenvalg", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 50, 34) },
            new Article { Title = "Rekordbillig å fly langt", Link = "vg.no/weplaytogether.html", PublishTime = new DateTime(2019, 10, 03, 13, 55, 34) },
            new Article { Title = "Kallmyrs klimatiltak: Ville ha kjøttfri fengselskost", Link = "vg.no/wetalktogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 45, 34) },
            new Article { Title = "Hellstrøm ut mot Michelin-guiden: – Ynkelig", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 50, 34) },
            new Article { Title = "Fikk 50 millioner til Hollywood-film i Norge", Link = "vg.no/weplaytogether.html", PublishTime = new DateTime(2019, 10, 03, 13, 55, 34) },
            new Article { Title = "NAV-sjefen går av i august", Link = "vg.no/wetalktogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 45, 34) },
            new Article { Title = "We eat together", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 50, 34) },
            new Article { Title = "We play together", Link = "vg.no/weplaytogether.html", PublishTime = new DateTime(2019, 10, 03, 13, 55, 34) }
        };

        [HttpGet]
        public IEnumerable<Article> GetAllArticles()
        {
            //chose the titles
            var noduplicates = articles.Select(a => a.Title).Distinct();
            int length = noduplicates.Count();
            //get the article array without duplicate
            Article[] myArticles = new Article[length];
            for (int i = 0; i < length; i++)
            {
                myArticles[i] = articles.First(myArticle => myArticle.Title == noduplicates.ElementAt(i));
            }
            return myArticles;
        }

        [HttpGet("GetMyArticles/{website}")]
        public IEnumerable<Article> GetMyArticles(string website)
        {
            string content = "";
            List<Article> myArticles = new List<Article>(); //to store the links of the articles
            myArticles.Clear();
            if (website != null) //get the html codes of the website
            {
                string domainPattern = "[a-zA-Z0-9]{2,}\\.[a-zA-Z0-9]{2,}$";//get domain name
                Match m = Regex.Match(website, domainPattern);
                website = "https://www." + m.ToString();
                ///website = "https://" + website;
                HttpWebRequest request0 = (HttpWebRequest)WebRequest.Create(website);
                HttpWebResponse response0 = (HttpWebResponse)request0.GetResponse();

                if (response0.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream0 = response0.GetResponseStream();
                    StreamReader readStream0 = null;

                    if (String.IsNullOrWhiteSpace(response0.CharacterSet))
                        readStream0 = new StreamReader(receiveStream0);
                    else
                        readStream0 = new StreamReader(receiveStream0, Encoding.GetEncoding(response0.CharacterSet));

                    content = readStream0.ReadToEnd();

                    response0.Close();
                    readStream0.Close();
                }
                string htmlTagPattern0 = "<a[\\s\\S]+?>?[\\s\\S]+?</a>?"; //get the html of the links in body part
                MatchCollection matches0 = Regex.Matches(content, htmlTagPattern0);
                
                foreach (Match match in matches0)
                {
                    string linkPattern = "(href=)[a-zA-Z0-9'\"/-_?\\.%]{4,}";
                    Match linkMatch = Regex.Match(match.ToString(), linkPattern);
                    string link = linkMatch.ToString();
                    if (!link.Contains(m.ToString()))
                    {
                        continue;
                    }
                    string titlePattern = ">[^<>]+<?";
                    Match titleMatch = Regex.Match(match.ToString(), titlePattern);
                    string title = titleMatch.ToString();
                    if (title.Length < 3 || !title.Contains(" ") || title.IndexOf("\n") > -1 || title.Length > 50)
                    {
                        continue;
                    }
                    Article newArticle = new Article { Title = title.Replace("<", "").Replace(">", ""), Link = link.Replace("\"", "").Replace("href=", ""), PublishTime = DateTime.Now };
                    myArticles.Add(newArticle);
                }
            }
            var noduplicates = myArticles.Select(t => t.Title).Distinct();
            List<Article> distinctArticles = new List<Article>();
            for (int i = 0; i < noduplicates.Count(); i++)
            {
                distinctArticles.Add(myArticles.First(myArticle => myArticle.Title == noduplicates.ElementAt(i)));
            }
            return distinctArticles;
        }

        [HttpGet("{title}")]
        public IActionResult GetArticle(string title)
        {
            var article = articles.FirstOrDefault((p) => p.Title == title);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpGet("GetArticle2/{searchstr1}/{searchstr2}")] //this function is not is use presently
        public IEnumerable<Article> GetArticle2(string searchStr1, string searchStr2)
        {
            //make the string as ISO DateTime format
            searchStr1 = searchStr1.Substring(0, 4) + "-" + searchStr1.Substring(4, 2) + "-" + searchStr1.Substring(6, 2);
            searchStr2 = searchStr1.Substring(0, 4) + "-" + searchStr1.Substring(4, 2) + "-" + searchStr1.Substring(6, 2);
            DateTime time1 = DateTime.Parse(searchStr1);
            DateTime time2 = DateTime.Parse(searchStr2);
           
            var articles2 = articles.Where(a => a.PublishTime > time1 && a.PublishTime > time2);
            return articles2.ToList();
        }


        [HttpPost]
        public ActionResult<Article> PostArticle(Article article)
        {
            articles.Add(article);
            //await article.SaveChangesAsync();
            return Ok();
        }

       [HttpGet("{action}")]
       public IEnumerable<Article> GetNewArticles()
       {
            var queryArticles = from newArticle in articles where newArticle.PublishTime >= DateTime.Now.AddYears(-5) select newArticle;           
            return queryArticles;

       }

    }
}