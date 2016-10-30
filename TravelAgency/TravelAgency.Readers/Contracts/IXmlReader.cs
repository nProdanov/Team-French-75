using System.Collections.Generic;

namespace TravelAgency.Readers.Contracts
{
    public interface IXmlReader
    {
        IDictionary<string, float> ReadXml();
    }
}