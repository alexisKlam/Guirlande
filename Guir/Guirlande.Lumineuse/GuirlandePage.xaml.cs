using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Sockets.Plugin;
using Sockets.Plugin.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Guirlande.Lumineuse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuirlandePage : ContentPage
    {
        private string _urlServeur;
        private int _position;
        private int _port;
        private IPlatformService _platformService;
        private UdpSocketMulticastClient _udpSocketMulticastClient;

        public GuirlandePage(string urlServeur, int position, int port)
        {
            _urlServeur = urlServeur;
            _position = position;
            _port = port;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _platformService = DependencyService.Get<IPlatformService>();
            if(Device.RuntimePlatform == Device.Android)
               _platformService.HideTabBar();

            _udpSocketMulticastClient = new UdpSocketMulticastClient();
            _udpSocketMulticastClient.TTL = 5;



        }

        protected override async  void OnAppearing()
        {
            base.OnAppearing();


            _udpSocketMulticastClient.MessageReceived += UdpSocketMulticastClientOnMessageReceived;

            await _udpSocketMulticastClient.JoinMulticastGroupAsync(_urlServeur, _port);

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            _udpSocketMulticastClient.MessageReceived -= UdpSocketMulticastClientOnMessageReceived;
            await _udpSocketMulticastClient.DisconnectAsync();

        }

        private void UdpSocketMulticastClientOnMessageReceived(object sender, UdpSocketMessageReceivedEventArgs args)
        {
            try
            {
                var from = String.Format("{0}:{1}", args.RemoteAddress, args.RemotePort);
                var data = Encoding.UTF8.GetString(args.ByteData, 0, args.ByteData.Length);
                var guirlande =  data.Split(';');

                if (guirlande != null)
                {
                    var entry = guirlande[_position];
                    EntryGuirlande.BackgroundColor = Color.FromHex(entry);
                }
                Debug.WriteLine("{0} - {1}", from, data);
            }
            catch (Exception e)
            {
                
                //throw;
            }
        }


    }
}