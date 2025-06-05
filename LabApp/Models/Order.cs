using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace LabApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [Required]
        public int DiagnosticianId { get; set; }
        public Diagnostician Diagnostician { get; set; }
        [Required]
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public DateTime OrderedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Examination> Examinations { get; set; }
    }
}
