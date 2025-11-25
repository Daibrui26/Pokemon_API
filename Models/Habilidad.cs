public class Habilidad{

    public int id_Habilidad { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool Beneficiosa { get; set; }
    
    public Habilidad(int id_habilidad, string nombre, string descripcion, bool beneficiosa){
        this.id_Habilidad = id_habilidad;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.Beneficiosa = beneficiosa;
    }
    public Habilidad(){}


}