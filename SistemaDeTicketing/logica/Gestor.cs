namespace SistemaDeTicketing.logica
{
    public class GestorTicket
    {
        public List<Ticket> lstTickets { get; set; }
        public List<Tecnico> lstTecnicos { get; set; }
        public List<Solicitante> lstSolicitantes { get; set; }

        // Constructor
        public GestorTicket()
        {
            lstTecnicos = new List<Tecnico>();
            lstTickets = new List<Ticket>();
            lstSolicitantes = new List<Solicitante>();
        }

        Public class CrearTicket(string strNombre, string strDescripcion, int intNumero, string strCategoria, string strPrioridad, solicitante objSolicitante)

        //Validaciones
        {
            if (string.IsNullOrEmpty(strNombre) || string.IsNullOrEmpty(strDescripcion) || string.IsNullOrEmpty(strCategoria) || string.IsNullOrEmpty(strPrioridad))
            {
                throw new ArgumentException("Todos los campos son obligatorios.");
            }
        }

        ticket objTicket = new Ticket(strNombre, strDescripcion, intNumero, strCategoria, strPrioridad, objSolicitante);
        objTicketTecnico(objTecnico);
        //logica disponibilidadtecnico

        publica ticket buscarticket(int intNumero)
        {
         ticket objticket = lstTicket.find(t => t.intNumero == intNumero);
         return objticket;
        }

    }
}