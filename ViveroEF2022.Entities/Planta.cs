namespace ViveroEF2022.Entities
{
    public class Planta
    {
        public int PlantaId { get; set; }

        public string Descripcion { get; set; }

        public int TipoDePlantaId { get; set; }
        public int TipoDeEnvaseId { get; set; }

        public decimal PrecioVenta { get; set; }

        public virtual TipoDePlanta TipoDePlanta { get; set; }
        public virtual  TipoDeEnvase TipoDeEnvase { get; set; }
    }
}
