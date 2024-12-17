using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using static ServerBrowser.Constants;

namespace ServerBrowser.Data.Models
{
    public class ServerList
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(ListTitleMaxLen)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(ListDescMaxLen)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(PublisherMaxLen)]
        public string? PublisherId { get; set; } = string.Empty!;

        public DateTime? AddedOn { get; set; }

        [MaxLength(ListIdsMaxLen)]
        public int[] ServerIds { get; set; } = new int[ListIdsMaxLen];
    }
}
