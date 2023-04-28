using SplitIt.Models;
using System;
using System.Collections.Generic;


using Xamarin.Forms;

namespace SplitIt
{	
	public partial class AddItemPage : ContentPage
	{

        FirebaseHelper firebaseHelper = new FirebaseHelper();


        public AddItemPage ()
		{
			InitializeComponent ();
		}
		async void ItemOnSave(object sender, EventArgs e)
        {
            string itemname = inputitemname.Text;
            var itemprice = Double.Parse(inputprice.Text);
            var itemqty = int.Parse(inputqty.Text);

            Person[] people = new Person[10];

            await firebaseHelper.AddItems(itemname, itemprice, itemqty, people);

            await DisplayAlert("", "Item has been added", "OK");

            await Navigation.PopAsync();

        }

        
    }
}

