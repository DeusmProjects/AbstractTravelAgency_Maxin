using AbstractTravelAgencyModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplementDataBase
{
    public class AbstractDbScope : DbContext
    {
        public AbstractDbScope() : base("AbstractDatabaseWeb")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<VoucherCondition> VoucherConditions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<CityCondition> CityConditions { get; set; }

    }
}
