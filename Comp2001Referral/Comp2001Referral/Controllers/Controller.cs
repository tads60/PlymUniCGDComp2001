using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp2001Referral.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp2001Referral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {

        private readonly access Access;

        public Controller(access Access)
        {
            this.Access = Access;
        }


        [HttpGet("{id}")]

        //validate customer
        public async Task<IActionResult> get(customers Customers)
        {
            bool existingCustomer = getValidation(Customers);

            if(existingCustomer)
            {
                return StatusCode(200); //Indicates success
            }

            else
            {
                return StatusCode(404); //Error
            }

        }

        //register customer
        public async Task<IActionResult> Post(customers Customers)
        {
            string Response;

            register(Customers, out Response);

            if (Response.Contains("200"))
            {
                return StatusCode(200); //Success
            }
            else
            {
                return StatusCode(208); //Already existing Customer
            }    
            

        }

        //update customer Details
        public async Task<IActionResult> Update(customers Customers)
        {
            bool existingCustomer = getValidation(Customers);

            if(existingCustomer)
            {
                Access.Update(Customers);
                return StatusCode(200); //Success
            }
            else
            {
                return StatusCode(404);
            }
        }

        //Delete customer
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int ID)
        {
            Access.DeleteCustomer(ID);
            return StatusCode(204); //success
        }
        
        [NonAction]

        public void register(customers Customers, out string httpResponse)
        {
            Access.Register(Customers, out httpResponse);
        }

        public bool getValidation(customers Customers)
        {
            return Access.CustomerValidation(Customers);
        }
    }
}
