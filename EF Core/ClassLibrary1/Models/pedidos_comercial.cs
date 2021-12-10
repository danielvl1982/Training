﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semicrol.Pallets.Models
{
    [Table("pedidos_comercial", Schema = "dbo")]
    public partial class pedidos_comercial
    {
        [Key]
        public int identificador { get; set; }
        public int codigo_formato { get; set; }
        public int numero_pedido { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime fecha_pedido { get; set; }
        [Column(TypeName = "decimal(15, 6)")]
        public decimal? cantidad { get; set; }
        [Column(TypeName = "numeric(5, 0)")]
        public decimal? codigo_cliente { get; set; }
        [StringLength(3)]
        public string codigo_delegado { get; set; }
        [StringLength(1)]
        public string codigo_empresa { get; set; }
        [StringLength(5)]
        public string codigo_familia { get; set; }
        [StringLength(5)]
        public string codigo_postal { get; set; }
        [StringLength(20)]
        public string codigo_proveed { get; set; }
        [Column(TypeName = "numeric(3, 0)")]
        public decimal? codigo_prov_pais { get; set; }
        [StringLength(3)]
        public string codigo_umfabric { get; set; }
        [StringLength(3)]
        public string codigo_umfactur { get; set; }
        [Column(TypeName = "decimal(15, 3)")]
        public decimal? conversor { get; set; }
        [StringLength(120)]
        public string descripcion { get; set; }
        [StringLength(40)]
        public string direccion { get; set; }
        [StringLength(40)]
        public string direccion2 { get; set; }
        [StringLength(20)]
        public string especificacion1 { get; set; }
        [StringLength(20)]
        public string especificacion2 { get; set; }
        [StringLength(1)]
        public string estado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha { get; set; }
        [StringLength(8)]
        public string formato_etiqueta { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora { get; set; }
        [StringLength(1)]
        public string indicador { get; set; }
        [StringLength(1)]
        public string ind_anulado { get; set; }
        [StringLength(1)]
        public string ind_modificado { get; set; }
        [StringLength(1)]
        public string ind_peso_teorico { get; set; }
        [StringLength(40)]
        public string nombre_cliente { get; set; }
        [StringLength(40)]
        public string nombre_destino { get; set; }
        [StringLength(40)]
        public string nombre_prov_pais { get; set; }
        public int? numero_etiquetas { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? peso_teorico { get; set; }
        [StringLength(30)]
        public string poblacion { get; set; }
        [StringLength(5)]
        public string proveedor_pallet { get; set; }
        [StringLength(1)]
        public string situacion_pedido { get; set; }
        public int? stock_pedido { get; set; }
        [StringLength(20)]
        public string supdo_cliente { get; set; }
        [StringLength(16)]
        public string suref_cliente { get; set; }
        [StringLength(8)]
        public string usuario_creacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_creacion { get; set; }
        [StringLength(8)]
        public string usuario_actua { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_actua { get; set; }
        public int? ean_formato { get; set; }
        [StringLength(40)]
        public string contacto { get; set; }
        [StringLength(14)]
        public string telefono { get; set; }
        [StringLength(40)]
        public string horario { get; set; }
        [StringLength(10)]
        public string codigo_producto { get; set; }
        public int? numero_linea { get; set; }
        [StringLength(1)]
        public string p_a { get; set; }
        public short? numero_destino { get; set; }
        [StringLength(100)]
        public string observ_exped { get; set; }
        [StringLength(3)]
        public string cond_entrega { get; set; }
        [StringLength(30)]
        public string observ_entrega { get; set; }
        [StringLength(1)]
        public string ind_kilos_factura { get; set; }
        [StringLength(2)]
        public string codigo_reciclaje { get; set; }
        [StringLength(20)]
        public string material_reciclaje { get; set; }
        [StringLength(200)]
        public string codigo_qr { get; set; }
    }
}