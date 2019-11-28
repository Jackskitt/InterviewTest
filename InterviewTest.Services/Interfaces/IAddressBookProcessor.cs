using InterviewTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.Services.Interfaces
{
    public interface IAddressBookProcessor
    {
        Task<int> GetAmountOfMales();

        Task<Person> FetchOldestPerson();
    }
}
