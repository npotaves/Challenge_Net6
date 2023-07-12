using AutoMapper;
using Challenge_Net6.DTO;
using Challenge_Net6.Models;
using Microsoft.AspNetCore.Mvc;

namespace Challenge_Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FacturaController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<FacturaDTO>> AddFactura(FacturaDTO nuevaFactura)
        {
            var factura = _mapper.Map<Factura>(nuevaFactura);
            _context.Entry(factura.Cliente).State = EntityState.Unchanged;
            _context.Entry(factura.Cliente.Ciudad).State = EntityState.Unchanged;
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
            return Created($"/{factura.Id}", _mapper.Map<FacturaDTO>(factura));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FacturaDTO>> Get(int id)
        {
            var cliente = await _context.Facturas.Include(e => e.Cliente).Include(e => e.Cliente.Ciudad)
                            .FirstOrDefaultAsync(e => e.Id == id);
            if (cliente == null)
            {
                return BadRequest("Cliente no encontrado");
            }
            return Ok(_mapper.Map<FacturaDTO>(cliente));
        }

    }
}
