using Models;

namespace Models;

public class Pokemon
{
    // Datos
    public int Id { get; set; }
    public string Region { get; set; }
    public string Nombre { get; set; }
    public double Peso { get; set; }
    public bool Shiny { get; set; }
    public string Tipo { get; set; }
    public Habilidad Habilidad { get; set; } = new();
    public Pokeball Pokeball { get; set; }
    public Habitat Habitat { get; set; }
    public Objeto Objeto {get; set; }
    public Reseña Reseña {get; set; }
    // Constructor completo
    public Pokemon(int id, string region, string nombre, double peso, bool shiny, string tipo, Habilidad habilidad, Pokeball pokeball, Habitat habitat, Objeto objeto, Reseña reseña)
    {
        this.Id = id;
        this.Region = region;
        this.Nombre = nombre;
        this.Peso = peso;
        this.Shiny = shiny;
        this.Tipo = tipo;
        this.Habilidad = habilidad;
        this.Pokeball = pokeball;
        this.Habitat = habitat;
        this.Objeto = objeto;
        this.Reseña = reseña;
    }

    // Constructor vacío
    public Pokemon()
    {
    }
}