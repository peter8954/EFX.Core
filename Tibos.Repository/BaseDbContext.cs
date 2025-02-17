﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tibos.Domain;
using Tibos.IRepository;
using Z.EntityFramework.Extensions;

namespace Tibos.Repository
{
    public class BaseDbContext : DbContext
    {
        private string ConnType { get; set; }
        private string ConnName { get; set; }


        public BaseDbContext DbContext { get; set; }
        public BaseDbContext()
        {
            
        }
        public BaseDbContext(string connType, string connName)
        {
            this.ConnName = connName;
            this.ConnType = connType;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("Admin");
            //modelBuilder.Entity<Dict>().ToTable("Dict");
            //base.OnModelCreating(modelBuilder);
        }

        //打印sql
        private static ILoggerFactory Mlogger => new LoggerFactory()
               .AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(opt => opt.Ignore(RelationalEventId.AmbientTransactionWarning));
            switch (ConnType)
            {
                case "mysql":
                    optionsBuilder.UseMySql(ConnName).UseLoggerFactory(Mlogger);
                    break;
                case "sqlserver":
                    break;
                case "sqlite":
                    break;
            }
        }
    }
}
