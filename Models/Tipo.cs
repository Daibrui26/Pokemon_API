using Models;

public class Tipo {

   public int id_Tipo { get; set; }
   public string  Nombre { get; set; }
   public string Debilidades {get; set; }
   public string Resistencias {get; set; }
   public string Inmunidad {get; set; }
   public string SuperEfectivo {get; set; }
   public string PocoEfectivo {get; set; }
   public string Inutilidad {get; set; }

   
    public Tipo(int id_tipo, string nombre, string debilidades, string resistencias, string inmunidad, string superefectivo, string pocoefectivo, string inutilidad) 
    {
        this.id_Tipo = id_tipo;
        this.Nombre = nombre;
        this.Debilidades = debilidades;
        this.Resistencias = resistencias;
        this.Inmunidad = inmunidad;
        this.SuperEfectivo = superefectivo;
        this.PocoEfectivo = pocoefectivo;
        this.Inutilidad = inutilidad;
    }

    public Tipo(){}

}