using System.Collections.Concurrent;

namespace BlazorApp.Data
{
    public class GameSessionService
    {
        private readonly ConcurrentDictionary<string, GameSession> sessions = new();
        private readonly Random random = new();

        public string CreateSession(string hostName)
        {
            string code;
            do
            {
                code = random.Next(100000, 999999).ToString();
            } while (sessions.ContainsKey(code));
            var session = new GameSession { HostName = hostName };
            session.Players.Add(hostName);
            sessions[code] = session;
            return code;
        }

        public bool JoinSession(string code, string playerName)
        {
            if (sessions.TryGetValue(code, out var session))
            {
                if (!session.Players.Contains(playerName))
                    session.Players.Add(playerName);
                return true;
            }
            return false;
        }

        public GameSession? GetSession(string code) => sessions.TryGetValue(code, out var s) ? s : null;
    }

    public class GameSession
    {
        public string HostName { get; set; } = string.Empty;
        public List<string> Players { get; set; } = new();
    }
}
