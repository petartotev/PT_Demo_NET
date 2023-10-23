using NET8_WebApp.Services.Interfaces;

namespace NET8_WebApp.Services
{
    public class KeyedOddMinuteService : IKeyedMinuteService
    {
        public string Execute()
        {
            return "Minute is odd!";
        }
    }
}
