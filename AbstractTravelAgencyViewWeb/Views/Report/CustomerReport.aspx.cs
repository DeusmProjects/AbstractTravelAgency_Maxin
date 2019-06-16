using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceImplementDataBase;
using Microsoft.Reporting.WebForms;

namespace AbstractTravelAgencyViewWeb.Views.Report
{
    public partial class CustomerReport : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportCustomerBookings.rdlc");
            AbstractDbScope entities = new AbstractDbScope();
            ReportDataSource datasource = new ReportDataSource("Bookings", (from customer in entities.Bookings
                select customer));
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
    }
}