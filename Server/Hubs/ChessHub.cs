using InfiniteChess.Shared;
using Microsoft.AspNetCore.SignalR;

namespace InfiniteChess.Server.Hubs; 

public class ChessHub : Hub {
	public async Task SendBoard(Board board) {
		await Clients.Others.SendAsync("ReceiveBoard", board);
	}
}
