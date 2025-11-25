using Models;

public class Movimiento {

    public int Num_MT { get; set; }
    public string Nombre { get; set; }
    public int Potencia { get; set; }
    public int Precision { get; set; }
    public int PP { get; set; }
    public string Categoria { get; set; }
    public string Efecto { get; set; }
    

    public Movimiento(int num_MT, string nombre, int potencia, int precision, int pp, string categoria, string efecto)
    {
        Num_MT = num_MT;
        Nombre = nombre;
        Potencia = potencia;
        Precision = precision;
        PP = pp;
        Categoria = categoria;
        Efecto = efecto;
    }

    public Movimiento() { }


}