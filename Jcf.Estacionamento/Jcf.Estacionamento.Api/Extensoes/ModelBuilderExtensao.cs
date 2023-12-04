using Jcf.Estacionamento.Api.Models;
using Jcf.Estacionamento.Api.Utils;
using Microsoft.EntityFrameworkCore;

namespace Jcf.Estacionamento.Api.Extensoes
{
    public static class ModelBuilderExtensao
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(
                    new Usuario
                    (
                        Guid.Parse("08dbd59a-2683-4c67-8e16-689ba2648545"),
                        "Administrador Usuário",
                        "admin@email.com",
                        SenhaUtil.CriarHashMD5("102030"),
                        RolesConstante.ADMIN
                    ),
                    new Usuario
                    (
                        Guid.Parse("08dbdc08-56e1-4e90-805f-db29361e075e"),
                        "Básico Usuário",
                        "basico@email.com",                        
                        SenhaUtil.CriarHashMD5("102030"),
                        RolesConstante.BASICO
                    )
                );

            modelBuilder.Entity<Models.Estacionamento>().HasData(
                    new Models.Estacionamento
                    (
                        Guid.Parse("08dbe21f-7354-4a2a-8b53-63cc10662e98"),
                        "Estacionamento Principal",
                        30,
                        10,
                        3
                    )
                );
        }
    }
}
