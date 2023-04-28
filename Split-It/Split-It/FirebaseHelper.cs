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
                    PaidBy = item.Object.PaidBy,

                }).ToList();
        }

        // Add a single Item

        public async Task AddItems(string name, double price, int qty, Person[] people)
        {
            await firebase
                .Child("Items")
                .PostAsync(new Items() { ItemName = name, ItemPrice = price, ItemQty = qty, PaidBy = people });
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

        // Add a single Person
        public async Task AddPerson(string name)
        {
            await firebase
                .Child("Person")
                .PostAsync(new Person() { PersonName = name });
        }

        // Add a single Record
        public async Task AddRecord(Record newRecord)
        {
            await firebase
                .Child("Records")
                .PostAsync(newRecord);
        }
        // Get list of Records
        public async Task<List<Record>> GetAllRecords()
        {
            return (await firebase
                .Child("Records")
                .OnceAsync<Record>()).Select(item => new Record
                {
                    RecordId = item.Object.RecordId,
                    ItemsList = item.Object.ItemsList,
                    DateCreated = item.Object.DateCreated
                }).OrderByDescending(record => record.DateCreated).ToList();
        }


        // Update PaidBy field in Items

        public async Task UpdateItems(string itemName, Person[] newPaidBy)
        {
            var toUpdateItem = (await firebase
              .Child("Items")
              .OnceAsync<Items>()).Where(a => a.Object.ItemName == itemName).FirstOrDefault();

            toUpdateItem.Object.PaidBy = newPaidBy;

            await firebase
                  .Child("Items")
                  .Child(toUpdateItem.Key)
                  .PutAsync(toUpdateItem.Object);

        }
        public async Task DeletePersonFromItem(string itemName, string personName)
        {
            var toUpdateItem = (await firebase
                .Child("Items")
                .OnceAsync<Items>()).Where(a => a.Object.ItemName == itemName).FirstOrDefault();

            if (toUpdateItem != null)
            {
                // Remove the person from the PaidBy list
                var updatedPaidByList = toUpdateItem.Object.PaidBy.ToList();
                updatedPaidByList.RemoveAll(p => p.PersonName == personName);

                // Update the PaidBy list of the item
                toUpdateItem.Object.PaidBy = updatedPaidByList.ToArray();

                // Update the item in the Firebase database
                await firebase
                    .Child("Items")
                    .Child(toUpdateItem.Key)
                    .PutAsync(toUpdateItem.Object);
            }
            else
            {
                throw new Exception("Item not found.");
            }
        }

        public async Task ResetDatabase()
        {
            // Delete the entire Items node
            await firebase.Child("Items").DeleteAsync();

            // Delete the entire Persons node
            await firebase.Child("Person").DeleteAsync();
        }



    }


}