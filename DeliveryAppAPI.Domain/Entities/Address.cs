using DeliveryAppAPI.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryAppAPI.Domain.Entities
{
    public class Address :AuditableEntity
    {
        [Column("AddressId")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public int? PostCodeId { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
    }
}
