using NET8_WebApp.Services.Interfaces;

namespace NET8_WebApp.Services;

public class KeyedEvenMinuteService : IKeyedMinuteService
{
    public string Execute()
    {
        return "Minute is even!";
    }
}
