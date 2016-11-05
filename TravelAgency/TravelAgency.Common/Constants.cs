using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Common
{
    public class Constants
    {
        public const string MongoDbConnectionString = "mongodb://localhost";
        public const string MongoDbDatabaseName = "travelagency";
        public const string MongoDbCollectionName = "touroperators";
        public const string MySQLConnectionString = "server=localhost;database=travelagency;uid=root;pwd=1234;";
        public const string GeneratedReportsPath = "../../../../Generated-Reports/";
    }
}
