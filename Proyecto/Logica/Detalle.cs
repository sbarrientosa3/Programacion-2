namespace Carrito.logica;

public class Detalle
{
    public Producto objProducto { get; set; }
    public int intCantidad { get; set; }
    //Constructo
    public Detalle(Producto objProducto, int intCantidad)
    {
        this.objProducto = objProducto;
        this.intCantidad = intCantidad;
    }
    public double calcularSubtotal()
    {
        if (objProducto == null) return 0.0
            return   objProducto.dblPrecio * intCantidad;
    }

    public void MostrarDetalle()
    {
        doble dblSubtotal = calcularSubtotal();
        console.writeline($"{objProducto.strNombre} {dblSubtotal}");
    }
}
