﻿using AutoMapper;
using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Models.DTOs.Estacionamento;
using Jcf.Estacionamento.Api.Models.DTOs.Usuario;

namespace Jcf.Estacionamento.Api.Configs
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {

            CreateMap<Usuario, UsuarioResponseDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            
            CreateMap<Models.Estacionamento, EstacionamentoResponseDTO>().ReverseMap();
        }
    }
}
