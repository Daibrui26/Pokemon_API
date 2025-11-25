public class Habilidad
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool Beneficiosa { get; set; }
    
    // Constructor completo
    public Habilidad(int id, string nombre, string descripcion, bool beneficiosa)
    {
        this.ID = id;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.Beneficiosa = beneficiosa;
    }
    
    // Constructor vacío
    public Habilidad()
    {
    }
}