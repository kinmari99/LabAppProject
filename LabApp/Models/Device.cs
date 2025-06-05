using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LabApp.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }
        public bool IsOperational { get; set; } = true;

        public ICollection<Order> Orders { get; set; }
        public ICollection<Examination> Examinations { get; set; }
    }
}
