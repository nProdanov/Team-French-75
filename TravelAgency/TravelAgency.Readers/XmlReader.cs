using System.Collections.Generic;

namespace TravelAgency.Readers
{
    public class XmlReader
    {
        public void ReadXml()
        {
            var url = "..\\..\\..\\..\\discounts.xml";

            var discounts = new Dictionary<string, double>();
            using (var reader = System.Xml.XmlReader.Create(url))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() &&
                        reader.Name == "trip")
                    {
                        var tripName = reader.GetAttribute("name");
                        tripName = tripName.Replace('-', ' ');
                        reader.Read();
                        var tripDiscount = double.Parse(reader.ReadElementString());
                        discounts.Add(tripName, tripDiscount);
                    }
                }
            }
        }
    }
}
