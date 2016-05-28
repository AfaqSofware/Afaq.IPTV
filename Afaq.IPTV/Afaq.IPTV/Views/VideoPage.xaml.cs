using System.Diagnostics;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class VideoPage
    {
        public VideoPage()
        {
            InitializeComponent();
        }


        private void VideoPage_OnFocused(object sender, FocusEventArgs e)
        {
            Debug.WriteLine("####### Player Got focus #######");
        }
    }
}