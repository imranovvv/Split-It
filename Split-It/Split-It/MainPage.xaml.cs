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




        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
                return;

            var itemSelected = e.SelectedItem as Items;
            if (personSelected != null)
            {
                AssignPersonToItem(itemSelected.ItemName, personSelected.PersonName);
            }
            else
            {
                DisplayAlert("Error", "No Person Chosen", "OK");

                ((ListView)sender).SelectedItem = null;
            }
        }



        Person personSelected;
        private void OnPersonSelected(object sender, SelectionChangedEventArgs e)
        {
            // Store person selected
            personSelected = e.CurrentSelection.FirstOrDefault() as Person;
            if (personSelected != null)
            {
                DisplayAlert("Person", $"{personSelected.PersonName}", "Ok");


            }
        }

        private async void AssignPersonToItem(string itemname, string personName)
        {
            // Get the current item from Firebase
            var currentItem = (await firebaseHelper.GetAllItems()).FirstOrDefault(item => item.ItemName == itemname);

            
                // Check if the person already exists in the database
                var existingPerson = (await firebaseHelper.GetAllPersons()).FirstOrDefault(person => person.PersonName == personName);

                
                    // Check if the person is already in the PaidBy list
                    if (currentItem.PaidBy != null && currentItem.PaidBy.Any(person => person.PersonName == existingPerson.PersonName))
                    {
                        await DisplayAlert("Error", "Person already added to the PaidBy list", "OK");
                    }
                    else
                    {
                        // Add the existing person to the current PaidBy list
                        List<Person> updatedPaidByList;

                        if (currentItem.PaidBy == null || currentItem.PaidBy.Length == 0)
                        {
                            updatedPaidByList = new List<Person> { existingPerson };
                        }
                        else
                        {
                            updatedPaidByList = new List<Person>(currentItem.PaidBy) { existingPerson };
                        }

                        // Update the item in the Firebase database
                        await firebaseHelper.UpdateItems(itemname, updatedPaidByList.ToArray());

                        await DisplayAlert("Record Saved", "Update Successful", "OK");
                    }
               

                displayItems.ItemsSource = await firebaseHelper.GetAllItems();
                personList.ItemsSource = await firebaseHelper.GetAllPersons();
                personSelected=null;
        }







    }
}