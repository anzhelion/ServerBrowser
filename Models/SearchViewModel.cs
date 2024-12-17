using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;

namespace ServerBrowser.Models
{
    public class SearchViewModel
    {
        public string Match { get; set; } = string.Empty;
    }
}
