using Microsoft.AspNetCore.Mvc;
using NET8_WebApp.Services.Interfaces;

namespace NET8_WebApp.Controllers
{
    // https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8#keyed-di-services
    [ApiController]
    [Route("[controller]")]
    public class KeyedMinuteController : ControllerBase
    {
        private readonly ILogger<KeyedMinuteController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IKeyedMinuteService _keyedService = null;

        public KeyedMinuteController(
            ILogger<KeyedMinuteController> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            SetKeyedServiceByCurrentMinute();
        }

        [HttpGet(Name = "GetKeyedMinute")]
        public string Get()
        {
            return _keyedService.Execute();
        }

        private void SetKeyedServiceByCurrentMinute()
        {
            _keyedService = _serviceProvider.GetRequiredKeyedService<IKeyedMinuteService>(DateTime.UtcNow.Minute % 2 == 0 ? "even" : "odd");
        }
    }
}