using System;
using System.IO;
using System.Xml;

namespace KMD
{
    class XMLBuilder
    {
        public static void BuildXml()
        {
            // Load user defined song list
            Variables.songList = File.ReadAllLines(Variables.listLocation);
            XmlWriter xmlWriter = XmlWriter.Create(Variables.fileOutputLocation);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("videos");

            // Loops through song list and uses input to create a download list with xml
            foreach (string song in Variables.songList)
            {
                Console.WriteLine("Current Search: " + song + Environment.NewLine);

                YoutubeSearch.FindVideos(song);

                for (int label_index = 0; label_index < Variables.current_titles.Count; label_index++)
                {
                    Console.WriteLine(label_index + " : " + Variables.current_titles[label_index] + " ~ " + Variables.current_uploaders[label_index]);
                }

                Console.WriteLine(Environment.NewLine + "Number to download: ");
                int download_choice = int.Parse(Console.ReadLine());

                if (download_choice != 0)
                {
                    Console.WriteLine(Environment.NewLine + "Song title: ");
                    string song_title = Console.ReadLine();

                    Console.WriteLine(Environment.NewLine + "Song artist: ");
                    string song_artist = Console.ReadLine();

                    xmlWriter.WriteStartElement("dl-info");
                    xmlWriter.WriteAttributeString("id", Variables.current_hrefs[download_choice]);
                    xmlWriter.WriteAttributeString("title", song_title);
                    xmlWriter.WriteAttributeString("artist", song_artist);
                    xmlWriter.WriteEndElement();
                    xmlWriter.Flush();
                }

                Console.Clear();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }
}
