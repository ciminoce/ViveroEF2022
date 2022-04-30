using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveroEF2022.Entities;

namespace ViveroEF2022.Context.EntityTypeConfigurations
{
    public class PlantaEntityTypeConfigurations:EntityTypeConfiguration<Planta>
    {
        public PlantaEntityTypeConfigurations()
        {
            ToTable("Plantas");
            HasKey(p => p.PlantaId);
            HasIndex(p => p.Descripcion).IsUnique();
            Property(p => p.Descripcion).HasMaxLength(100).IsRequired().IsUnicode(false);
            Property(e => e.Descripcion);


        }
    }
}
