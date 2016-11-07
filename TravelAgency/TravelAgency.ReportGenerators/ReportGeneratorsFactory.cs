using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public IReportGenerator CreateReportGenerator(string reportRequest)
        {
            var controllerClassName = reportRequest + "Generator";
            var type =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(
                        x => x.Name.ToLower() == controllerClassName.ToLower() && typeof(IReportGenerator).IsAssignableFrom(x));

            if (type == null)
            {
                throw new ArgumentException("There is no generator of that type.");
            }

            IReportGenerator instance;
            if (type == typeof(JsonGenerator))
            {
                instance = (IReportGenerator)Activator.CreateInstance(type, this.mySqlImporter);
            }
            else
            {
                instance = (IReportGenerator)Activator.CreateInstance(type);
            }

            return instance;
        }
    }
}
