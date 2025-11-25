using Models;

public class Tipo
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Debilidades { get; set; }
    public string Resistencias { get; set; }
    public string Inmunidad { get; set; }
    public string SuperEfectivo { get; set; }
    public string PocoEfectivo { get; set; }
    public string Inutilidad { get; set; }

    // Constructor completo
    public Tipo(int id, string nombre, string debilidades, string resistencias, string inmunidad, string superEfectivo, string pocoEfectivo, string inutilidad)
    {
        this.ID = id;
        this.Nombre = nombre;
        this.Debilidades = debilidades;
        this.Resistencias = resistencias;
        this.Inmunidad = inmunidad;
        this.SuperEfectivo = superEfectivo;
        this.PocoEfectivo = pocoEfectivo;
        this.Inutilidad = inutilidad;
    }

    // Constructor vacío
    public Tipo()
    {
    }
}