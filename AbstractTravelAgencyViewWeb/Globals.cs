using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceImplement.Implementations;
using AbstractTravelAgencyServiceImplementDataBase;
using AbstractTravelAgencyServiceImplementDataBase.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbstractTravelAgencyViewWeb
{
    public static class Globals
    {
        public static AbstractDbScope DbScope { get; } = new AbstractDbScope();
        public static ICustomerService CustomerService { get; } = new CustomerServiceDB(DbScope);
        public static IConditionService ConditionService { get; } = new ConditionServiceDB(DbScope);
        public static IVoucherService VoucherService { get; } = new VoucherServiceDB(DbScope);
        public static IMainService MainService { get; } = new MainServiceDB(DbScope);
        public static ICityService CityService { get; } = new CityServiceDB(DbScope);
    }
}