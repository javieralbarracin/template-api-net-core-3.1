using Microsoft.EntityFrameworkCore;
using MisGastos.Core.Entities;
using MisGastos.Core.Interfaces;
using MisGastos.Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace MisGastos.Infrastructure.Context
{
    public class MisGastosContext : DbContext
    {
        public MisGastosContext()
        {
        }

        public MisGastosContext(DbContextOptions<MisGastosContext> options, IAuthenticatedUserService authenticatedUser)
            : base(options)
        {
            _authenticatedUser = authenticatedUser;
        }
        #region Entidades con Herencia
        public virtual DbSet<Gasto> Gasto { get; set; }
        public virtual DbSet<GastoCredito> GastoCredito { get; set; }
        //public virtual DbSet<GastoFijo> GastoFijo { get; set; }
        #endregion
        #region Entidades Principales       
        public virtual DbSet<GastoTipo> GastoTipo { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<Frecuencia> Frecuencia { get; set; }
        public virtual DbSet<Mes> Mes { get; set; }
        public virtual DbSet<LogFrontend> LogFrontend { get; set; }
        public virtual DbSet<GastoTipoDetalle> GastoTipoDetalle { get; set; }
        #endregion
        #region User and Security
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Security> Security { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        /**======================**/
        public virtual void Save()
        {
            base.SaveChanges();
        }
        //public string UserProvider
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(WindowsIdentity.GetCurrent().Name))
        //            return WindowsIdentity.GetCurrent().Name.Split('\\')[1];
        //        return string.Empty;
        //    }
        //}

        public Func<DateTime> TimestampProvider { get; set; } = ()
            => DateTime.UtcNow;
        public IAuthenticatedUserService _authenticatedUser { get; }

        public override int SaveChanges()
        {
            TrackChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void TrackChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            {
                if (entry.Entity is IAuditoria)
                {
                    var auditable = entry.Entity as IAuditoria;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditable.CreadoPor= _authenticatedUser.UserId;
                            auditable.CreadoFecha = TimestampProvider();
                            break;
                        case EntityState.Modified:
                            auditable.ModificadoPor = _authenticatedUser.UserId;
                            auditable.ModificadoFecha = TimestampProvider();
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            auditable.EliminadoPor = _authenticatedUser.UserId;
                            auditable.Eliminado = true;
                            auditable.EliminadoFecha = TimestampProvider();
                            break;
                    }
                    //if (entry.State == EntityState.Added)
                    //{
                    //    auditable.CreatedBy = UserProvider;
                    //    auditable.CreatedOn = TimestampProvider();                        
                    //}
                    //else
                    //{
                    //    auditable.UpdatedBy = UserProvider;
                    //    auditable.UpdatedOn = TimestampProvider();
                    //}
                }
            }
        }
        /**======================**/
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    ProcesarSalvado();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //private void ProcesarSalvado()
        //{
        //    var horaActual = DateTimeOffset.UtcNow;
        //    foreach (var item in ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Added && e.Entity is Entidad))
        //    {
        //        var entidad = item.Entity as Entidad;
        //        entidad.FechaCreacion = horaActual;
        //        entidad.UsuarioCreacion = servicioUsuarioActual.ObtenerNombreUsuarioActual();
        //        entidad.FechaModificacion = horaActual;
        //        entidad.UsuarioModificacion = servicioUsuarioActual.ObtenerNombreUsuarioActual();
        //    }

        //    foreach (var item in ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Modified && e.Entity is Entidad))
        //    {
        //        var entidad = item.Entity as Entidad;
        //        entidad.FechaModificacion = horaActual;
        //        entidad.UsuarioModificacion = servicioUsuarioActual.ObtenerNombreUsuarioActual();
        //        item.Property(nameof(entidad.FechaCreacion)).IsModified = false;
        //        item.Property(nameof(entidad.UsuarioCreacion)).IsModified = false;
        //    }
        //}
    }
}
