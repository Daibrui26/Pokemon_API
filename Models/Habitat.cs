using Models;
public class Habitat
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Region { get; set; }
    public string Clima { get; set; }
    public int Temperatura { get; set; }
    public string Descripcion { get; set; }

    // Constructor completo
    public Habitat(int id, string nombre, string region, string clima, int temperatura, string descripcion)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Region = region;
        this.Clima = clima;
        this.Temperatura = temperatura;
        this.Descripcion = descripcion;
    }

    // Constructor vacío
    public Habitat()
    {
    }
}