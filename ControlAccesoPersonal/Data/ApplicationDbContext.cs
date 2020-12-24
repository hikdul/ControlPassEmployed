using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ControlAccesoPersonal.Models;
using Microsoft.AspNetCore.Identity;

namespace ControlAccesoPersonal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ControlAccesoPersonal.Models.Feriados> Feriados { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            //los id son guid se generaron por medio de guidGenerator.com

            //administrador del sistema
            var RoleAdmin = new IdentityRole()
            {
                Id = "46d94d45-d30f-42f6-99bd-10ae13126805",
                Name = "admin",
                NormalizedName = "admin"
            };
            //usuario standar
            var RoleUser = new IdentityRole()
            {
                Id = "d9fa2c77-fec1-452b-b375-8a9bd415d399",
                Name = "user",
                NormalizedName = "user"
            };

            //usuario que envia solo los registros diarios
            var RoleRegister = new IdentityRole()
            {
                Id = "e80c2272-fa07-46db-9b09-bc8690a66a8d",
                Name = "register",
                NormalizedName = "register"
            };
            //para el auditor externo, este solo ve los datos
            var RoleAuditor = new IdentityRole()
            {
                Id = "30a23d9d-4e5e-4fab-8d6b-30ee04030e90",
                Name = "auditor",
                NormalizedName = "auditor"
            };

            builder.Entity<IdentityRole>().HasData(RoleAdmin);
            builder.Entity<IdentityRole>().HasData(RoleAuditor);
            builder.Entity<IdentityRole>().HasData(RoleUser);
            builder.Entity<IdentityRole>().HasData(RoleRegister);

            base.OnModelCreating(builder);
        }
    }
}
