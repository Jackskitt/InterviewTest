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
    }
}
