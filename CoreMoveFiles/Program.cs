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

            }
            else
            {
                moveLocations.Add(new MoveLocation() { SouceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Freddy", DesctionLocation = "" });
                moveLocations.Add(new MoveLocation() { SouceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Elliott", DesctionLocation = "" });

                XmlSerializer xs = new XmlSerializer(moveLocations.GetType());
                TextWriter tw = new StreamWriter(xmldataloction);
                xs.Serialize(tw, moveLocations);

            }


            foreach (MoveLocation imagemove in moveLocations)
            {

            }
           

        }
    }
}
