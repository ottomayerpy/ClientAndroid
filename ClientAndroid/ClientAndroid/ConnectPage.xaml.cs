using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClientAndroid
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnectPage : ContentPage
	{
		public ConnectPage()
		{
            InitializeComponent();
            TxtIP.Text = Connect.IP;
            TxtPort.Text = Connect.Port.ToString();
            LabelLog.Text = Connect.Log;
        }

        private void ButtonStart_Clicked(object sender, EventArgs e)
        {
            LabelLog.Text = Connect.Log = Connect.Message(TxtCommand.Text);
            TxtCommand.Text = null;
        }

        private void TxtIP_TextChanged(object sender, TextChangedEventArgs e) => Connect.IP = TxtIP.Text;

        private void TxtPort_TextChanged(object sender, TextChangedEventArgs e) => Connect.Port = Convert.ToInt32(TxtPort.Text);
    }
}