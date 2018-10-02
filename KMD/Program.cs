using System;

namespace KMD
{
    class Program
    {
        static void Main(string[] args)
        {
            int programMode = 0;

            Console.Title = "Kryptic Music Downloader";

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
                        // Parse XML and download youtube links using supplied artist and song name
                        break;

                    default:
                        Console.WriteLine("Must supply a valid mode");
                        break;
                }
            }
        }
    }
}
