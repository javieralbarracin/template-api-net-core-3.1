using AutoMapper;
using MisGastos.Core.DTOs;
using MisGastos.Core.Entities;

namespace MisGastos.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Gasto, GastoDto>()
                .ForMember(dest =>
                           dest.Frecuencia,
                    opt => opt.MapFrom(src => src.Frecuencia.Descripcion))
                .ForMember(tip =>
                           tip.GastoTipo,
                    opt => opt.MapFrom(src => src.GastoTipo.Descripcion))   
                .ForMember(mes => mes.Mes,
                    opt => opt.MapFrom(src => src.Mes.Descripcion));
            
            CreateMap<Gasto, GastoDetalleDto>()
                .ForMember(dest =>
                           dest.Frecuencia,
                    opt => opt.MapFrom(src => src.Frecuencia.Descripcion))
                .ForMember(tip =>
                           tip.GastoTipo,
                    opt => opt.MapFrom(src => src.GastoTipo.Descripcion))   
                .ForMember(cred =>
                           cred.Creditos,
                    opt => opt.MapFrom(src => src.GastosCreditos))   
                .ForMember(mes => mes.Mes,
                    opt => opt.MapFrom(src => src.Mes.Descripcion));            
            
            CreateMap<Security, SecurityDto>().ReverseMap();
            
            CreateMap<GastoCreacionDto, Gasto>().ReverseMap(); 
            
            CreateMap<GastoTipo, GastoTipoDto> ().ReverseMap(); 
            
            CreateMap<Frecuencia, FrecuenciaDto> ().ReverseMap(); 

            CreateMap<Mes, MesDto> ().ReverseMap().ReverseMap();

            CreateMap<GastoTipoDetalle, GastoTipoDetalleDto>();
        }
    }
}
