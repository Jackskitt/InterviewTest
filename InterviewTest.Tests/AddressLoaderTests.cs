using InterviewTest.Models;
using InterviewTest.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InterviewTest.Tests
{
    /// <summary>
    /// Async tests will fail if all run at once due to the async test runner
    /// </summary>
    public class AddressLoaderTests
    {
        private readonly ILogger<Person> mockLogger;
        private readonly string projectDirectory;
        public AddressLoaderTests()
        {
            mockLogger = new NullLogger<Person>();
            projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.ToString();
        }

        [Fact]
        public async Task LOAD_FILE_EXPECT_SUCCESS()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory +"/Data/AddressBook");

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
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory+ "/Data/InvalidAddressBook");

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
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory+ "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            await addressBook.GetAddressBook();
            Assert.True(addressBook.HasLoaded);
            var addressBookArray = (Person[])addressBook.AddressBook;
            Assert.Equal(5, addressBookArray.Length);
        }
    }
}
