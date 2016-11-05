using System;

using TravelAgency.Importers;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class ReportGeneratorsFactory
    {
        private MySqlImporter mySqlImporter;
        public ReportGeneratorsFactory(MySqlImporter mySqlImporter)
        {
            this.mySqlImporter = mySqlImporter;
        }
        public IReportGenerator CreateReportGenerator(string type)
        {
            switch (type)
            {
                case "xml":
                    return new XmlGenerator();
                case "json":
                    return new JsonGenerator(this.mySqlImporter);
                case "pdf":
                    return new PdfGenerator();
                default:
                    throw new ArgumentException("There is no generator of that type.");
            }
        }
    }
}
