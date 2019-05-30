using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceImplement.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbstractTravelAgencyViewWeb
{
    public static class Globals
    {
        public static ICustomerService CustomerService { get; } = new CustomerServiceList();
        public static IConditionService ConditionService { get; } = new ConditionServiceList();
        public static IVoucherService VoucherService { get; } = new VoucherServiceList();
        public static IMainService MainService { get; } = new MainServiceList();
    }
}