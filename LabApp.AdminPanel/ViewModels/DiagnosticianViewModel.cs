using System.ComponentModel.DataAnnotations;

namespace LabApp.AdminPanel.ViewModels
{
    public class DiagnosticianViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PWZDL { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
    }
}
