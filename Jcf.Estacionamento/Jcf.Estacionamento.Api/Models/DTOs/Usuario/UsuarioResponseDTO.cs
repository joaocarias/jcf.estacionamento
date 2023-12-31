﻿using Jcf.Estacionamento.Api.Extensoes;

namespace Jcf.Estacionamento.Api.Models.DTOs.Usuario
{
    public class UsuarioResponseDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string PrimeiroNome
        {
            get
            {
                return Nome.PrimeiraParte();
            }
        }

        public UsuarioResponseDTO()
        {
            Nome = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
        }
    }
}
