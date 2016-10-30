using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TravelAgency.Models
{
    public class Touroperator
    {
        private const int MinNameLength = 3;
        private const int MaxNameLength = 20;
        private const string MinNameLengthErrorMessage = "The {0} must be at least {1} characters long.";
        private const string MaxNameLengthErrorMessage = "The {0} must be no more than {1} characters long.";

        private ICollection<Trip> trips;
        private ICollection<Customer> customers;

        public Touroperator()
        {
            this.trips = new HashSet<Trip>();
            this.customers = new HashSet<Customer>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength, ErrorMessage = MinNameLengthErrorMessage)]
        [MaxLength(MaxNameLength, ErrorMessage = MaxNameLengthErrorMessage)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }

        public virtual ICollection<Customer> Customers
        {
            get { return this.customers; }
            set { this.customers = value; }
        }
    }
}
