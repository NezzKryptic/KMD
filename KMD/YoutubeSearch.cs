using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Net;

namespace KMD
{
    class YoutubeSearch
    {
        public static void FindVideos(string search_term)
        {
            if (string.IsNullOrWhiteSpace(search_term))
            {
                throw new ArgumentException("message", nameof(search_term));
            }

            Variables.current_titles.Clear();
            Variables.current_hrefs.Clear();

            Variables.current_titles.Add("Skip");
            Variables.current_hrefs.Add("Skip");

            HtmlDocument document = new HtmlDocument();

            string search_url = "https://www.youtube.com/results?search_query=" + search_term
                                    .Replace("!", "%21").Replace("\"", "%22")
                                    .Replace("#", "%23").Replace("$", "%24").Replace("%", "%25")
                                    .Replace("&", "%26").Replace("'", "%27").Replace("(", "%28")
                                    .Replace(")", "%29").Replace("*", "%2A").Replace("+", "%2B")
                                    .Replace(",", "%2C").Replace("-", "%2D").Replace(".", "%2E")
                                    .Replace("/", "%2F").Replace(":", "%3A").Replace(";", "%3B")
                                    .Replace("<", "%3C").Replace("=", "%3D").Replace(">", "%3E")
                                    .Replace("?", "%3F").Replace("@", "%40").Replace("[", "%5B")
                                    .Replace("\\", "%5C").Replace("]", "%5D").Replace("^", "%5E")
                                    .Replace("_", "%5F").Replace("`", "%60").Replace("{", "%7B")
                                    .Replace("|", "%7C").Replace("}", "%7D").Replace("~", "%7E")
                                    .Replace("`", "%80").Replace("‚", "%82").Replace(" ", "+");

            // MAKE SURE TO REMOVE THIS
            Process.Start("C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe", search_url);

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");

                    string html = client.DownloadString(search_url);

                    document.LoadHtml(html);

                    foreach (HtmlNode current_node in document.DocumentNode.SelectNodes("//h3/a"))
                    {
                        try
                        {
                            string video_title = current_node.GetAttributeValue("title", null);
                            string video_href = current_node.GetAttributeValue("href", null);

                            if (video_title != null)
                            {
                                Variables.current_titles.Add(video_title);
                            }
                            if (video_href != null)
                            {
                                Variables.current_hrefs.Add(video_href);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
