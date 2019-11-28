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
    }
}
