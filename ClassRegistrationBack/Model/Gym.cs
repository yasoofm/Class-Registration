namespace ClassRegistrationBack.Model
{
    public class Gym
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public List<Section> Section { get; set; }  
    }
}
