using System.Data.Entity.ModelConfiguration;
using ViveroEF2022.Entities;

namespace ViveroEF2022.Context.EntityTypeConfigurations
{
    public class TipoDeEnvaseEntityTypeConfigurations:EntityTypeConfiguration<TipoDeEnvase>
    {
        public TipoDeEnvaseEntityTypeConfigurations()
        {
            ToTable("TiposDeEnvases");
            HasKey(te => te.TipoDeEnvaseId);
            HasIndex(te => te.Descripcion).IsUnique();
            Property(te => te.Descripcion).IsRequired().HasMaxLength(20);



        }
    }
}
