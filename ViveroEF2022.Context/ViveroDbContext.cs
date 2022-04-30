using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ViveroEF2022.Context.EntityTypeConfigurations;
using ViveroEF2022.Entities;

namespace ViveroEF2022.Context
{
    public partial class ViveroDbContext : DbContext
    {
        public ViveroDbContext()
            : base("name=ViveroDbContext")
        {
        }

        public virtual DbSet<Planta> Plantas { get; set; }
        public virtual DbSet<TipoDePlanta> TiposDePlantas { get; set; }
        public virtual DbSet<TipoDeEnvase> TiposDeEnvases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new PlantaEntityTypeConfigurations());
            modelBuilder.Configurations.Add(new TipoDePlantaEntityTypeConfigurations());
            modelBuilder.Configurations.Add(new TipoDeEnvaseEntityTypeConfigurations());
        }
    }
}
