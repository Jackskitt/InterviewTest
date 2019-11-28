using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewTest.Models.Dtos
{
    public class PersonAgeDifferenceResponse
    {
        public PersonAgeDifferenceResponse()
        {
        }

        public PersonAgeDifferenceResponse(string message, bool isSuccessful = false)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public PersonAgeDifferenceResponse(int difference, string firstPersonName, string secondPersonName, bool isSuccessful = true)
        {
            IsSuccessful = isSuccessful;
            Difference = difference;
            Message = GetAgeDifferenceString(firstPersonName, secondPersonName);
        }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public int Difference { get; set; }

        public string GetAgeDifferenceString(string firstPerson, string secondPerson)
        {
            if (Difference > 0)
                return $"{firstPerson} is {Difference} days older than {secondPerson}";

            return $"{firstPerson} is {Difference*-1} days younger than {secondPerson}";
        }
    }
}
