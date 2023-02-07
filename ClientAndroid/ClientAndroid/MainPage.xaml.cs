using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ClientAndroid
{
    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }

        public MainPage()
        {
            InitializeComponent();

            menuList = new List<MasterPageItem>
            {
                new MasterPageItem()
                {
                    Title = "Подключение",
                    Icon = "connect.png",
                    TargetType = typeof(ConnectPage)
                },
                new MasterPageItem()
                {
                    Title = "Каталоги",
                    Icon = "catalog.png",
                    TargetType = typeof(CatalogPage)
                },
                new MasterPageItem()
                {
                    Title = "Управление",
                    Icon = "control.png",
                    TargetType = typeof(ControlPage)
                },
                new MasterPageItem()
                {
                    Title = "Сценарии",
                    Icon = "scripts.png",
                    TargetType = typeof(ScriptsPage)
                },
                new MasterPageItem()
                {
                    Title = "Процессы",
                    Icon = "process.png",
                    TargetType = typeof(ProcessPage)
                }
            };

            navigationDrawerList.ItemsSource = menuList; 
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ConnectPage)));
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}