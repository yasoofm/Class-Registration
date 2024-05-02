namespace ClassRegistrationBack.Model
{
    public class Instructor
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public List<Section> Sections { get; set; }
    }

    public class InstructorResponse
    {
        public int Id { get; set; }
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public List<Section> Sections { get; set; }
    }
}
