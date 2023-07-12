using AutoMapper;
using Challenge_Net6.DTO;
using Challenge_Net6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Challenge_Net6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Cliente user = new Cliente();
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration,DataContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Cliente>> Register(RegisterDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var cliente = new Cliente();
            cliente.Email = request.Email;
            cliente.PasswordHash = passwordHash;
            cliente.PasswordSalt = passwordSalt;
            cliente.Nombre = request.Nombre;
            cliente.Apellido = request.Apellido;
            cliente.Domicilio = request.Domicilio;
            cliente.Ciudad = _mapper.Map<Ciudad>(request.Ciudad);

            _context.Entry(cliente.Ciudad).State = EntityState.Unchanged;
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return Created($"/{cliente.Id}", _mapper.Map<ClienteDTO>(cliente));
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginUserDTO request)
        {
            var usuario = await _context.Clientes.FirstOrDefaultAsync(e => e.Email == request.Email);
          
           if (usuario == null)
            {
                return BadRequest("User not found.");
            }
            Cliente user = _mapper.Map<Cliente>(usuario);
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }
 

        private string CreateToken(Cliente user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
