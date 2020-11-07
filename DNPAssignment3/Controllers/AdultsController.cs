using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assignment2.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DNPAssignment3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdultsController : ControllerBase
    {
        private IAdultService AdultService;

        public AdultsController(IAdultService adultService)
        {
            this.AdultService = adultService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetAdults()
        {
            try
            {
                IList<Adult> adults = await AdultService.GetAdultsAsync();
                return Ok(adults);
            }
            catch (Exception e)
            {
                Console.Write(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromRoute] int id) {
            try {
                await AdultService.RemoveAdultAsync(id);
                return Ok();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
                Adult added = await AdultService.AddAdultAsync(adult);
                return Created("added",added);
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}