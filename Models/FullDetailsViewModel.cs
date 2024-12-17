using System.ComponentModel.DataAnnotations;
using static ServerBrowser.Constants;

namespace ServerBrowser.Models
{
    public class FullDetailsViewModel
    {
        public ServerViewModel? serverViewModel;

        public List<ReviewViewModel>? reviewViewModels = new();

        public List<ListViewModel>? listViewModels = new();

        public List<AnnouncementViewModel>? announcementViewModels = new();
    }
}
