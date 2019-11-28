using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CoreMoveFiles
{
    class Program
    {
        static string xmldataloction = Directory.GetCurrentDirectory() + "\\FilesToMove.xml";
        static List<MoveLocation> moveLocations = new List<MoveLocation>();

        static void Main(string[] args)
        {           

            if (File.Exists(xmldataloction))
            {
                moveLocations = DeserializeXMLFileToObject<List<MoveLocation>>(xmldataloction);
                
            }
            else
            {

                bool IsWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

                if (IsWindows)
                {
                    CreatWindowsDefaultsValues();
                }
                else
                {
                    Console.WriteLine("Can't find OS!!!");
                }
                               

            }


            foreach (MoveLocation imagemove in moveLocations)
            {
                if (!Directory.Exists(imagemove.SourceLocation))
                {
                    Console.WriteLine($"Can't find source folder {imagemove.SourceLocation}. Skipping folder.");
                    continue;
                }
                else if(!Directory.Exists(imagemove.DestinationLocation))
                {
                    Console.WriteLine($"Can't destination folder file {imagemove.DestinationLocation}. Skipping folder.");
                    continue;
                    //File.Create(imagemove.DestinationLocation);
                }

                

                var AllFiles = Directory.GetFiles(imagemove.SourceLocation);

                foreach (string filetomove in AllFiles)
                {
                    FileInfo fi = new FileInfo(filetomove);
                    string fullnamedesc = imagemove.DestinationLocation + "\\" + fi.Name;

                    if (File.Exists(fullnamedesc))
                    {
                        FileInfo fid = new FileInfo(fullnamedesc);

                        if (fi.Length.Equals(fid.Length))
                        {
                            Console.WriteLine($"Found File {fullnamedesc}. Deleting file.");
                            FileSystem.DeleteFile(fullnamedesc,UIOption.OnlyErrorDialogs,RecycleOption.SendToRecycleBin);
                            continue;
                        }
                        else
                        {
                            fullnamedesc = fullnamedesc.Replace(".", $"_{DateTime.Now.ToFileTime()}.");
                        }
                        
                    }

                    File.Move(filetomove, fullnamedesc);
                    Console.WriteLine($"Moving file {fullnamedesc}");

                }

            }
           

        }

        public static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename)) return default(T);

            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnObject;
        }

        public static void CreatWindowsDefaultsValues()
        {
            moveLocations.Add(new MoveLocation()
            {
                SourceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Freddy",
                DestinationLocation = @"E:\Personal\Images\Clean\People\Freddy Lee"
            });

            moveLocations.Add(new MoveLocation()
            {
                SourceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Elliott",
                DestinationLocation = @"E:\Personal\Images\Clean\People\Elliot Ryker Wolke Newsom"
            });

            XmlSerializer xs = new XmlSerializer(moveLocations.GetType());
            TextWriter tw = new StreamWriter(xmldataloction);
            xs.Serialize(tw, moveLocations);
        }
    }
}
