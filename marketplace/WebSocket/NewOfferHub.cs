using marketplace.DTO.ProductOnSaleDTO;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace marketplace.WebSocket
{
	public class NewOfferHub : Hub
	{

		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await base.OnDisconnectedAsync(exception);
		}

		public string GetConnectionId()
		{
			return Context.ConnectionId;
		}

		public static async Task SendNewOffer(IHubContext<NewOfferHub> hub, ProductOnSaleOfferDTO productOnSale)
		{
			var productOnSaleOffer = JsonConvert.SerializeObject(productOnSale);
			await hub.Clients.All.SendAsync("NewOffer", productOnSaleOffer);
		}
	}
}
