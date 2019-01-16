using System;
using System.Threading;

namespace KMD
{
    class Program
    {
        static void Main(string[] args)
        {
            int programMode = 0;

            Console.Title = "Kryptic Music Downloader";

            if (args.Length == 0 || args == null)
            {
                while (programMode != 1 || programMode != 2 || programMode != 3)
                {
                    Console.Clear();

                    Console.WriteLine("Would you like to: " + Environment.NewLine +
                                        "1: Create an XML file to prepare to download" + Environment.NewLine +
                                        "2: Load a completed XML to download from" + Environment.NewLine);

                    // Check user input to set program mode
                    programMode = int.Parse(Console.ReadLine());

                    // Choosing program mode
                    switch (programMode)
                    {
                        case 1:
                            Console.Clear();

                            Console.Write("Location of song list: ");
                            Variables.listLocation = (Console.ReadLine()).Replace("\\", "\\\\");

                            Console.Write("Where would you like to output XML: ");
                            Variables.fileOutputLocation = (Console.ReadLine()).Replace("\\", "\\\\");

                            Console.Clear();

                            XMLBuilder.BuildXml();
                            break;

                        case 2:
                            // Parse XML and download youtube links using supplied artist and song name\

                            // YouTube-DL Command
                            // youtube-dl -i --extract-audio --audio-format mp3 --audio-quality 0 --id video_id
                            break;

                        case 3:
                            Console.Clear();

                            Variables.listLocation = "F:\\Backup\\Notes\\Songs.txt";

                            Variables.fileOutputLocation = "C:\\Users\\IanEgger\\Desktop\\SongDownload.xml";

                            XMLBuilder.BuildXml();

                            Console.Clear();
                            break;

                        default:
                            Console.WriteLine("Must supply a valid mode");
                            break;
                    }
                }
            }
            else
            {
                programMode = int.Parse(args[0]);

                if(programMode == 0)
                {
                    Console.Clear();

                    Variables.listLocation = args[1].Replace("\\", "\\\\");

                    Variables.fileOutputLocation = args[2].Replace("\\", "\\\\");

                    Thread.Sleep(5);

                    Console.Clear();

                    XMLBuilder.BuildXml();
                }
                else if(programMode == 1)
                {
                    Console.WriteLine("Incomplete");
                }
                else
                {
                    Console.WriteLine("Program Argruments: KMD.exe program_mode:0-1 song_list_location xml_output_location");
                }
            }
        }
    }
}
