using System;
using System.Diagnostics.CodeAnalysis;
using GigaPark.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GigaPark.Database.Helpers
{
    /// <summary>
    ///     Klasse für das Verwalten von Datenbankkontexten.
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        ///     <para>
        ///         Override this method to configure the database (and other options) to be used for this context.
        ///         This method is called for each instance of the context that is created.
        ///         The base implementation does nothing.
        ///     </para>
        ///     <para>
        ///         In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        ///         to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        ///         the options have already been set, and skip some or all of the logic in
        ///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
        ///     for more information.
        /// </remarks>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other extensions)
        ///     typically define extension methods on this object that allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=parkhaus.db");
        }

        /// <summary>
        ///     Gibt die <see cref="DbSet{TEntity}"/>-Instanz an, die es möglich macht, LINQ-Statements in SQL-Statements für die Tabelle "Parkplatz" zu parsen.
        /// </summary>
#pragma warning disable CS8618
        public DbSet<Parkplatz> Parkplatz { get; set; }

        /// <summary>
        ///     Gibt die <see cref="DbSet{TEntity}"/>-Instanz an, die es möglich macht, LINQ-Statements in SQL-Statements für die Tabelle "Parkschein" zu parsen.
        /// </summary>
        public DbSet<Parkschein> Parkschein { get; set; }
#pragma warning restore CS8618
    }
}
