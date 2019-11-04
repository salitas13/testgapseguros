namespace TestSegurosGAP.Entidades.Respuestas
{
    public class Respuesta<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public T result { get; set; }
    }
}
