using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Usuarios_Dir
    {
        int iD;
        int usuario;
        string calle;
        string numero_Ext;
        string numero_Int;
        string colonia;
        string municipio;
        string estado;
        int cP;

        public int ID { get => iD; set => iD = value; }
        public int Usuario { get => usuario; set => usuario = value; }
        public string Calle { get => calle; set => calle = value; }
        public string Numero_Ext { get => numero_Ext; set => numero_Ext = value; }
        public string Numero_Int { get => numero_Int; set => numero_Int = value; }
        public string Colonia { get => colonia; set => colonia = value; }
        public string Municipio { get => municipio; set => municipio = value; }
        public string Estado { get => estado; set => estado = value; }
        public int CP { get => cP; set => cP = value; }
    }
}