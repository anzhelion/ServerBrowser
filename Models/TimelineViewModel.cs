using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;

namespace ServerBrowser.Models
{
    public class TimelineViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ServerId { get; set; }

        [Required]
        [MaxLength(TimelineNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        public string?[] EventDescription { get; set; } = new string[TimelineEventsMax];

        [MaxLength(TimelineEventsMax)]
        public DateTime?[]? AddedOn { get; set; }

        [Required]
        [MaxLength(TimelineEventDescriptionMaxLen)]
        public string DescriptionsInput { get; set; } = string.Empty;

        public string? InputAddedOn { get; set; } = string.Empty;
    }
}
