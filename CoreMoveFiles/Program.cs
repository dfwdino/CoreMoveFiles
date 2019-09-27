using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CoreMoveFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmldataloction = Directory.GetCurrentDirectory() + "FilesToMove.xml";
            List<MoveLocation> moveLocations = new List<MoveLocation>();

            if (File.Exists(xmldataloction))
            {
                moveLocations = DeserializeXMLFileToObject<List<MoveLocation>>(xmldataloction);
                
            }
            else
            {
                moveLocations.Add(new MoveLocation() { SouceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Freddy", 
                                                        DesctionLocation = @"E:\Personal\Images\Clean\People\Freddy Lee" });

                moveLocations.Add(new MoveLocation() { SouceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Elliott", 
                                                        DesctionLocation = @"E:\Personal\Images\Clean\People\Elliot Ryker Wolke Newsom" });

                XmlSerializer xs = new XmlSerializer(moveLocations.GetType());
                TextWriter tw = new StreamWriter(xmldataloction);
                xs.Serialize(tw, moveLocations);

            }


            foreach (MoveLocation imagemove in moveLocations)
            {
                if (File.Exists(imagemove.SouceLocation))
                {
                    Console.WriteLine($"Can't find file {imagemove.SouceLocation}. Skipping folder.");
                    continue;
                }
                else if(File.Exists(imagemove.DesctionLocation))
                {
                    Console.WriteLine($"Can't find file {imagemove.DesctionLocation}. Skipping folder.");
                    File.Create(imagemove.DesctionLocation);
                }

                var AllFiles = Directory.GetFiles(imagemove.SouceLocation);

                foreach (string filetomove in AllFiles)
                {
                    FileInfo fi = new FileInfo(filetomove);
                    string fullnamedesc = imagemove.DesctionLocation + "\\" + fi.Name;

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
    }
}
