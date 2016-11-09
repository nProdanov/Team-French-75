using System.Collections.Generic;
using System.IO;
using System.Linq;

using TravelAgency.Readers.Contracts;

namespace TravelAgency.Readers
{
    public class JsonReportsFileReader : IJsonReportsFileReader
    {
        private const string JsonReportsDirectory = "../../../../Generated-Reports/Json-Reports/";

        public IEnumerable<string> ReadJsonReports()
        {
            var filesPaths = Directory.GetFiles(JsonReportsDirectory);

            var jsonTexts = filesPaths.Select(path => File.ReadAllText(path));

            return jsonTexts;
        }
    }
}
