namespace Agencia.Core
{
    public class Evento
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public Evento(string titulo, string descripcion)
        {
            Titulo = titulo;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"{Titulo}: {Descripcion}";
        }
    }
}
