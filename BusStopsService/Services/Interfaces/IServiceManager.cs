using Services.Abstractions.Interfaces;

namespace Services.Interfaces
{
    public interface IServiceManager
    {
        IBusStopService BusStopService { get; }
    }
}
