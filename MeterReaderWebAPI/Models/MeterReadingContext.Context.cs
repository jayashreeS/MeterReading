//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeterReaderWebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MeterReaderDBContext : DbContext
    {
        public MeterReaderDBContext()
            : base("name=MeterReaderDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<MeterReading> MeterReadings { get; set; }
    
        public virtual int spInsertMeterReading(System.Data.DataTable dT_meterReadingFile, ObjectParameter readingSuccessCount, ObjectParameter readingFailureCount)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spInsertMeterReading", readingSuccessCount, readingFailureCount);
        }
    }
}
