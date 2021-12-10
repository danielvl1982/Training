using Semicrol.Pallets.Context;
using System;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SilvalacContext silvalacContext = new SilvalacContext())
            {
                var query = from c in silvalacContext.pedidos
                            join d in silvalacContext.procesos on c.identificador equals d.ref_pedido
                            where c.estado_fabricacion == "F" select new PedidosSecuencia(c.numero_pedido, c.secuencia, d.proceso, d.peso_metro );
                            //where c.estado_fabricacion == "F" select new { c.numero_pedido, c.secuencia, d.proceso, d.peso_metro };

                var data = query.ToList();

                foreach (var item in data)
                {
                    Console.WriteLine($"{item.Numero_pedido}, {item.Secuencia}, {item.Proceso}, {item.Peso_metro}");
                    //Console.WriteLine($"{item.numero_pedido}, {item.secuencia}, {item.proceso}, {item.peso_metro}");
                }

                Console.Read();

            }
            Console.WriteLine("Hello World!");
        }
    }
}
