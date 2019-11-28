using InterviewTest.Models;
using InterviewTest.Models.Dtos;
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

        Task<PersonAgeDifferenceResponse> GetAgeDifference(string person, string toComparePerson);
    }
}
