namespace ClassRegistrationFront.Models
{
    public class GlobalAppState
    {
        public string token { get; set; }

        public bool IsLoggedIn()
        {
            if(token == null)
            {
                return false;
            }
            return true;
        }
    }
}
