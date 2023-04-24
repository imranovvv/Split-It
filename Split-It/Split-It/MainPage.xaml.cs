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
using Split;

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
        private void GoToItemPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddItemPage());
        }



        /*async void ItemOnSave(object sender, EventArgs e)
        {
            string itemname = inputitemname.Text;
            var itemprice = Double.Parse(inputprice.Text);
            var itemqty = int.Parse(inputqty.Text);

            await firebaseHelper.AddRecord(itemname, itemprice, itemqty);

            await DisplayAlert("Record Saved", "Item has been added", "OK");

            // Update the ListView with the new data
            displayItems.ItemsSource = await firebaseHelper.GetAllItems();
        }

        async void PersonOnSave(object sender, EventArgs e)
        {
            string personName = inputname.Text;
            

            await firebaseHelper.AddPerson(personName);

            await DisplayAlert("Record Saved", "Person has been added", "OK");

            // Update the ListView with the new data
            personList.ItemsSource = await firebaseHelper.GetAllPersons();
        }*/
    }
}