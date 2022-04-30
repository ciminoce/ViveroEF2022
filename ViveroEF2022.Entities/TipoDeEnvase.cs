using System.Collections.Generic;
using System.Security.AccessControl;

namespace ViveroEF2022.Entities
{
    public class TipoDeEnvase
    {
        public TipoDeEnvase()
        {
            Plantas = new List<Planta>();
        }
        public int TipoDeEnvaseId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Planta> Plantas { get; set; }
    }
}
