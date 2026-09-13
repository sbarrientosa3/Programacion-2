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
            
        }

    }
}