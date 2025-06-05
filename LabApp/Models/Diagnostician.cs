using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace LabApp.Models
{
    public class Diagnostician
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength (50)]
        public string LastName { get; set; }
        [Required]
        [StringLength (5)]
        public string PWZDL { get; set; }
        [Required]
        [EmailAddress]
        [StringLength (100)]
        public string Email { get; set; }
        public string Specialization { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
