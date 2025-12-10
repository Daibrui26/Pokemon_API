using Models;
public class Reseña
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public string Texto { get; set; }
    public int Puntuacion { get; set; }

    // Constructor completo
    public Reseña(int id, string usuario, DateTime fecha, string texto, int puntuacion)
    {
        this.Id = id;
        this.Usuario = usuario;
        this.Fecha = fecha;
        this.Texto = texto;
        this.Puntuacion = puntuacion;
        
    }

    // Constructor vacío
    public Reseña()
    {
    }
}