using InterviewTest.Models;
using InterviewTest.Models.Dtos;
using InterviewTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.Services.Implementations
{
    public class AddressBookProcessor : IAddressBookProcessor
    {
        private readonly IAddressBookStore addressBookStore;

        public AddressBookProcessor(IAddressBookStore addressBookStore)
        {
            this.addressBookStore = addressBookStore;
        }

        /// <summary>
        /// Sorts the address book list by age and gets the first result
        /// We do this also on load but again i'm making the methods not specific to the file store
        /// </summary>
        /// <returns></returns>
        public async Task<Person> FetchOldestPerson()
        {
            var addressBook = await LoadAddressBook();

            var sortedAddressBook = addressBook.OrderBy(x => x.Dob);
            return sortedAddressBook.First();
        }

        /// <summary>
        /// Load the address book and throw an exception if it's empty
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<Person>> LoadAddressBook()
        {
            var addressBook = await addressBookStore.GetAddressBook();
            if (addressBook == default(IEnumerable<Person>))
                throw new AddressBookException("Couldn't load address book");
            return addressBook;
        }

        /// <summary>
        /// Get the address book and count the amount of males
        /// (NOTE: This could probably just be done when we load the address book but i'm writing it as if the data source is unknown)
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetAmountOfMales()
        {
            var addressBook = await LoadAddressBook();

            return addressBook.Count(x => x.Gender.Equals("Male"));
        }

        /// <summary>
        /// Gets the age difference in days between two people
        /// Bill and Paul don't exist in the database i'm 90% certain this is on purpose, so it will return a message if the users don't exist
        /// </summary>
        /// <param name="person"></param>
        /// <param name="toComparePerson"></param>
        /// <returns></returns>
        public async Task<PersonAgeDifferenceResponse> GetAgeDifference(string person, string toComparePerson)
        {
            var addressBook = await LoadAddressBook();
            var firstPersonObject = addressBook.FirstOrDefault(x => x.Name == person);
            if (firstPersonObject == default(Person))
                return new PersonAgeDifferenceResponse($"{person} does not exist in the database");

            var toComparePersonObject = addressBook.FirstOrDefault(x => x.Name == toComparePerson);
            if (toComparePersonObject == default(Person))
                return new PersonAgeDifferenceResponse($"{toComparePerson} does not exist in the database");

            var difference = toComparePersonObject.Dob -firstPersonObject.Dob;
            return new PersonAgeDifferenceResponse(difference.Days, person, toComparePerson);
        }
    }
}
