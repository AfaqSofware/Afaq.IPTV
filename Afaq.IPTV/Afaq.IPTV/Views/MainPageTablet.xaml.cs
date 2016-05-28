using System;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class MainPageTablet
    {
        public MainPageTablet()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, "Enter", OnEnter);
            NavigationPage.SetHasNavigationBar(Detail, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void OnEnter(object obj)
        {
            if (IsPresented)
            {
                IsPresented = false;
            }
            else
            {
                IsPresented = true;
            }
            //if (Master.IsVisible)
            //{
            //    this.Master.IsVisible = false;

            //}
            //else
            //{
            //    Master.IsVisible = true;
            //}
        }

        public override bool ShouldShowToolbarButton()
        {
            // Hide toolbar button on Windows platforms.
            return false;
        }

        private void OnMasterTapped(object sender, EventArgs args)
        {
            // Catch exceptions when setting IsPresented in split mode.
            try
            {
                IsPresented = false;
            }
            catch
            {
            }
        }

        private void OnDetailTapped(object sender, EventArgs args)
        {
            IsPresented = true;
        }
    }
}