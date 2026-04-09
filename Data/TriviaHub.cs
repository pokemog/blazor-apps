using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace BlazorApp.Data
{
    public class TriviaHub : Hub
    {
        public async Task JoinGame(string gameCode, string playerName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameCode);
            await Clients.Group(gameCode).SendAsync("PlayerJoined", playerName);
        }

        public async Task PlayerListChanged(string gameCode)
        {
            await Clients.Group(gameCode).SendAsync("PlayerListChanged");
        }

        public async Task StartGame(string gameCode)
        {
            await Clients.Group(gameCode).SendAsync("GameStarted");
        }

        public async Task SendQuestion(string gameCode, string questionText, string[] answers)
        {
            await Clients.Group(gameCode).SendAsync("ReceiveQuestion", questionText, answers);
        }

        public async Task SendAnswer(string gameCode, string playerName, string answer)
        {
            await Clients.Group(gameCode).SendAsync("ReceiveAnswer", playerName, answer);
        }
    }
}
