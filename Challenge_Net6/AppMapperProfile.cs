using AutoMapper;
using Challenge_Net6.DTO;
using Challenge_Net6.Models;

namespace Challenge_Net6
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>();

            CreateMap<Ciudad, CiudadDTO>();
            CreateMap<CiudadDTO, Ciudad>();

            CreateMap<FacturaDTO, Factura>();
            CreateMap<Factura, FacturaDTO>();

        }
    }

}
