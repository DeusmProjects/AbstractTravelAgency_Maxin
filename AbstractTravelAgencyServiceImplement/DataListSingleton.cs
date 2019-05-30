using AbstractTravelAgencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTravelAgencyServiceImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Customer> Customers { get; set; }

        public List<Condition> Conditions { get; set; }

        public List<Booking> Bookings { get; set; }

        public List<Voucher> Vouchers { get; set; }

        public List<VoucherCondition> VoucherConditions { get; set; }

        public List<City> Cities { get; set; }

        public List<CityCondition> CityConditions { get; set; }

        private DataListSingleton()
        {
            Customers = new List<Customer>();
            Conditions = new List<Condition>();
            Bookings = new List<Booking>();
            Vouchers = new List<Voucher>();
            VoucherConditions = new List<VoucherCondition>();
            Cities = new List<City>();
            CityConditions = new List<CityCondition>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
