using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Data;
using OrderManagementAPI.DTOs;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(OrderManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomersList()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> PostCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<CustomerDto>(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> PutCustomer(int id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
                return BadRequest();

            var existing = await _context.Customers.FindAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(customerDto, existing);

            await _context.SaveChangesAsync();

            var dto = _mapper.Map<CustomerDto>(existing);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}