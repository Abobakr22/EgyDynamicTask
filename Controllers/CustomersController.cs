using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EgyDynamicTask.Data;
using EgyDynamicTask.Models;
using EgyDynamicTask.Dtos.CustomerDtos;

namespace EgyDynamicTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET all Customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _context.Customers
                            .Select(c => new GetCustomersDto
                            {
                                CustomerId = c.Id,
                                CustomerName = c.CustomerName,
                                Residence = c.Residence,
                                Description = c.Description,
                                Job = c.Job,
                                EnteredBy = c.EnteredBy,
                                EnteredDate = c.EntryDate,
                                LastModifiedBy = c.LastModifiedBy,
                                LastModifiedDate = c.LastModifiedDate,
                                CustomerSource = c.CustomerSource,
                                CustomerClassification = c.CustomerClassification,
                                CustomerAddress = c.CustomerAddress,
                                FirstPhone = c.Phone1,
                                SecondaryPhone = c.Phone2,
                                WhatsApp = c.Whatsapp,
                                Email = c.Email,
                                CustomerCode = c.CustomerCode,
                                Nationality = c.Nationality,
                                SalesManName = c.SalesMan.Name,

                            })
                            .ToListAsync();

            return Ok(customers);
        }

        // GET customer by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _context.Customers.Where(c => c.Id == id)
                 .Select(c => new GetCustomersDto
                 {
                     CustomerId = c.Id,
                     CustomerName = c.CustomerName,
                     Residence = c.Residence,
                     Description = c.Description,
                     Job = c.Job,
                     EnteredBy = c.EnteredBy,
                     EnteredDate = c.EntryDate,
                     LastModifiedBy = c.LastModifiedBy,
                     LastModifiedDate = c.LastModifiedDate,
                     CustomerSource = c.CustomerSource,
                     CustomerClassification = c.CustomerClassification,
                     CustomerAddress = c.CustomerAddress,
                     FirstPhone = c.Phone1,
                     SecondaryPhone = c.Phone2,
                     WhatsApp = c.Whatsapp,
                     Email = c.Email,
                     CustomerCode = c.CustomerCode,
                     Nationality = c.Nationality,
                     SalesManName = c.SalesMan.Name
                 }).FirstOrDefaultAsync();

            if(customer == null)
            {
                return NotFound($"no customer found with id = {id}");
            }

            return Ok(customer);
        }

        //create a new customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto CreateCustomerDto)
        {
            // check existence of SalesManId 
            var salesman = await _context.SalesMen.FindAsync(CreateCustomerDto.SalesmanId);
            if(salesman == null)
            {
                return BadRequest("Invalid SalesmanId.");
            }

            var customer = new Customer
            {
                CustomerName = CreateCustomerDto.CustomerName,
                Residence = CreateCustomerDto.Residence,
                Job = CreateCustomerDto.Job,
                Description = CreateCustomerDto.Description,
                Nationality = CreateCustomerDto.Nationality,
                CustomerAddress = CreateCustomerDto.CustomerAddress,
                CustomerClassification = CreateCustomerDto.CustomerClassification,
                CustomerCode = CreateCustomerDto.CustomerCode,
                CustomerSource = CreateCustomerDto.CustomerSource,
                EnteredBy = CreateCustomerDto.EnteredBy,
                EntryDate = CreateCustomerDto.EnteredDate,
                LastModifiedBy = CreateCustomerDto.LastModifiedBy,
                LastModifiedDate = CreateCustomerDto.LastModifiedDate,
                Phone1 = CreateCustomerDto.FirstPhone,
                Phone2 = CreateCustomerDto.SecondaryPhone,
                Whatsapp = CreateCustomerDto.WhatsApp,
                Email = CreateCustomerDto.Email,
                SalesManId = CreateCustomerDto.SalesmanId
            };

            await _context.Customers.AddAsync(customer);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(customer);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        // edit existed customer by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
                return NotFound($"No customer found with id = {id}");

            customer.CustomerName = updateCustomerDto.CustomerName;
            customer.Residence = updateCustomerDto.Residence;
            customer.Job = updateCustomerDto.Job;
            customer.Description = updateCustomerDto.Description;
            customer.Nationality = updateCustomerDto.Nationality;
            customer.CustomerAddress = updateCustomerDto.CustomerAddress;
            customer.CustomerClassification = updateCustomerDto.CustomerClassification;
            customer.CustomerCode = updateCustomerDto.CustomerCode;
            customer.CustomerSource = updateCustomerDto.CustomerSource;
            customer.EnteredBy = updateCustomerDto.EnteredBy;
            customer.EntryDate = updateCustomerDto.EnteredDate;
            customer.LastModifiedBy = updateCustomerDto.LastModifiedBy;
            customer.LastModifiedDate = updateCustomerDto.LastModifiedDate;
            customer.Phone1 = updateCustomerDto.FirstPhone;
            customer.Phone2 = updateCustomerDto.SecondaryPhone;
            customer.Whatsapp = updateCustomerDto.WhatsApp;
            customer.Email = updateCustomerDto.Email;

            _context.SaveChanges();
            return Ok(customer);
        }

        //delete customer by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound("no customer found with that id");
            }

            // Set Customer_id to null in it's related calls so it can be removed after that
            var relatedCalls = _context.Calls.Where(c => c.CustomerId == id);
            foreach(var call in relatedCalls)
            {
                call.CustomerId = null;
            }
            await _context.SaveChangesAsync(); // Save changes for setting foreign keys to null

            // Removing customer after setting all calls by null ( calls depend on customer )
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("Customer Deleted Successfully");
        }
    }
}
