namespace ClassRegistrationBack.Model
{
    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Section> Sections { get; set; }  
    }
    public class GymResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
