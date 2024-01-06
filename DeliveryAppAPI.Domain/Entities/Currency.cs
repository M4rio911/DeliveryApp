using DeliveryAppAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryAppAPI.Domain.Entities
{
    public class Currency : AuditableEntity
    {
        [Column("CurrencyId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short { get; set; }
    }
}
