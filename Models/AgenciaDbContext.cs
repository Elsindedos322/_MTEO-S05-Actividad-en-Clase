using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Caso04actividadclase.Models;

public partial class AgenciaDbContext : DbContext
{
    public AgenciaDbContext()
    {
    }

    public AgenciaDbContext(DbContextOptions<AgenciaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Evaluacionesproveedor> Evaluacionesproveedors { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Paquetesturistico> Paquetesturisticos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in", "like", "ilike", "is", "match", "imatch", "isdistinct" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Idcliente).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.Documentoidentidad, "clientes_documentoidentidad_key").IsUnique();

            entity.HasIndex(e => e.Email, "clientes_email_key").IsUnique();

            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Documentoidentidad)
                .HasMaxLength(20)
                .HasColumnName("documentoidentidad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Evaluacionesproveedor>(entity =>
        {
            entity.HasKey(e => e.Idevaluacion).HasName("evaluacionesproveedor_pkey");

            entity.ToTable("evaluacionesproveedor");

            entity.Property(e => e.Idevaluacion).HasColumnName("idevaluacion");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Evaluacionesproveedors)
                .HasForeignKey(d => d.Idcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evaluaciones_clientes");

            entity.HasOne(d => d.IdproveedorNavigation).WithMany(p => p.Evaluacionesproveedors)
                .HasForeignKey(d => d.Idproveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evaluaciones_proveedores");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Idpago).HasName("pagos_pkey");

            entity.ToTable("pagos");

            entity.Property(e => e.Idpago).HasColumnName("idpago");
            entity.Property(e => e.Estadopago)
                .HasMaxLength(30)
                .HasColumnName("estadopago");
            entity.Property(e => e.Fechapago)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechapago");
            entity.Property(e => e.Idreserva).HasColumnName("idreserva");
            entity.Property(e => e.Metodopago)
                .HasMaxLength(50)
                .HasColumnName("metodopago");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdreservaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.Idreserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pagos_reservas");
        });

        modelBuilder.Entity<Paquetesturistico>(entity =>
        {
            entity.HasKey(e => e.Idpaquete).HasName("paquetesturisticos_pkey");

            entity.ToTable("paquetesturisticos");

            entity.Property(e => e.Idpaquete).HasColumnName("idpaquete");
            entity.Property(e => e.Cupomaximo).HasColumnName("cupomaximo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Fechafin).HasColumnName("fechafin");
            entity.Property(e => e.Fechainicio).HasColumnName("fechainicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Preciobase)
                .HasPrecision(10, 2)
                .HasColumnName("preciobase");

            entity.HasMany(d => d.Idservicios).WithMany(p => p.Idpaquetes)
                .UsingEntity<Dictionary<string, object>>(
                    "Paqueteservicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("Idservicio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_paqueteservicios_servicios"),
                    l => l.HasOne<Paquetesturistico>().WithMany()
                        .HasForeignKey("Idpaquete")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_paqueteservicios_paquetes"),
                    j =>
                    {
                        j.HasKey("Idpaquete", "Idservicio").HasName("paqueteservicios_pkey");
                        j.ToTable("paqueteservicios");
                        j.IndexerProperty<int>("Idpaquete").HasColumnName("idpaquete");
                        j.IndexerProperty<int>("Idservicio").HasColumnName("idservicio");
                    });
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Idproveedor).HasName("proveedores_pkey");

            entity.ToTable("proveedores");

            entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");
            entity.Property(e => e.Calificacionpromedio)
                .HasPrecision(3, 2)
                .HasDefaultValue(0.00m)
                .HasColumnName("calificacionpromedio");
            entity.Property(e => e.Contacto)
                .HasMaxLength(100)
                .HasColumnName("contacto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Tiposervicio)
                .HasMaxLength(50)
                .HasColumnName("tiposervicio");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Idreserva).HasName("reservas_pkey");

            entity.ToTable("reservas");

            entity.Property(e => e.Idreserva).HasColumnName("idreserva");
            entity.Property(e => e.Estadoreserva)
                .HasMaxLength(30)
                .HasDefaultValueSql("'Pendiente'::character varying")
                .HasColumnName("estadoreserva");
            entity.Property(e => e.Fechareserva)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechareserva");
            entity.Property(e => e.Idcliente).HasColumnName("idcliente");
            entity.Property(e => e.Idpaquete).HasColumnName("idpaquete");
            entity.Property(e => e.Montototal)
                .HasPrecision(10, 2)
                .HasColumnName("montototal");

            entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Idcliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reservas_clientes");

            entity.HasOne(d => d.IdpaqueteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.Idpaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reservas_paquetes");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Idservicio).HasName("servicios_pkey");

            entity.ToTable("servicios");

            entity.Property(e => e.Idservicio).HasColumnName("idservicio");
            entity.Property(e => e.Capacidaddisponible).HasColumnName("capacidaddisponible");
            entity.Property(e => e.Costo)
                .HasPrecision(10, 2)
                .HasColumnName("costo");
            entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdproveedorNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.Idproveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_servicios_proveedores");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
