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

            await firebaseHelper.AddRecord(itemname, itemprice, itemqty);

            await DisplayAlert("Record Saved", "Item has been added", "OK");

            await Navigation.PopAsync();

        }
    }
}

