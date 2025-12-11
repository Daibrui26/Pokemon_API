using Models;
public class Opinion
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string Comentario { get; set; }
    public double Calificacion { get; set; }
    public string Fecha {get;set;}
    
    // Constructor completo
    public Opinion(int id, string usuario, string comentario, double calificacion, string fecha)
    {
        this.Id = id;
        this.Usuario = usuario;
        this.Comentario = comentario;
        this.Calificacion = calificacion;
        this.Fecha = fecha;

    }
    
    // Constructor vacío
    public Opinion()
    {
    }
}