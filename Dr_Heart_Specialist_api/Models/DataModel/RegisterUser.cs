namespace Dr_Heart_Specialist_api.Models.DataModel
{
    public class RegisterUser
    {
        public string Consultant_Name { get; set; }
        public string Consultant_Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string Contact_No { get; set; }
        public bool IsDiabetic { get; set; }
    }
}