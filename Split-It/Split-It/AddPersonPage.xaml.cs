using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SplitIt
{	
	public partial class AddPersonPage : ContentPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public AddPersonPage ()
		{
			InitializeComponent ();
		}

        async void PersonOnSave(object sender, EventArgs e)
        {
            string personName = inputname.Text;


            await firebaseHelper.AddPerson(personName);

            await DisplayAlert("Record Saved", "Person has been added", "OK");

            await Navigation.PopAsync();

        }
    }
}

