using System;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Essentials;
using SplitIt.Models;

namespace SplitIt
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://split-it-6b887-default-rtdb.asia-southeast1.firebasedatabase.app");

        // Get list of Items
        public async Task<List<Items>> GetAllItems()
        {
            return (await firebase
                .Child("Items")
                .OnceAsync<Items>()).Select(item => new Items
                {
                    ItemName = item.Object.ItemName,
                    ItemPrice = item.Object.ItemPrice,
                    ItemQty = item.Object.ItemQty,


                }).ToList();
        }

        // Add a single record of Items

        public async Task AddRecord(string name, double price, int qty)
        {
            await firebase
                .Child("Items")
                .PostAsync(new Items() { ItemName = name, ItemPrice = price, ItemQty = qty });
        }

        // Get list of Persons
        public async Task<List<Person>> GetAllPersons()
        {
            return (await firebase
                .Child("Person")
                .OnceAsync<Person>()).Select(persons => new Person
                {
                    PersonName = persons.Object.PersonName,

                }).ToList();
        }

        // Add a single record of Person
        public async Task AddPerson(string name)
        {
            await firebase
                .Child("Person")
                .PostAsync(new Person() { PersonName = name });
        }

    }


}