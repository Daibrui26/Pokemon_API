public class Habilidad
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public bool Beneficiosa { get; set; }
    
    // Constructor completo
    public Habilidad(int id, string nombre, string descripcion, bool beneficiosa)
    {
        this.Id = id;
        this.Nombre = nombre;
        this.Descripcion = descripcion;
        this.Beneficiosa = beneficiosa;
    }
    
    // Constructor vacío
    public Habilidad()
    {
    }
}