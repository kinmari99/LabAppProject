using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LabApp.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "PESEL must be exactly 11 digits.")]
        public string PESEL { get; set; }
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [StringLength (50)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
