using AutoMapper;
using Challenge_Net6.DTO;
using Challenge_Net6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ClienteController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
 
        }      

        [HttpGet]
        public async Task<ActionResult<List<ClienteDTO>>> Get()
        {
            var clientes = await _context.Clientes.Include(_ => _.Ciudad).ToListAsync();
            var reto = clientes.Select(cliente => _mapper.Map<ClienteDTO>(cliente));
            return Ok(reto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await _context.Clientes.Include(e => e.Ciudad)
                            .FirstOrDefaultAsync(e => e.Id == id);
            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }
            return Ok(_mapper.Map<ClienteDTO>(cliente));
        }

        [HttpPut]
        public async Task<ActionResult<ClienteDTO>> UpdateCliente(ClienteDTO clienteModif)
        {
            var cliente = _mapper.Map<Cliente>(clienteModif);
            _context.Clientes.Update(cliente);
            _context.Entry(cliente).State = EntityState.Modified;
            _context.Entry(cliente.Ciudad).State = EntityState.Unchanged;
            _context.Entry(cliente).Property(x => x.PasswordHash).IsModified = false;
            _context.Entry(cliente).Property(x => x.PasswordSalt).IsModified = false;
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ClienteDTO>(cliente));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> Delete(int id)
        {
            var dbCliente = await _context.Clientes.FindAsync(id);
            if (dbCliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }
            _context.Clientes.Remove(dbCliente);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ClienteDTO>(dbCliente));
        }

    }
}
