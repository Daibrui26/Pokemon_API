using Models;
public class Habilidad
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool Beneficiosa { get; set; }
    public bool Oculta {get;set;}
    public bool Unica {get;set;}
    
    // Constructor completo
    public Habilidad(int id, string nombre, string descripcion, bool beneficiosa, bool oculta, bool unica)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.Beneficiosa = beneficiosa;
        this.Oculta = oculta;
        this.Unica = unica;
    }
    
    // Constructor vacío
    public Habilidad()
    {
    }
}