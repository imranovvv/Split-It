using Split_It;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplitIt
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageFlyout : ContentPage
    {
        public ListView ListView;

        public MasterPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MasterPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MasterPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterPageFlyoutMenuItem> MenuItems { get; set; }
            
            public MasterPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MasterPageFlyoutMenuItem>(new[]
                {
                    new MasterPageFlyoutMenuItem { Id = 0, Title = "Split-It",TargetType=typeof(MainPage) },
                    new MasterPageFlyoutMenuItem { Id = 1, Title = "Information", TargetType=typeof(InformationPage)},
                    new MasterPageFlyoutMenuItem { Id = 2, Title = "Record",TargetType=typeof(RecordPage) },
                    
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}