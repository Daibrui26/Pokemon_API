using System.Collections;

namespace Models;

public class Pokemon {

    //Datos
    public int Pokedex_Id { get; set; }
    public int Generacion { get; set; }
    public string Nombre { get; set; }
    public int Peso { get; set; }
    public bool Shiny { get; set; }
    public string Color { get; set; }
    public Estadistica Estadisticas { get; set; }



    //Constructor
    public Pokemon(int pokedex_id, int generacion, string nombre, int peso, bool shiny, string color){
        Pokedex_Id = pokedex_id;
        Generacion = generacion;
        Nombre = nombre;
        Peso = peso;
        Shiny = shiny;
        Color = color;
    }


}