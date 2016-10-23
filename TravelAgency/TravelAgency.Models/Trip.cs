using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    public class Trip
    {
        private const int MinNameLength = 3;
        private const int MaxNameLength = 50;
        private const string MinNameLengthErrorMessage = "The {0} must be at least {1} characters long.";
        private const string MaxNameLengthErrorMessage = "The {0} must be no more than {1} characters long.";

        private ICollection<Customer> customers;
        private ICollection<Destination> destinations;

        public Trip()
        {
            this.customers = new HashSet<Customer>();
            this.destinations = new HashSet<Destination>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(MinNameLength, ErrorMessage = MinNameLengthErrorMessage)]
        [MaxLength(MaxNameLength, ErrorMessage = MaxNameLengthErrorMessage)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime DeparterDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public float Discount { get; set; }

        public virtual Touroperator Touroperator { get; set; }

        public virtual ICollection<Customer> Customers
        {
            get { return this.customers; }
            set { this.customers = value; }
        }

        public virtual ICollection<Destination> Destinations
        {
            get { return this.destinations; }
            set { this.destinations = value; }
        }
    }
}
