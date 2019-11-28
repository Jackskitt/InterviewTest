using System;

namespace InterviewTest.Models
{
    public class Person
    {


        public Person(string fileLine, char delim = ',')
        {
            ParsePerson(fileLine, delim);
        }

        public void ParsePerson(string fileLine, char delim)
        {
            var contents = fileLine.Split(delim);
            if (contents.Length != 3)
                throw new InvalidPersonFormatException("Person line does not have 3 variables");

            if (string.IsNullOrEmpty(contents[0]))
                throw new InvalidPersonFormatException("Person does not have a name");

            Name = contents[0].Trim();
            Gender = contents[1].Trim();

            if (DateTime.TryParse(contents[2].Trim(), out var dateOfBirth))
            {
                Dob = dateOfBirth;
            }
            else
            {
                throw new InvalidPersonFormatException("Invalid Date of birth");
            }
        }

        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime Dob { get; set; }
    }
}
