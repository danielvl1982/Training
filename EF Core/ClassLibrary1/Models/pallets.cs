﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semicrol.Pallets.Models
{
    [Table("pallets", Schema = "dbo")]
    public partial class pallets
    {
        [Key]
        public int identificador { get; set; }
        public int? ref_pedido { get; set; }
        public int? ref_maquina { get; set; }
        public int? ref_operario { get; set; }
        public int? ref_resumen { get; set; }
        public int? ref_seccion { get; set; }
        public int? ref_pallet_origen { get; set; }
        public int? numero_pallet { get; set; }
        [StringLength(12)]
        public string numero_lote { get; set; }
        [StringLength(2)]
        public string proceso { get; set; }
        [StringLength(2)]
        public string unimedida_proceso { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? peso_unitario { get; set; }
        [StringLength(2)]
        public string proceso_destino { get; set; }
        [StringLength(1)]
        public string procesado { get; set; }
        [StringLength(2)]
        public string proceso_origen { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? kilos_brutos { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? kilos_netos { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? kilos_supernetos { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? metros { get; set; }
        [Column(TypeName = "numeric(7, 0)")]
        public decimal? unidades { get; set; }
        [StringLength(1)]
        public string pallet_bolsa { get; set; }
        [StringLength(1)]
        public string extru_impre { get; set; }
        [Column(TypeName = "numeric(24, 3)")]
        public decimal? destare_pallet { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? mandriles { get; set; }
        [StringLength(1)]
        public string estado_fabricacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_fabricacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora_fabricacion { get; set; }
        [StringLength(1)]
        public string ind_comunicacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_comunicacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora_comunicacion { get; set; }
        [StringLength(1)]
        public string estado_expedicion { get; set; }
        [StringLength(8)]
        public string usuario_expedicion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_expedicion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora_expedicion { get; set; }
        [StringLength(1)]
        public string estado_comu_expe { get; set; }
        [StringLength(1)]
        public string anulado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_anulacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora_anulacion { get; set; }
        [StringLength(8)]
        public string usuario_creacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_creacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora_creacion { get; set; }
        [StringLength(8)]
        public string usuario_actua { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_actua { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora_actua { get; set; }
        [StringLength(1)]
        public string tipo_pallet { get; set; }
        public int? ref_pallet_embalaje { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? kilos_super_pro { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? kilos_neto_pro { get; set; }
        [Column(TypeName = "numeric(18, 3)")]
        public decimal? kilos_bruto_pro { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? unidades_pro { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? metros_pro { get; set; }
        [Column(TypeName = "numeric(18, 0)")]
        public decimal? orden_carga { get; set; }
        [StringLength(10)]
        public string ubicacion { get; set; }
        [StringLength(10)]
        public string usuario_ubicacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_ubicacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? hora_ubicacion { get; set; }
        [StringLength(1)]
        public string estado_ubicacion { get; set; }
        [StringLength(1)]
        public string Eremas { get; set; }
        [StringLength(1)]
        public string comprada_fabricada { get; set; }
        [StringLength(4)]
        public string motivo_desecho { get; set; }
        public int? ref_pallet_desglose { get; set; }
        public int? centrotrabajo { get; set; }
        public short? liberado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? fecha_liberacion { get; set; }
        [StringLength(20)]
        public string usuario_liberacion { get; set; }
        public short? traspasado { get; set; }
        public int? motivo_reproceso { get; set; }
    }
}