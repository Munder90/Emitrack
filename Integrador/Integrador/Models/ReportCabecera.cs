using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class ReportCabecera
    {
        public string PEDIDO { get; set; }
        public string USUARIO { get; set; }
        public string FECHA { get; set; }
        public string TOTAL { get; set; }
        public string DIRECCION { get; set; }
        public string PAGO { get; set; }
    }
}