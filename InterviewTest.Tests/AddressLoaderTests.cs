using InterviewTest.Models;
using InterviewTest.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InterviewTest.Tests
{
    public class AddressLoaderTests
    {
        private readonly ILogger<Person> mockLogger;
        public AddressLoaderTests()
        {
            mockLogger = new NullLogger<Person>();
        }

        [Fact]
        public async Task LOAD_FILE_EXPECT_SUCCESS()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns("/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            await addressBook.LoadAddressBook();
            Assert.True(addressBook.HasLoaded);
            var addressBookArray = (Person[])addressBook.AddressBook;
            Assert.Equal(5, addressBookArray.Length);        
        }

        [Fact]
        public async Task LOAD_FILE_EXPECT_SUCCESS_WITH_FOUR_PEOPLE()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns("/Data/Tests/InvalidAddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            await addressBook.LoadAddressBook();
            Assert.True(addressBook.HasLoaded);
            var addressBookArray = (Person[])addressBook.AddressBook;
            Assert.Equal(4, addressBookArray.Length);
            Assert.Empty(addressBookArray.Where(x => x == default(Person)));
        }

        [Fact]
        public async Task GET_ADDRESS_BOOK_EXPECT_SUCCESS()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns("/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            await addressBook.GetAddressBook();
            Assert.True(addressBook.HasLoaded);
            var addressBookArray = (Person[])addressBook.AddressBook;
            Assert.Equal(5, addressBookArray.Length);
        }
    }
}
