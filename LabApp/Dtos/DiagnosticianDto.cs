namespace LabApp.Dtos
{
    public class DiagnosticianDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PWZDL { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
    }

    public class CreateDiagnosticianDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PWZDL { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
    }
    public class UpdateDiagnosticianDto
    {
        public string LastName { get; set; }
        public string PWZDL { get; set; }
        public string Email { get; set; }
        public string Specialization { get; set; }
    }
}
