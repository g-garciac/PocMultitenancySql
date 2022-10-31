using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMultitenancySql
{
    public class EmpleadosDbContext : DbContext
    {
        private readonly string _cadenaConexion;

        //Solo para pruebas DEBUG. Comentar para ejecutar migraciones. Descomentar para crear instancias explicitas
        public EmpleadosDbContext(string connectionString) : base()
        {
            _cadenaConexion = connectionString;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MappingsEf.Map(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(_cadenaConexion))
                //Con esto es suficiente para poder crear migraciones:            
                optionsBuilder.UseSqlServer(builder => builder.EnableRetryOnFailure());

            if (!string.IsNullOrWhiteSpace(_cadenaConexion))
                //Para poder crear instancias del dbcontext directo sin inyeccion hay que pasar la cadena de conexion
                //Se usa solo para algunas pruebas de DEBUG. No funciona para ejecutar migraciones, para migraciones se debe llamar sin para el parametro de la cadena de conexion
                optionsBuilder.UseSqlServer(_cadenaConexion, builder => builder.EnableRetryOnFailure());
        }
        public DbSet<Empleado> Empleados { get; set; }
    }

    public static class MappingsEf
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            EmpleadoMappings(modelBuilder);
        }

        public static void EmpleadoMappings(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Empleado>();
            entity.ToTable("Empleados");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasMaxLength(36);
            entity.Property(p => p.Nombre).HasMaxLength(64);
            entity.Property(p => p.Sueldo).HasPrecision(8, 2);
        }
    }
}
