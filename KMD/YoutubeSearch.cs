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
                throw new ArgumentException("No Search Term Provided", nameof(search_term));
            }

            HtmlDocument document = new HtmlDocument();

            Variables.current_titles.Clear();
            Variables.current_uploaders.Clear();
            Variables.current_hrefs.Clear();

            Variables.current_titles.Add("Skip");
            Variables.current_uploaders.Add("None");
            Variables.current_hrefs.Add("None");

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

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";

                string html = client.DownloadString(search_url);

                document.LoadHtml(html);

                foreach (HtmlNode current_node in document.DocumentNode.SelectNodes("//ytd-video-renderer/div/div/div/div/h3/a"))
                {
                    string video_href = current_node.GetAttributeValue("href", null).Replace("/watch?v=", "");
                    string video_title = current_node.GetAttributeValue("title", null);

                    try
                    {
                        Variables.current_hrefs.Add(video_href);
                    }
                    catch { }
                    finally
                    {
                        Variables.current_hrefs.Add("None");
                    }

                    try
                    {
                        Variables.current_titles.Add(video_title);
                    }
                    catch { }
                    finally
                    {
                        Variables.current_hrefs.Add("None");
                    }
                }

                foreach (HtmlNode current_node in document.DocumentNode.SelectNodes("//ytd-video-meta-block/div/div/div/yt-formatted-string"))
                {
                    string video_uploader = current_node.GetAttributeValue("title", null);

                    try
                    {
                        Variables.current_uploaders.Add(video_uploader);
                    }
                    catch { }
                    finally
                    {
                        Variables.current_hrefs.Add("None");
                    }
                }

                /*
                foreach (string href in Variables.current_hrefs)
                {
                    Console.WriteLine(href);
                }

                Console.ReadKey();

                Console.Clear();

                foreach (string title in Variables.current_titles)
                {
                    Console.WriteLine(title);
                }

                Console.ReadKey();

                Console.Clear();

                foreach (string uploader in Variables.current_uploaders)
                {
                    Console.WriteLine(uploader);
                }

                Console.ReadKey();

                Console.Clear();
                */
            }
        }
    }
}
