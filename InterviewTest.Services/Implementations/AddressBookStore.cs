using InterviewTest.Models;
using InterviewTest.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTest.Services.Implementations
{
    public class AddressBookStore : IAddressBookStore
    {
        private string bookLocation;
        public bool HasLoaded;

        private readonly ILogger<Person> logger;
        private readonly IConfiguration configuration;

        public IEnumerable<Person> AddressBook { get; set; }

        public AddressBookStore(ILogger<Person> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            bookLocation = configuration["BookLocation"];
        }

        /// <summary>
        /// Fetch the address book, if the address book is null and it hasn't been loaded
        /// we try to load it again, if that fails we return default
        /// </summary>
        /// <returns>default or Person[]</returns>
        public async Task<IEnumerable<Person>> GetAddressBook()
        {

            if (AddressBook == default(IEnumerable<Person>))
            {
                if (HasLoaded)
                    return default(IEnumerable<Person>);

                //Try to load the book as it hasn't been loaded yet
                await LoadAddressBook();
                if (AddressBook == default(IEnumerable<Person>))
                    return default(IEnumerable<Person>);
            }

            return AddressBook;

        }

        /// <summary>
        /// Proccess a line of the file
        /// </summary>
        /// <param name="lineRead"></param>
        /// <returns></returns>
        private async Task<Person> ProcessLine(Task<string> lineRead)
        {
            var fileLine = await lineRead;
            try
            {
                return new Person(fileLine);
            }
            catch (InvalidPersonFormatException e)
            {
                logger.LogError(e, "Couldn't parse person");
                return default(Person);
            }
        }

        /// <summary>
        /// Load the address book from a file line by line and then proccesses the line into people
        /// Were not doing the processing here and storing it, because that's boring and not much of a test
        /// </summary>
        /// <returns></returns>
        public async Task LoadAddressBook()
        {
            var personsTask = new List<Task<Person>>();
            if (!File.Exists(bookLocation))
                return;

            using (var addressBookStream = new StreamReader(bookLocation))
            {
                while (!addressBookStream.EndOfStream)
                {
                    var lineReadTask = addressBookStream.ReadLineAsync();
                    personsTask.Add(ProcessLine(lineReadTask));
                }
            }
            var parsedPersons = await Task.WhenAll(personsTask);
            AddressBook = parsedPersons.Where(x => x != default(Person)).OrderBy(x => x.Dob).ToArray();
            HasLoaded = true;
        }
    }
}
