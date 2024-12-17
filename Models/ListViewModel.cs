using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;

namespace ServerBrowser.Models
{
    public class ListViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ReviewTitleMinLen)]
        [MaxLength(ReviewTitleMaxLen)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(ReviewDescMaxLen)]
        public string? Description { get; set; } = string.Empty;

        [MaxLength(PublisherMaxLen)]
        public string? PublisherId { get; set; } = string.Empty!;

        public DateTime? AddedOn { get; set; }

        [Required]
        [MinLength(ListIdsStringMinLen)]
        [MaxLength(ListIdsStringMaxLen)]
        public string ServerIds { get; set; } = string.Empty;
    }
}
