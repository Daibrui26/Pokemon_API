using Models;

public class Objeto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public double Precio { get; set; }
    public bool Unico { get; set; }
    public string Efecto { get; set; }

    // Constructor completo
    public Objeto(int id, string nombre, string descripcion, double precio, bool unico, string efecto)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.Precio = precio;
        this.Unico = unico;
        this.Efecto = efecto;
    }

    // Constructor vacío
    public Objeto()
    {
    }
}