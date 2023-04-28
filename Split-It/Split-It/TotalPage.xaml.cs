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
    public partial class TotalPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public TotalPage()
        {
            InitializeComponent();

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allItems = await firebaseHelper.GetAllItems();
            displayItems.ItemsSource = allItems;
            UpdatePersonTotals(allItems);
        }


        public void UpdatePersonTotals(List<Items> items)
        {
            var personTotals = new Dictionary<string, double>();

            foreach (var item in items)
            {
                if (item.PaidBy == null) continue;

                var splitAmount = item.ItemPrice / item.PaidBy.Length;

                foreach (var person in item.PaidBy)
                {
                    if (personTotals.ContainsKey(person.PersonName))
                    {
                        personTotals[person.PersonName] += splitAmount;
                    }
                    else
                    {
                        personTotals[person.PersonName] = splitAmount;
                    }
                }
            }

            foreach (var personTotal in personTotals)
            {
                var totalLabel = new Label
                {
                    Text = $"{personTotal.Key}: RM {personTotal.Value:F2}",
                    Margin = new Thickness(0, 0, 0, 5)
                };
                personTotalsStackLayout.Children.Add(totalLabel);
            }
        }

        private async void SaveRecordClicked(object sender, EventArgs e)
        {
            // Retrieve all items from the database
            var items = await firebaseHelper.GetAllItems();

            // Create a new record
            Record newRecord = new Record
            {
                RecordId = Guid.NewGuid().ToString(),
                ItemsList = items,
                DateCreated= DateTime.Now,
            };

            // Save the new record to the database
            await firebaseHelper.AddRecord(newRecord);

            await DisplayAlert("", "Record Saved", "Ok");
        }

    }
}