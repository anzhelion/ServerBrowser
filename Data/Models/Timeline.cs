using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using static ServerBrowser.Constants;

namespace ServerBrowser.Data.Models
{
    public class Timeline
    {
        [Key]
        public int Id { get; set; }

        public int ServerId { get; set; }

        [MaxLength(TimelineNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(TimelineEventDescriptionMaxLen)]
        public string EventDescription { get; set; } = string.Empty;

        [MaxLength(TimelineEventsMax)]
        public DateTime?[]? AddedOn { get; set; }
    }
}
