using SplitIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SplitIt
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RecordPage : ContentPage
{
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public RecordPage()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            recordListView.ItemsSource = await firebaseHelper.GetAllRecords();
        }

        





    }
}