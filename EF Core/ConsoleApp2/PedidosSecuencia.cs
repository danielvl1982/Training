using System;

namespace ConsoleApp2
{
    internal class PedidosSecuencia
    {
        private int? numero_pedido;
        private short? secuencia;
        private string proceso;
        private decimal? peso_metro;

        public PedidosSecuencia(int? numero_pedido, short? secuencia, string proceso, decimal? peso_metro)
        {
            this.numero_pedido = numero_pedido;
            this.secuencia = secuencia;
            this.proceso = proceso;
            this.peso_metro = peso_metro;
        }

        public int? Numero_pedido { get => numero_pedido; set => numero_pedido = value; }
        public short? Secuencia { get => secuencia; set => secuencia = value; }
        public string Proceso { get => proceso; set => proceso = value; }
        public decimal? Peso_metro { get => peso_metro; set => peso_metro = value; }

        public override bool Equals(object obj)
        {
            return obj is PedidosSecuencia secuencia &&
                   numero_pedido == secuencia.numero_pedido &&
                   this.secuencia == secuencia.secuencia &&
                   proceso == secuencia.proceso &&
                   peso_metro == secuencia.peso_metro;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(numero_pedido, secuencia, proceso, peso_metro);
        }
    }
}