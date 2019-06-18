using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Usuarios_T
    {
        int iD;
        string desc;

        public int ID { get => iD; set => iD = value; }
        public string Desc { get => desc; set => desc = value; }
    }
}