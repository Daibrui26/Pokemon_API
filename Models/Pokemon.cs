using System.Collections;

namespace Models;

public class Pokemon
{
    // Datos
    public int ID { get; set; }
    public string Region { get; set; }
    public string Nombre { get; set; }
    public double Peso { get; set; }
    public bool Shiny { get; set; }
    public string Tipo { get; set; }
    public Habilidad Habilidad { get; set; } = new();
    public Pokeball Pokeball { get; set; }
    public Habitat Habitat { get; set; }

    // Constructor completo
    public Pokemon(int id, string region, string nombre, double peso, bool shiny, string tipo, Habilidad habilidad, Pokeball pokeball, Habitat habitat)
    {
        this.ID = id;
        this.Region = region;
        this.Nombre = nombre;
        this.Peso = peso;
        this.Shiny = shiny;
        this.Tipo = tipo;
        this.Habilidad = habilidad;
        this.Pokeball = pokeball;
        this.Habitat = habitat;
    }

    // Constructor vacío
    public Pokemon()
    {
    }
}