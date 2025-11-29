namespace Agencia.Core
{
    public enum FaccionTemporal
    {
        Heroes,
        Villanos,
        Anime,
        DibujosClasicos,
        SciFi,
        Memeverso
    }

    public class Universo
    {
        public int Id { get; }
        public string Codigo { get; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Riesgo { get; set; }   // 1-100
        public bool Activo { get; set; }
        public FaccionTemporal Faccion { get; set; }

        // Árbol k-ario (máx 6 hijos)
        public Universo[] Hijos { get; } = new Universo[6];

        // Lista enlazada de eventos locales
        public LinkedList<Evento> Eventos { get; set; } = new LinkedList<Evento>();

        public Universo(int id, string codigo, string nombre, string descripcion, int riesgo, FaccionTemporal faccion)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Riesgo = riesgo;
            Faccion = faccion;
            Activo = true;
        }

        public bool AgregarHijo(Universo hijo)
        {
            for (int i = 0; i < Hijos.Length; i++)
            {
                if (Hijos[i] == null)
                {
                    Hijos[i] = hijo;
                    return true;
                }
            }
            return false; // ya tiene 6 hijos
        }

        public override string ToString()
        {
            return $"U{Id} [{Codigo}] {Nombre} (Riesgo {Riesgo}/100, {Faccion}, {(Activo ? "ACTIVO" : "PODADO")})";
        }
    }
}
