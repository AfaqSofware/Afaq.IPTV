using System;
using Afaq.IPTV.ViewModels;

namespace Afaq.IPTV.Views
{
    public partial class ActivationCodePage 
    {

        public ActivationCodePage()
        {
            InitializeComponent();
         
            (BindingContext as ActivationCodePageViewModel)?.Init("Default");
        }


    
    }
}
