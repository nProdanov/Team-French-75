using System.Collections.Generic;

using TravelAgency.Readers.Contracts;

namespace TravelAgency.Readers
{
    public class XmlReader : IXmlReader
    {
        public const string DiscountsListPath = "..\\..\\..\\..\\discounts.xml";

        public IDictionary<string, float> ReadXml()
        {
            var discounts = new Dictionary<string, float>();
            using (var reader = System.Xml.XmlReader.Create(DiscountsListPath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement() &&
                        reader.Name == "trip")
                    {
                        var tripName = reader.GetAttribute("name");
                        tripName = tripName.Replace('-', ' ');
                        reader.Read();
                        var tripDiscount = float.Parse(reader.ReadElementString());
                        discounts.Add(tripName, tripDiscount);
                    }
                }
            }

            return discounts;
        }
    }
}
