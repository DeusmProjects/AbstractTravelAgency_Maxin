using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceImplementDataBase.Implementations;
using AbstractTravelAgencyServiceImplement.Implementations;
using AbstractTravelAgencyServiceImplementDataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using AbstractGarmentFactoryServiceImplementDataBase.Implementations;

namespace AbstractTravelAgencyView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbScope>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICustomerService, CustomerServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConditionService, ConditionServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IVoucherService, VoucherServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ICityService, CityServiceDB>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
