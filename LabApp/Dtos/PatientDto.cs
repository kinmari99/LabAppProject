namespace LabApp.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string PESEL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
   
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreatePatientDto
    {
        public string PESEL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdatePatientDto
    {
        public string LastName { get; set; }
       
        public string PhoneNumber { get; set; }
    }


}
