using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Common;
using Guirlande.Server;
using Sockets.Plugin;
using Xamarin.Forms;

namespace GuirdServ
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private UdpSocketMulticastClient _udpSocketMulticastClient;

        public event PropertyChangedEventHandler PropertyChanged;


        private List<GuirlandeBase> _guirlandes = new List<GuirlandeBase>();
        public GuirlandeBase CurrentGuirlande { get; set; }


        public MainPage()
        {
            InitializeComponent();
            _udpSocketMulticastClient = new UdpSocketMulticastClient();
            _udpSocketMulticastClient.TTL = 5;

            _guirlandes.Add(new Guirlande3Ligne());
            _guirlandes.Add(new GuirlandePairing());

            ChoiceGuirlande_OnValueChanged(null, new ValueChangedEventArgs(0,0));


        }

        protected override async  void OnAppearing()
        {
            base.OnAppearing();
            await _udpSocketMulticastClient.JoinMulticastGroupAsync(Constante.UrlServeur, Constante.Port);

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await _udpSocketMulticastClient.DisconnectAsync();

        }

        private int _count = 0;
        private bool _continuerTimer;
        private async void StartClicked(object sender, EventArgs e)
        {

            _continuerTimer = !_continuerTimer;
            if(!_continuerTimer)// ON vient d'arreter la guirlande
                return;

            Constante.TotalLight = Int32.Parse(TotalDeviceEntry.Text);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                var msg = CurrentGuirlande.GetNextStep(_count);
                var msgBytes = Encoding.UTF8.GetBytes(msg);

                // send a message that will be received by all listening in
                // the same multicast group. 
                _udpSocketMulticastClient.SendMulticastAsync(msgBytes);
                _count += 1;
                return _continuerTimer; // True = Repeat again, False = Stop the timer
            });
          
        }


        private void ChoiceGuirlande_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            CurrentGuirlande = _guirlandes[((int) e.NewValue) % _guirlandes.Count];
            CurrentEntry.Text = CurrentGuirlande.Name;
        }
    }

}
