using Prism.Navigation;

namespace Afaq.IPTV.ViewModels
{
    public interface IVideoPageViewModel
    {
        string VideoSource { get; set; }

        void OnNavigatedFrom(NavigationParameters parameters);
        void OnNavigatedTo(NavigationParameters parameters);
    }
}