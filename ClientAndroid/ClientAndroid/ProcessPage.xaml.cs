using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace ClientAndroid
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProcessPage : ContentPage
	{
        List<string> data = new List<string>();

        public ProcessPage()
		{
			InitializeComponent();
            UpdateProcessList();
            BindingContext = this;
        }

        private void ProcessListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
                LabelProcess.Text = e.Item.ToString();
            ((ListView)sender).SelectedItem = null;
        }

        private void UpdateProcessList()
        {
            string text = Connect.Message("$P");
            data.Clear();

            try
            {
                while (true) // В цикле заведомо предусмотренно исключение, возникающее когда заканчиваются строки для считывания
                {
                    int index = text.IndexOf("|");
                    string message = text.Substring(0, index);
                    text = text.Remove(0, index + 1);
                    data.Add(message);
                }
            }
            catch { }

            data.Sort();
            ProcessListView.ItemsSource = data;
        }

        private void ButtonClose_Clicked(object sender, System.EventArgs e)
        {
            Connect.Log = Connect.Message("$K" + LabelProcess.Text);
            UpdateProcessList();
        }
    }
}