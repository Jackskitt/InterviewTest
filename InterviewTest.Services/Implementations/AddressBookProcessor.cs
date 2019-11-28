using InterviewTest.Models;
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
        /// Get the address book and count the amount of males
        /// (NOTE: This could probably just be done when we load the address book but i'm writing it as if the data source is unknown)
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetAmountOfMales()
        {
            var addressBook = await addressBookStore.GetAddressBook();
            if (addressBook == default(IEnumerable<Person>))
                throw new AddressBookException("Couldn't load address book");

            return addressBook.Count(x => x.Gender.Equals("Male"));
        }
    }
}
