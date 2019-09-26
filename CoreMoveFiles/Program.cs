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
                moveLocations.Add(new MoveLocation() { SouceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Freddy", DesctionLocation = @"E:\Personal\Images\Clean\People\Freddy Lee" });
                moveLocations.Add(new MoveLocation() { SouceLocation = @"C:\Users\Shane\Desktop\tempphtos\temp phone\Elliott", DesctionLocation = @"E:\Personal\Images\Clean\People\Elliot Ryker Wolke Newsom" });

                XmlSerializer xs = new XmlSerializer(moveLocations.GetType());
                TextWriter tw = new StreamWriter(xmldataloction);
                xs.Serialize(tw, moveLocations);

            }


            foreach (MoveLocation imagemove in moveLocations)
            {

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
                //ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now);
            }
            return returnObject;
        }
    }
}
