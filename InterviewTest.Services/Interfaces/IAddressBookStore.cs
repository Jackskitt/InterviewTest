using InterviewTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.Services.Interfaces
{
    public interface IAddressBookStore
    {
        /// <summary>
        /// Load the addressbook from the data source
        /// </summary>
        /// <returns></returns>
        Task LoadAddressBook();

        /// <summary>
        /// Get the address book
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Person>> GetAddressBook();
    }
}
