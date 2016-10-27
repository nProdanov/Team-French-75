using System.Collections.Generic;

using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.MongoDbExtractor;

namespace TravelAgency.Client
{
    public class TravelAgenciesDataImporter
    {
        private ITravelAgencyDbContext travelAgencyDbContext;
        private IMongoDbExtractor mongoExtractor;

        public TravelAgenciesDataImporter(ITravelAgencyDbContext travelAgencyDbContext, IMongoDbExtractor mongoExtractor)
        {
            this.travelAgencyDbContext = travelAgencyDbContext;
            this.mongoExtractor = mongoExtractor;
        }

        public void ImportData()
        {
            var touroperatorsReadyForImport = this.MergeData();

            foreach (var touroperator in touroperatorsReadyForImport)
            {
                this.travelAgencyDbContext.Touroperators.Add(touroperator);
            }
        }

        private IEnumerable<Touroperator> MergeData()
        {
            var mongoTourOperators = this.mongoExtractor.ExtractMongoDbTourOperators();

            // TODO: here will collect and merge data from excel and xml

            return new List<Touroperator>(); // just da ne pishi
        }
    }
}
