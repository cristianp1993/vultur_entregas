﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VulturDeliverys.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VulturDeliverysEntities : DbContext
    {
        public VulturDeliverysEntities()
            : base("name=VulturDeliverysEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Conexion> Conexion { get; set; }
        public virtual DbSet<Emisor> Emisor { get; set; }
        public virtual DbSet<Envio> Envio { get; set; }
        public virtual DbSet<Receptor> Receptor { get; set; }
        public virtual DbSet<Trazabilidad> Trazabilidad { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
