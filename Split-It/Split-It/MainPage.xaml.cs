using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplitIt;
using Xamarin.Forms;
using SplitIt.Models;
using Xamarin.Essentials;

namespace Split_It
{
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public MainPage()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            displayItems.ItemsSource = await firebaseHelper.GetAllItems();
            personList.ItemsSource= await firebaseHelper.GetAllPersons();
        }
        private async void GoToItemPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddItemPage());
        }

        private async void GoToPersonPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPersonPage());
        }

        
    }
}