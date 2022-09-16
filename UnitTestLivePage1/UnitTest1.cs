using Microsoft.VisualStudio.TestTools.UnitTesting;
using LivePage1;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System;
using LivePage1.Models;
using System.Linq;

namespace UnitTestLivePage1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string htmlTagPattern0 = "<[Aa][\\s\\S]*?</[Aa]>";
            string input = "This is a link <a>a link</a>; This is another link <a>another \n\n link</a>";
            int expected = 2;
            MatchCollection matches0 = Regex.Matches(input, htmlTagPattern0);
            
            int actual = matches0.Count;
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            string htmlTagPattern0 = "((href)(\\s)?=(\\s)?['\"].+?['\"])";
            string input = "This is a link <a href='http://vg.no/wearetogether.html' > a link</a>; This is another link <a  href= 'http://www.vg.no/wearetoeuther.html' > another link</a>";
            int expected1 = 2;
            MatchCollection matches0 = Regex.Matches(input, htmlTagPattern0);

            int actural1 = matches0.Count;
            Assert.AreEqual(expected1, actural1);
            string expected2 = "href='http://vg.no/wearetogether.html'";
            string actual2 = matches0[0].ToString();
            Assert.AreEqual(expected2, actual2);
            string expected3 = "href= 'http://www.vg.no/wearetoeuther.html'";
            string actual3 = matches0[1].ToString();
            Assert.AreEqual(expected3, actual3);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string input = "This is a link <a href='http://vg.no/wearetogether.html' > a link</a>; This is a speical link <a href= '#wearetoeuther.html' > a speical link</a>; This is anoter link <a  href= 'http://www.vg.no/wearetoeuther.\n\nhtml' > another link</a>";
            string htmlTagPattern1 = "((href)(\\s)?=(\\s)?['\"][\\s\\S^'\"]+?['\"])";
            List<string> myLinks = new List<string>();
            MatchCollection matches1 = Regex.Matches(input, htmlTagPattern1);
            foreach (Match match1 in matches1)
            if (!(match1.ToString().Contains("#")) || match1.ToString().IndexOf("#") > 10)
            {
                string htmlTagPattern2 = "['\"][\\s\\S^'\"]+?['\"]";
                MatchCollection matches2 = Regex.Matches(match1.ToString(), htmlTagPattern2);
                myLinks.Add(matches2[0].ToString().Substring(1, matches2[0].ToString().Length - 2));
            }
            string expected = "http://vg.no/wearetogether.html";
            string actual = myLinks[0].ToString();
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestMethod4()
        {
            string input = "This is a title <title>my title</title>; This is a time <time>publish time</time>";
            string title = Regex.Matches(input, "<title>[\\s\\S]+?</title>")[0].ToString().Replace("<title>", "").Replace("</title>", "");
            string time = Regex.Matches(input, "<time>[\\s\\S]+?</time>")[0].ToString().Replace("<time>", "").Replace("</time>", "");
            string expected1 = "my title";
            string actual1 = title;
            Assert.AreEqual(expected1, actual1);
            string expected2 = "publish time";
            string actual2 = time;
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void TestMethod5()
        {
            string articleStr = "";
            HttpWebRequest request0 = (HttpWebRequest)WebRequest.Create("https://www.vg.no/sport/langrenn/i/Vb2pl3/johaug-vant-og-roste-flugstad-oestberg-betyr-enormt-mye-for-henne?utm_content=row-1&utm_source=vgfront");
            HttpWebResponse response0 = (HttpWebResponse)request0.GetResponse();

            if (response0.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream0 = response0.GetResponseStream();
                StreamReader readStream0 = null;

                if (String.IsNullOrWhiteSpace(response0.CharacterSet))
                    readStream0 = new StreamReader(receiveStream0);
                else
                    readStream0 = new StreamReader(receiveStream0, Encoding.GetEncoding(response0.CharacterSet));

                articleStr = readStream0.ReadToEnd();

                response0.Close();
                readStream0.Close();

            }
            string title = "";
            MatchCollection titleMatches = Regex.Matches(articleStr, "<head>[\\s\\S]+?</head>");
            if (titleMatches.Count > 0)
            {
                string titlePattern = titleMatches[0].ToString();
                title = Regex.Matches(titlePattern, "<title[\\s\\S]+?</title>")[0].ToString();
                title = Regex.Replace(title, "<title[\\s\\S]+?>", "");
                title = Regex.Replace(title, "</title>", "");
            }
            string expected = "Johaug vant og roste Flugstad Østberg: – Betyr enormt mye for henne – VG";
 
             Assert.AreEqual(expected, title);

        }

        [TestMethod]
        public void TestMethod6()
        {
            string expected = DateTime.Now.ToString();
            string actual = "y";
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void TestMethod7()
        {
            string input = "https://hjem.vg.no/";
            int expected = input.Split("/").Length;
            int actual = 4;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestMethod8()
        {
            string input = ">we er her <";
            string actual = Regex.Match(input.ToString(), ">[\\s\\S]+?<").Value;
            string expected = input;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestMethod9()
        {
            string input = ">we er her <";
            string origin = Regex.Match(input.ToString(), ">[\\s\\S]+?<").Value;
            string actual = origin.Replace('>', ' ').Replace('<', ' ').Trim();
            string expected = "we er her";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod10()
        {
            string input = ">we er her <";
            string origin = Regex.Match(input.ToString(), ">[\\s\\S]+?<").Value;
            int actual = origin.Replace('>', ' ').Replace('<', ' ').Trim().Split(" ").Length;
            int expected = 3;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod11()
        {
            Article[] articles = new Article[]{
                        new Article { Title = "Demokratene tapte dragkamp om vitner", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 50, 34) },
                        new Article { Title = "Demokratene tapte dragkamp om vitner", Link = "vg.no/weeattogether.html", PublishTime = new DateTime(2020, 10, 03, 13, 50, 34) }
                    };

            var noduplicates = articles.Select(a => a.Title).Distinct();
            int length = noduplicates.Count();
            //get the article array without duplicate
            Article[] myArticles = new Article[length];
            for (int i = 0; i < length; i++)
            {
                myArticles[i] = articles.First(myArticle => myArticle.Title == noduplicates.ElementAt(i));
            }
            int actual = myArticles.Length;
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}
