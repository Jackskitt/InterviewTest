using InterviewTest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InterviewTest.Tests
{
    public class PersonParseTests
    {
        [Fact]
        public void PARSE_PERSON_MALE_EXPECT_SUCCESS() {
            var testLine = "John Snow, Male, 16/03/77";
            var createdPerson = new Person(testLine);
            Assert.NotNull(createdPerson);
            Assert.Equal("John Snow", createdPerson.Name);
            Assert.Equal("Male", createdPerson.Gender);
            Assert.Equal(new DateTime(1977, 03, 16), createdPerson.Dob);
        }

        [Fact]
        public void PARSE_PERSON_FEMALE_EXPECT_SUCCESS()
        {
            var testLine = "JohnSnow, Female, 16/03/71";
            var createdPerson = new Person(testLine);
            Assert.NotNull(createdPerson);
            Assert.Equal("JohnSnow", createdPerson.Name);
            Assert.Equal("Female", createdPerson.Gender);
            Assert.Equal(new DateTime(1971, 03, 16), createdPerson.Dob);
        }

        [Fact]
        public void PARSE_PERSON_INVALID_LENGTH_EXPECT_EXCEPTION()
        {
            var testLine = "JohnSnow, Female";
            Assert.Throws<InvalidPersonFormatException>(() => new Person(testLine));
        }
        [Fact]
        public void PARSE_PERSON_NO_NAME_EXPECT_EXCEPTION()
        {
            var testLine = ", Female, 16/03/71";
            Assert.Throws<InvalidPersonFormatException>(() => new Person(testLine));        
        }

        [Fact]
        public void PARSE_PERSON_INVALID_DATE_EXPECT_EXCEPTION()
        {
            var testLine = "John Snow, Female, 216/03/71";
            Assert.Throws<InvalidPersonFormatException>(() => new Person(testLine));
        }
    }
}
