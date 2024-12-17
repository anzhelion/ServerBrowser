using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;

namespace ServerBrowser.Models
{
    public class SearchViewModel
    {
        [MaxLength(ServerTitleMaxLen)]
        public string Match { get; set; } = string.Empty;
    }
}
