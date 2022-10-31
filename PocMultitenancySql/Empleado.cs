using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMultitenancySql
{
    public class Empleado
    {
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public string Puesto { get; private set; }
        public decimal Sueldo { get; private set; }

        public Empleado(string id, string nombre, string puesto, decimal sueldo)
        {
            Id = id;
            Nombre = nombre;
            Puesto = puesto;
            Sueldo = sueldo;
        }
    }
}
