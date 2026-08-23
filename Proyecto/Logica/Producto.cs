namespace Carrito.logica

public class Carrito
{
    public int intStock { get; set; }
    public double dblPrecio { get; set; }
    public string strNombre { get; set; }

    public string strCodigo { get; set; }
    // Contructor 
    public Producto(int intStock, double dblPrecio, string strNombre, string strCodigo)
    {
        this.intStock = intStock;
        this.dbl.Precio = dbl.Precio;
    }

    public bool verificarStock(int intStock)
    {
        return intCantidad > o && this.Stock >= intCantidad;
    }

    public void Producto()
    {
        console.whitelime ($"{strCodigo} {strNombre}");
    }
}