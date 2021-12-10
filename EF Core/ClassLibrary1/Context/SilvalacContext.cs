﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Semicrol.Pallets.Models;

namespace Semicrol.Pallets.Context
{
    public partial class SilvalacContext : DbContext
    {
        public SilvalacContext()
        {
        }

        public SilvalacContext(DbContextOptions<SilvalacContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pallets_BobinasLiberacion> Pallets_BobinasLiberacion { get; set; }
        public virtual DbSet<pallets> pallets { get; set; }
        public virtual DbSet<pallets_asociados> pallets_asociados { get; set; }
        public virtual DbSet<pallets_asociados_cc> pallets_asociados_cc { get; set; }
        public virtual DbSet<pedidos> pedidos { get; set; }
        public virtual DbSet<pedidos_comercial> pedidos_comercial { get; set; }
        public virtual DbSet<procesos> procesos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "agil");

            modelBuilder.Entity<Pallets_BobinasLiberacion>(entity =>
            {
                entity.HasIndex(e => e.idBobina)
                    .HasName("IX_Pallets_BobinasLiberacion_1");

                entity.HasIndex(e => e.idPallet)
                    .HasName("IX_Pallets_BobinasLiberacion");

                entity.HasIndex(e => new { e.idPallet, e.idBobina, e.proceso })
                    .HasName("IX_Pallets_BobinasLiberacion_2");

                entity.Property(e => e.proceso)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<pallets>(entity =>
            {
                entity.HasIndex(e => e.fecha_ubicacion)
                    .HasName("IX_pallets_14");

                entity.HasIndex(e => e.numero_lote)
                    .HasName("IX_pallets_5");

                entity.HasIndex(e => e.orden_carga)
                    .HasName("ix_pallets6");

                entity.HasIndex(e => e.ref_pallet_desglose)
                    .HasName("IX_pallets_9");

                entity.HasIndex(e => e.ref_pallet_embalaje)
                    .HasName("IX_pallets_6");

                entity.HasIndex(e => e.ref_pallet_origen)
                    .HasName("IX_pallets_7");

                entity.HasIndex(e => e.ref_resumen)
                    .HasName("IX_pallets_8");

                entity.HasIndex(e => e.ubicacion)
                    .HasName("IX_pallets_13");

                entity.HasIndex(e => new { e.fecha_expedicion, e.anulado })
                    .HasName("IX_pallets_10");

                entity.HasIndex(e => new { e.fecha_fabricacion, e.motivo_desecho })
                    .HasName("IX_pallets_15");

                entity.HasIndex(e => new { e.liberado, e.ref_pedido })
                    .HasName("IX_pallets_16");

                entity.HasIndex(e => new { e.ref_pedido, e.proceso })
                    .HasName("IX_pallets_1");

                entity.HasIndex(e => new { e.ref_pedido, e.proceso_origen })
                    .HasName("IX_pallets_2");

                entity.HasIndex(e => new { e.proceso, e.proceso_destino, e.estado_fabricacion })
                    .HasName("IX_pallets_11");

                entity.HasIndex(e => new { e.ref_pedido, e.ref_operario, e.ref_maquina })
                    .HasName("IX_pallets_3");

                entity.HasIndex(e => new { e.ref_pedido, e.ref_seccion, e.proceso })
                    .HasName("IX_pallets");

                entity.HasIndex(e => new { e.ref_pedido, e.ref_seccion, e.numero_pallet, e.tipo_pallet })
                    .HasName("IX_pallets_4")
                    .IsUnique();

                entity.Property(e => e.Eremas)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.anulado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.comprada_fabricada)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.estado_comu_expe)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.estado_expedicion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.estado_fabricacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.estado_ubicacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.extru_impre)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_comunicacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.motivo_desecho)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.numero_lote)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.pallet_bolsa)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.procesado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.proceso)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.proceso_destino)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.proceso_origen)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.tipo_pallet)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ubicacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.unimedida_proceso)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_actua)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_creacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_expedicion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_liberacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_ubicacion)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<pallets_asociados>(entity =>
            {
                entity.HasIndex(e => e.ref_pallet_circulacion)
                    .HasName("IX_pallets_asociados");

                entity.HasIndex(e => e.ref_pallet_embalaje)
                    .HasName("IX_pallets_asociados_2");

                entity.Property(e => e.usuario_anulacion).IsUnicode(false);

                entity.Property(e => e.usuario_creacion).IsUnicode(false);
            });

            modelBuilder.Entity<pallets_asociados_cc>(entity =>
            {
                entity.HasIndex(e => e.ref_pallet)
                    .HasName("IX_pallets_asociados_cc");

                entity.HasIndex(e => e.ref_pallet_origen)
                    .HasName("IX_pallets_asociados_cc_2");

                entity.Property(e => e.usuario_anulacion).IsUnicode(false);

                entity.Property(e => e.usuario_creacion).IsUnicode(false);
            });

            modelBuilder.Entity<pedidos>(entity =>
            {
                entity.HasKey(e => e.identificador)
                    .IsClustered(false);

                entity.HasIndex(e => e.codigo_cliente)
                    .HasName("IX_pedidos_5");

                entity.HasIndex(e => e.fecha_pedido)
                    .HasName("IX_pedidos_4");

                entity.HasIndex(e => e.orden_fabric)
                    .HasName("IX_pedidos")
                    .IsUnique();

                entity.HasIndex(e => new { e.codigo_formato, e.tipo_pedido })
                    .HasName("IX_pedidos_1");

                entity.HasIndex(e => new { e.codigo_producto, e.tipo_pedido })
                    .HasName("IX_pedidos_3");

                entity.HasIndex(e => new { e.numero_pedido, e.secuencia })
                    .HasName("IX_pedidos_2");

                entity.HasIndex(e => new { e.indicador, e.estado_fabricacion, e.orden_fabric, e.numero_pedido, e.fecha_pedido })
                    .HasName("IX_pedidos_6");

                entity.Property(e => e.codigo_formato)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_material)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_producto)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_unimedida)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.color)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.delegacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.estado_fabricacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.etiqueta_bobina).IsUnicode(false);

                entity.Property(e => e.etiqueta_pale).IsUnicode(false);

                entity.Property(e => e.ind_anulado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_desglose)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_desglose2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_etiqcli)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_kilos_factura)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_metmot)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_modificado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_usoaliment)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.indicador)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.logotipo_etiqueta)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.nombre_cliente)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.nombre_formato)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.observaciones)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.proveed_cliente)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.supdo_cliente)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.suref_cliente)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.tipo_embalaje)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.tipo_etiquetado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.tipo_pedido)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.tipo_peso_etiq)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_actua)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_creacion)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<pedidos_comercial>(entity =>
            {
                entity.HasKey(e => e.identificador)
                    .HasName("pedidos_c_PK");

                entity.HasIndex(e => e.codigo_producto)
                    .HasName("IX_pedidos_comercial_1");

                entity.HasIndex(e => e.numero_pedido)
                    .HasName("pedidos_c_IX")
                    .IsUnique();

                entity.HasIndex(e => new { e.codigo_cliente, e.numero_linea })
                    .HasName("IX_pedidos_comercial_2");

                entity.Property(e => e.codigo_delegado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_empresa)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_familia)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_postal)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_producto)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_proveed)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_qr).IsUnicode(false);

                entity.Property(e => e.codigo_reciclaje)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_umfabric)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_umfactur)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.cond_entrega).IsUnicode(false);

                entity.Property(e => e.contacto).IsUnicode(false);

                entity.Property(e => e.descripcion).IsUnicode(false);

                entity.Property(e => e.direccion).IsUnicode(false);

                entity.Property(e => e.direccion2).IsUnicode(false);

                entity.Property(e => e.especificacion1).IsUnicode(false);

                entity.Property(e => e.especificacion2).IsUnicode(false);

                entity.Property(e => e.estado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.formato_etiqueta)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.horario).IsUnicode(false);

                entity.Property(e => e.ind_anulado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_kilos_factura)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_modificado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ind_peso_teorico)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.indicador)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.material_reciclaje).IsUnicode(false);

                entity.Property(e => e.nombre_cliente)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.nombre_destino)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.nombre_prov_pais)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.observ_entrega).IsUnicode(false);

                entity.Property(e => e.observ_exped).IsUnicode(false);

                entity.Property(e => e.p_a)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.poblacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.proveedor_pallet)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.situacion_pedido)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.supdo_cliente)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.suref_cliente)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.telefono).IsUnicode(false);

                entity.Property(e => e.usuario_actua)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_creacion)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<procesos>(entity =>
            {
                entity.HasKey(e => e.identificador)
                    .IsClustered(false);

                entity.HasIndex(e => new { e.ref_maquina, e.Orden })
                    .HasName("IX_procesos_2");

                entity.HasIndex(e => new { e.ref_maquina, e.Terminado })
                    .HasName("IX_procesos_1");

                entity.HasIndex(e => new { e.ref_pedido, e.proceso })
                    .HasName("IX_procesos");

                entity.HasIndex(e => new { e.ref_seccion, e.identificador })
                    .HasName("IX_procesos_3");

                entity.Property(e => e.Terminado)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_producto)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.codigo_unimedida)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.nombre_producto)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.proceso)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.tipo_desviacion)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_actua)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.usuario_creacion)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}