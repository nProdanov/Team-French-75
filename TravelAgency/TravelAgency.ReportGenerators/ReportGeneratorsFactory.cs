using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class ReportGeneratorsFactory
    {
        public IReportGenerator CreateReportGenerator(string type)
        {
            switch (type)
            {
                case "xml":
                    return new XmlGenerator();
                case "json":
                    return new JsonGenerator();
                case "pdf":
                    return new PdfGenerator();
                default:
                    throw new ArgumentException("There is no generator of that type.");
            }
        }
    }
}
