using System.ComponentModel.DataAnnotations;

namespace ServerBrowser.Models
{
    public class ServerViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string PublisherId { get; set; } = string.Empty!;

        public DateTime AddedOn { get; set; }

        public int ServerType { get; set; }
        // 0 - Diverse Server
        // 1 - Game Server
        // 2 - Lobby Server
        // 3 - Chatting Server

        public string IPAddress { get; set; }

        public DateTime LastLiveOn { get; set; }

        public DateTime ActiveTimeStart { get; set; }

        public DateTime ActiveTimeEnd { get; set; }

        public int PeakConcurrentUsers { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsDedicated { get; set; }

        public bool IsOfficial { get; set; }

        public bool IsRemoved { get; set; }

        public string TypeString { get; set; } = string.Empty;
    }
}
