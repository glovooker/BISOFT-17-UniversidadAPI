using Microsoft.AspNetCore.SignalR;

namespace UniversidadAPI.Observer
{
    public class SignalRObserver : IObserver
    {
        private readonly IHubContext<TableUpdateHub> _hubContext;

        public SignalRObserver(IHubContext<TableUpdateHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async void Update(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveTableUpdate", message);
        }
    }

}
