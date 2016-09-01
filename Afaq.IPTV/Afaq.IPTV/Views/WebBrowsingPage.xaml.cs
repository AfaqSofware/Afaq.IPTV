namespace Afaq.IPTV.Views
{
    public partial class WebBrowsingPage
    {
        private readonly string _url;

        public WebBrowsingPage(string url)
        {
            _url = url;
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            MyWebView.HeightRequest = Height/1.25;
            MyWebView.Source = _url;
            base.OnSizeAllocated(width, height);
        }

        protected override bool OnBackButtonPressed()
        {
            if (MyWebView.CanGoBack)
            {
                MyWebView.GoBack();
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}