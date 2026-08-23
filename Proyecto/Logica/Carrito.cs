using System;
usingSystem.CollectionsGeneric;

namespace Carrito.Logica
public class Carrito
{
    public list<Detalle> lstDetalle { get; set; }
    //Constructo
    public Carrito()
    {
        this.lstDetalle = new list<detalle>();
    }
    public bool agregarProducto(int intCantidad, Producto objProducto)
        //Validaciones
    {
        if (! objProducto.verificarStock (intCantidad))
            console.writeline ("No hay stock");
        return false;
    }
    //De lo contrario
    lstDetalle.add (new Detalle (intCantidad, objProducto));
}

public class MostrarCarrito ()
{
if  (lstDetalle.count == -1)
    console.writeline ("Carrito vacio");
return;
}
{
    foreach (var item in lstDetalle)
    {
        item.MostrarDetalle();
    }
}