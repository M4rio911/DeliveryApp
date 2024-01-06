using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryAppAPI.Domain.Common
{
    public class AuditableEntity
    {
        [MaxLength(150)]
        public string CreatedBy { get; set; } = default!;

        public DateTime Created { get; set; }

        [MaxLength(150)]
        public string ModifiedBy { get; set; } = default!;

        public DateTime? Modified { get; set; }
    }
}
