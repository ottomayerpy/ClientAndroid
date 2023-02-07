using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System;

namespace ClientAndroid
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CatalogPage : ContentPage
	{
        List<string> Catalog = new List<string>();
        List<string> Files = new List<string>();
        bool mode = true; // true - Catalogs; false - Files;

        public CatalogPage()
		{
			InitializeComponent();
            UpdateCatalog(null);
            BindingContext = this;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
                LabelSelectItem.Text = e.Item.ToString();
            ((ListView)sender).SelectedItem = null;
        }

        private void UpdateFiles(string path)
        {
            string content = Connect.Message("$F" + path);
            Files.Clear();

            try
            {
                while (true) // В цикле заведомо предусмотренно исключение, возникающее когда заканчиваются строки для считывания
                {
                    int index = content.IndexOf("|");
                    string message = content.Substring(0, index);
                    content = content.Remove(0, index + 1);

                    if (path != null)
                    {
                        int index2 = message.LastIndexOf("\\");
                        message = message.Remove(0, index2 + 1);
                    }

                    Files.Add(message);
                }
            }
            catch
            {
                Files.Sort();
                ListView.ItemsSource = Files;
            }
        }

        private void UpdateCatalog(string path)
        {
            string content = Connect.Message("$C" + path);
            Catalog.Clear();

            try
            {
                while (true) // В цикле заведомо предусмотренно исключение, возникающее когда заканчиваются строки для считывания
                {
                    int index = content.IndexOf("|");
                    string message = content.Substring(0, index);
                    content = content.Remove(0, index + 1);

                    if (path != null)
                    {
                        int index2 = message.LastIndexOf("\\");
                        message = message.Remove(0, index2 + 1);
                    }

                    Catalog.Add(message);
                }
            } catch { }

            if (path != null)
                if (path.Substring(path.Length - 1) != "\\")
                    path += "\\";

            LabelCatalog.Text = path;
            Catalog.Sort();
            ListView.ItemsSource = Catalog;
        }

        private async void AlertButton_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet();
            //actionLabel.Text = action;
        }

        private void ItemCreate_Clicked(object sender, EventArgs e)
        {
            
            //if (mode)
            //Connect.Log = Connect.Message("%DC");
            //else
            //Connect.Log = Connect.Message("%FC");
        }

        private void ItemDelete_Clicked(object sender, EventArgs e)
        {
            if (mode)
                Connect.Log = Connect.Message("%DD" + LabelCatalog.Text + LabelSelectItem.Text);
            else
                Connect.Log = Connect.Message("%FD" + LabelCatalog.Text + LabelSelectItem.Text);
        }

        private void ItemMove_Clicked(object sender, EventArgs e)
        {
            //if (mode)
                //Connect.Log = Connect.Message("%DM" + LabelSelectItem.Text);
            //else
                //Connect.Log = Connect.Message("%FM" + LabelSelectItem.Text);
        }

        private void ItemChangeMode_Clicked(object sender, EventArgs e)
        {
            if (ItemChangeMode.Text == "Каталоги")
            {
                ItemChangeMode.Text = "Файлы";
                mode = false;
                ButtonOpen.IsEnabled = false;
                ButtonBack.IsEnabled = false;
                UpdateFiles();
            }
            else
            {
                ItemChangeMode.Text = "Каталоги";
                mode = true;
                ButtonOpen.IsEnabled = true;
                ButtonBack.IsEnabled = true;
                UpdateCatalog();
            }
        }

        private void ButtonBack_Clicked(object sender, EventArgs e)
        {
            if (LabelCatalog.Text.Length > 3)
            {
                LabelCatalog.Text = LabelCatalog.Text.Remove(LabelCatalog.Text.LastIndexOf("\\") - 1);
                LabelCatalog.Text = LabelCatalog.Text.Remove(LabelCatalog.Text.LastIndexOf("\\"));

                if (LabelCatalog.Text.Substring(LabelCatalog.Text.Length - 1) == ":")
                    LabelCatalog.Text += "\\";

                UpdateCatalog(LabelCatalog.Text);
            }
            else
                UpdateCatalog(null);
        }

        private void ButtonOpen_Clicked(object sender, EventArgs e)
        {
            UpdateCatalog(LabelCatalog.Text + LabelSelectItem.Text);
        }
    }
}