using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integrador.Models
{
    public class Pregunta
    {
        string id;
        string apass;
        string nques;
        string nress;

        public string Id { get => id; set => id = value; }
        public string Apass { get => apass; set => apass = value; }
        public string Nques { get => nques; set => nques = value; }
        public string Nress { get => nress; set => nress = value; }
    }
}