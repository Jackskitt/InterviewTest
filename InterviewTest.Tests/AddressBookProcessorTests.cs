using InterviewTest.Models;
using InterviewTest.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace InterviewTest.Tests
{
    public class AddressBookProcessorTests
    {
        private readonly ILogger<Person> mockLogger;
        private readonly string projectDirectory;
        public AddressBookProcessorTests()
        {
            mockLogger = new NullLogger<Person>();
            projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.ToString();
        }

        [Fact]
        public async Task GET_AMOUNT_OF_MALES_EXPECT_THREE()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var amountOfMales = await processor.GetAmountOfMales();
            Assert.Equal(3, amountOfMales);
        }

        [Fact]
        public async Task GET_AMOUNT_OF_MALES_EXPECT_TWO()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/InvalidAddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var amountOfMales = await processor.GetAmountOfMales();
            Assert.Equal(2, amountOfMales);
        }

        [Fact]
        public async Task GET_AMOUNT_OF_MALES_EXPECT_EXCEPTION()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/NOBOOK");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            await Assert.ThrowsAsync<AddressBookException>(() => processor.GetAmountOfMales());
        }

        [Fact]
        public async Task FETCH_OLDEST_PERSON_EXPECT_CHUCK_JACKSON()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var oldestPerson = await processor.FetchOldestPerson();
            Assert.Equal("Chuck Jackson", oldestPerson.Name);
        }

        [Fact]
        public async Task GET_OLDEST_PERSON_EXPECT_EXCEPTION()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/NOBOOK");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            await Assert.ThrowsAsync<AddressBookException>(() => processor.FetchOldestPerson());
        }

        [Fact]
        public async Task GET_DIFFERENCE_BETWEEN_TWO_EXISTING_PEOPLE_EXPECT_YOUNGER()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var ageDifference = await processor.GetAgeDifference("John Snow", "Chuck Jackson");
            Assert.True(ageDifference.IsSuccessful);
            Assert.Equal(-945, ageDifference.Difference);
            Assert.Equal("John Snow is 945 days younger than Chuck Jackson", ageDifference.Message);
        }

        [Fact]
        public async Task GET_DIFFERENCE_BETWEEN_TWO_EXISTING_PEOPLE_EXPECT_OLDER()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var ageDifference = await processor.GetAgeDifference("Chuck Jackson", "John Snow");
            Assert.True(ageDifference.IsSuccessful);
            Assert.Equal(945, ageDifference.Difference);
            Assert.Equal("Chuck Jackson is 945 days older than John Snow", ageDifference.Message);
        }

        [Fact]
        public async Task GET_DIFFERENCE_BETWEEN_PEOPLE_FIRST_MISSING_PERSON_EXPECT_FAILURE()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var ageDifference = await processor.GetAgeDifference("Bob the Builder","John Snow");
            Assert.False(ageDifference.IsSuccessful);
            Assert.Equal("Bob the Builder does not exist in the database", ageDifference.Message);
        }

        [Fact]
        public async Task GET_DIFFERENCE_BETWEEN_PEOPLE_SECOND_MISSING_PERSON_EXPECT_FAILURE()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var ageDifference = await processor.GetAgeDifference("John Snow","Bob the Builder");
            Assert.False(ageDifference.IsSuccessful);
            Assert.Equal("Bob the Builder does not exist in the database", ageDifference.Message);
        }

        [Fact]
        public async Task GET_DIFFERENCE_BETWEEN_PEOPLE_BOTH_MISSING_PERSON_EXPECT_FAILURE()
        {
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x["BookLocation"]).Returns(projectDirectory + "/Data/AddressBook");

            var addressBook = new AddressBookStore(mockLogger, mockConfig.Object);
            var processor = new AddressBookProcessor(addressBook);
            var ageDifference = await processor.GetAgeDifference("Ted", "Bob the Builder");
            Assert.False(ageDifference.IsSuccessful);
            Assert.Equal("Ted does not exist in the database", ageDifference.Message);
        }
    }
}
