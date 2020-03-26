using DMAnaliseSentimento.Web.API.Domain;
using DMAnaliseSentimento.Web.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMAnaliseSentimento.Web.API.EntityContext
{
    public class DMAnaliseSentimentoContext : DbContext
    {
        public DMAnaliseSentimentoContext(DbContextOptions<DMAnaliseSentimentoContext> options) : base(options) { }
        
        public DbSet<DataSet> DataSet { get; set; }
        public DbSet<ResultadoTreinamento> ResultadoTreinamento { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSet>().ToTable("DataSet");
            modelBuilder.Entity<ResultadoTreinamento>().ToTable("ResultadoTreinamento");
        }
    }
}
