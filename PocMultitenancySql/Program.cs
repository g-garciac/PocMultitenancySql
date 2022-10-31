using Microsoft.Data.SqlClient;

namespace PocMultitenancySql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new EmpleadosDbContext("Server=DESKTOP-0NJKIMA;Database=empleadosdb;User Id=u0001;Password=Pa$$w0rdAbc123-1000:H3ll0;");
            var id = $"{DateTime.Now:yyyyMMddHHmmssfff}";
            db.Empleados.Add(new Empleado(id, $"Nombre {id}", $"Puesto {id}", (DateTime.Now.Millisecond + 1) * 10));
            db.SaveChanges();
            foreach (var empleado in db.Empleados)
                Console.WriteLine($"{empleado.Nombre} {empleado.Sueldo:C}");
        }
    }
}