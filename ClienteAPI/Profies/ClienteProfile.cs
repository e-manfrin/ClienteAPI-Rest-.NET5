using AutoMapper;
using ClienteAPI.Dtos;
using ClienteAPI.Models;

namespace ClienteAPI.Profies
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<CreateClienteDto, Cliente>();
            CreateMap<UpdateClienteDto, Cliente>();
        }
    }
}
