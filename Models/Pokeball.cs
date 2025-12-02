using Models;
public class Pokeball
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public double Ratio { get; set; }
    public double Precio { get; set; }
    public string Color { get; set; }
    public string Efecto { get; set; }

    // Constructor completo
    public Pokeball(int id, string nombre, double ratio, double precio, string color, string efecto)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Ratio = ratio;
        this.Precio = precio;
        this.Color = color;
        this.Efecto = efecto;
    }

    // Constructor vacío
    public Pokeball()
    {
    }
}