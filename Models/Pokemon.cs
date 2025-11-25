using System.Collections;

namespace Models;

public class Pokemon {

    //Datos
    public int Pokedex_Id { get; set; }
    public int Generacion { get; set; }
    public string Nombre { get; set; }
    public int Peso { get; set; }
    public bool Shiny { get; set; }
    public List<Tipo?> Tipos { get; set; } = new();
    public List<Movimiento?> Movimientos { get; set; } = new();
    public List<Habilidad?> Habilidades { get; set; } = new();
    public Estadistica Estadisticas { get; set; }
    


    //Constructor
    public Pokemon(int pokedex_id, int generacion, string nombre, int peso, bool shiny, List<Tipo?> tipos, List<Movimiento?> movimientos, List<Habilidad?> habilidades, Estadistica estadisticas){
        Pokedex_Id = pokedex_id;
        Generacion = generacion;
        Nombre = nombre;
        Peso = peso;
        Shiny = shiny;
        Tipos = tipos;
        Movimientos = movimientos;
        Habilidades = habilidades;
        Estadisticas = estadisticas;
    }

    public Pokemon(){}


}