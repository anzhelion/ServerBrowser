using System.ComponentModel.DataAnnotations;

namespace ServerBrowser.Models
{
    public class AnnouncementViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string PublisherId { get; set; } = string.Empty!;

        public DateTime AddedOn { get; set; }

        public int ServerId { get; set; }
    }
}
