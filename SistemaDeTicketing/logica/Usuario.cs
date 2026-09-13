using SistemaDeTicketing.logica
namespace logica;  
{
public class Usuario 
{
public string strNombre { get; set; }
public string strCodigo { get; set; }
public string strCorreo { get; set; }
}
//Constructor
public Usuario(string strCodigo, string strNombre, string strCorreo)
   {
    this.strCodigo = strCodigo;
    this.strNombre = strNombre;
    this.strCorreo = strCorreo;
   }
  public abstract obtenerRol();
}
