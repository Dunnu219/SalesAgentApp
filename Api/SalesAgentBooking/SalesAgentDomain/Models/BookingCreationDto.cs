using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAgentDomain.Models
{
    public class BookingCreationDto
    {
        public Guid CorrelationId => new Guid();

        [Required]
        public DateTime BookingDateTime { get; set; }

        [Required]
        public string BookedBy { get; set; }

        [Required]
        public int AgentId { get; set; }

        [Required]
        public int TimeSlotId { get; set; }

        [Required]
        public string BookingMessage { get; set; }
    }
}
