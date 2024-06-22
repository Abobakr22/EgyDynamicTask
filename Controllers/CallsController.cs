using EgyDynamicTask.Data;
using EgyDynamicTask.Dtos.CallDtos;
using EgyDynamicTask.Dtos.CustomerDtos;
using EgyDynamicTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgyDynamicTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CallsController(AppDbContext context)
        {
            _context = context;
        }

        // GET all Calls
        [HttpGet]
        public async Task<IActionResult> GetAllCalls()
        {
            var calls = await _context.Calls
                            .Select(c => new GetCallsDto
                            {
                                CallId = c.Id,
                                CallTitle = c.CallTitle,
                                CallDate = c.CallDate,
                                CallType = c.CallType,
                                EnteredBy = c.EnteredBy,
                                EntryDate = c.EntryDate,
                                IsCompleted = c.IsCompleted,
                                Description = c.Description,
                                LastModifiedBy = c.LastModifiedBy,
                                LastModifiedDate = c.LastModifiedDate,
                                IsIncoming = c.IsIncoming,
                                CustomerName = c.Customer.CustomerName,
                                EmployeeName = c.Employee.Name,
                                ProjectName = c.Project.Name
                            })
                            .ToListAsync();

            if(calls == null || !calls.Any())
            {
                return NotFound("No calls found");
            }

            return Ok(calls);
        }

        // GET call by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCallById(int id)
        {
            var call = await _context.Calls.Where(c => c.Id == id)
                 .Select(c => new GetCallsDto
                 {
                     CallId = c.Id,
                     CallDate = c.CallDate,
                     CallTitle = c.CallTitle,
                     CallType = c.CallType,
                     EnteredBy = c.EnteredBy,
                     EntryDate = c.EntryDate,
                     IsCompleted = c.IsCompleted,
                     Description = c.Description,
                     LastModifiedBy = c.LastModifiedBy,
                     LastModifiedDate = c.LastModifiedDate,
                     IsIncoming = c.IsIncoming,
                     CustomerName = c.Customer.CustomerName,
                     EmployeeName = c.Employee.Name,
                     ProjectName = c.Project.Name
                 }).FirstOrDefaultAsync();

            if(call == null)
            {
                return NotFound($"no call found with id = {id}");
            }

            return Ok(call);
        }

        //create a new Call
        [HttpPost]
        public async Task<IActionResult> CreateCall([FromBody] CallDto CreateCallDto)
        {
            if(CreateCallDto == null)
            {
                return BadRequest("Call data is null.");
            }

            // check existence of Customer 
            var customer = await _context.Customers.AnyAsync(c => c.Id == CreateCallDto.CustomerId);
            if(!customer)
            {
                return BadRequest($"customer with id = {CreateCallDto.CustomerId} not exist");
            }

            // check existence of Employee 
            var employee = await _context.Employees.AnyAsync(e => e.Id == CreateCallDto.EmployeeId);
            if(!employee)
            {
                return BadRequest($"employee with id = {CreateCallDto.EmployeeId} not exist");
            }

            // check existence of Project
            if(CreateCallDto.ProjectId.HasValue)
            {
                var projectExists = await _context.Projects.AnyAsync(p => p.Id == CreateCallDto.ProjectId.Value);
                if(!projectExists)
                {
                    return BadRequest($"Project with Id {CreateCallDto.ProjectId.Value} does not exist.");
                }
            }

            var call = new Call
            {
                Description = CreateCallDto.Description,
                CallTitle = CreateCallDto.CallTitle,
                CallDate = CreateCallDto.CallDate,
                ProjectId = CreateCallDto.ProjectId,
                EmployeeId = CreateCallDto.EmployeeId,
                CallType = CreateCallDto.CallType,
                IsCompleted = CreateCallDto.IsCompleted,
                IsIncoming = CreateCallDto.IsIncoming,
                EnteredBy = CreateCallDto.EnteredBy,
                EntryDate = CreateCallDto.EntryDate,
                LastModifiedBy = CreateCallDto.LastModifiedBy,
                LastModifiedDate = CreateCallDto.LastModifiedDate,
                CustomerId = CreateCallDto.CustomerId
            };

            await _context.Calls.AddAsync(call);
            _context.SaveChanges();
            return Ok(call);
        }

        // edit call
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCall(int id, UpdateCallDto updateCallDto)
        {
            try
            {
                var call = await _context.Calls.FindAsync(id);
                if(call == null)
                    return NotFound($"No call found with id = {id}");

                call.Description = updateCallDto.Description;
                call.CallTitle = updateCallDto.CallTitle;
                call.CallDate = updateCallDto.CallDate;
                call.CallType = updateCallDto.CallType;
                call.IsCompleted = updateCallDto.IsCompleted;
                call.IsIncoming = updateCallDto.IsIncoming;
                call.EnteredBy = updateCallDto.EnteredBy;
                call.EntryDate = updateCallDto.EntryDate;
                call.LastModifiedBy = updateCallDto.LastModifiedBy;
                call.LastModifiedDate = updateCallDto.LastModifiedDate;
                call.CustomerId = updateCallDto.CustomerId;
                call.EmployeeId = updateCallDto.EmployeeId;
                call.ProjectId = updateCallDto.ProjectId;

                await _context.SaveChangesAsync();

                return Ok("Call updated successfully");
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine($"Error updating call with id {id}: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the call. Please try again later.");
            }
        }

        //delete call by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCall(int id)
        {
            var call = await _context.Calls.FindAsync(id);
            if(call == null)
            {
                return NotFound($"no call found with id = {id}");
            }

            _context.Calls.Remove(call);
            await _context.SaveChangesAsync();

            return Ok("Call Deleted Successfully");
        }   
    }
}