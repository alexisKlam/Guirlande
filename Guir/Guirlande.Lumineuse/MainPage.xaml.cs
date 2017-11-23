using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Xamarin.Forms;

namespace Guirlande.Lumineuse
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            urlEntry.Text = Constante.UrlServeur;
            portEntry.Text = Constante.Port.ToString();
        }

        private void BeginLighCclik(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new GuirlandePage(urlEntry.Text,Convert.ToInt32(positionEntry.Text), Convert.ToInt32(portEntry.Text)));
        }
    }
}
