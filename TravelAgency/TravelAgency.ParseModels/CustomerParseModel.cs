using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.ParseModels
{
    public class CustomerParseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool HasDiscount { get; set; }
    }
}
