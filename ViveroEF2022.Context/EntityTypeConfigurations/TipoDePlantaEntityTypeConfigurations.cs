using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveroEF2022.Entities;

namespace ViveroEF2022.Context.EntityTypeConfigurations
{
    public class TipoDePlantaEntityTypeConfigurations : EntityTypeConfiguration<TipoDePlanta>
    {
        public TipoDePlantaEntityTypeConfigurations()
        {
            ToTable("TiposDePlantas");
            HasKey(tp => tp.TipoDePlantaId);
            HasIndex(tp => tp.Descripcion).IsUnique();
            Property(tp => tp.Descripcion).IsRequired().HasMaxLength(50);
            Property(e => e.Descripcion)
                .IsUnicode(false);

            HasMany(e => e.Plantas)
                .WithRequired(e => e.TipoDePlanta)
                .WillCascadeOnDelete(false);
        }
    }
}
