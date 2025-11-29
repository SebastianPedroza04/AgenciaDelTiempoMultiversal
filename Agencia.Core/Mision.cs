namespace Agencia.Core
{
    public class Mision
    {
        public int Id { get; set; }
        public int UniversoDestinoId { get; set; }
        public string Descripcion { get; set; }
        public int Prioridad { get; set; }   // 1 = máxima prioridad

        public Mision(int id, int universoDestinoId, string descripcion, int prioridad)
        {
            Id = id;
            UniversoDestinoId = universoDestinoId;
            Descripcion = descripcion;
            Prioridad = prioridad;
        }

        public override string ToString()
        {
            return $"Misión #{Id} -> U{UniversoDestinoId} (P={Prioridad}): {Descripcion}";
        }
    }
}
