﻿using System.Collections.Generic;
using System.Linq;

using TravelAgency.MySqlData;

namespace TravelAgency.Readers
{
    public class MySqlReader : IMySqlReader
    {
        private const string ConnectionString = "server=localhost;database=travelagency;uid=root;pwd=1234;";

        public ICollection<TripReportMySqlModel> ReadMySql()
        {
            using (var context = new MySqlContext(ConnectionString)) 
            {
                return context.TripsReports.ToList();
            }
        }
    }
}