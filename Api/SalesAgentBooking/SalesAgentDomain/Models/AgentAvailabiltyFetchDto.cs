using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentDomain.Models
{
    public class AgentAvailabiltyFetchDto
    {
        public Guid CorrelationId => new Guid();

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        public int AgentId { get; set; }
    }
}
