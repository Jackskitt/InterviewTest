using InterviewTest.Models;
using InterviewTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InterviewTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookProcessor addressBookProcessor;
        private readonly ILogger<AddressBookController> logger;

        public AddressBookController(IAddressBookProcessor addressBookProcessor, ILogger<AddressBookController> logger)
        {
            this.addressBookProcessor = addressBookProcessor;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the amount of men within the address book
        /// </summary>
        /// <returns>int</returns>
        [HttpGet("AmountOfMales")]
        public async Task<IActionResult> GetAmountOfMales()
        {
            try
            {
                var maleCount = await addressBookProcessor.GetAmountOfMales();
                return Ok(maleCount);
            }
            catch (AddressBookException)
            {
                return BadRequest("We've couldn't load the address book");
            }
            catch (Exception e)
            {
                logger.LogCritical(e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Fetches the oldest person from the address book store
        /// </summary>
        /// <returns>A person object</returns>
        [HttpGet("GetOldestPerson")]
        public async Task<IActionResult> GetOldestPerson()
        {
            try
            {
                var oldestPerson = await addressBookProcessor.FetchOldestPerson();
                return Ok(oldestPerson);
            }
            catch (AddressBookException)
            {
                return BadRequest("We've couldn't load the address book");
            }
            catch (Exception e)
            {
                logger.LogCritical(e, e.Message);
                throw;
            }
        }

        /// <summary>
        /// Compares two people by name to fetch the age difference
        /// </summary>
        /// <param name="firstPerson">Person you want to check the difference</param>
        /// <param name="toCompare">Who to compare to </param>
        /// <returns>A human readable message</returns>
        [HttpGet("GetAgeDifference")]
        public async Task<IActionResult> GetAgeDifference([FromQuery] string firstPerson, [FromQuery] string toCompare)
        {
            if (string.IsNullOrEmpty(firstPerson))
                return BadRequest("Please enter a name for the first person");

            if (string.IsNullOrEmpty(toCompare))
                return BadRequest("Please enter a name for the second person");

            try
            {
                var ageDifferenceResult = await addressBookProcessor.GetAgeDifference(firstPerson, toCompare);
                if (ageDifferenceResult.IsSuccessful)
                    return Ok(ageDifferenceResult.Message);

                return BadRequest(ageDifferenceResult.Message);
            }
            catch (AddressBookException)
            {
                return BadRequest("We've couldn't load the address book");
            }
            catch (Exception e)
            {
                logger.LogCritical(e, e.Message);
                throw;
            }
        }
    }
}
