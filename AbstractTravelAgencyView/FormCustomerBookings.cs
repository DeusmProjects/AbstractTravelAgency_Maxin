using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractTravelAgencyView
{
    public partial class FormCustomerBookings : Form
    {
        public FormCustomerBookings()
        {
            InitializeComponent();
        }
        private void buttonMake_Click(object sender, EventArgs e)
        {
             if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
             {
                 MessageBox.Show("Дата начала должна быть меньше даты окончания",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             try
             {
                 ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                 "c " +
                dateTimePickerFrom.Value.ToShortDateString() +
                 " по " +
                dateTimePickerTo.Value.ToShortDateString());
                    reportViewer.LocalReport.SetParameters(parameter);
                 List<CustomerBookingsModel> response =
                APIClient.PostRequest<ReportBindingModel,
                List<CustomerBookingsModel>>("api/Report/GetCustomerBookings", new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.Value,
                    DateTo = dateTimePickerTo.Value
                });
                    ReportDataSource source = new ReportDataSource("DataSetBookings",
                   response);
                    reportViewer.LocalReport.DataSources.Add(source);
                 reportViewer.RefreshReport();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
             }
        }

        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания",
               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    APIClient.PostRequest<ReportBindingModel,
                   bool>("api/Report/SaveCustomerBookings", new ReportBindingModel
                   {
                       FileName = sfd.FileName,
                       DateFrom = dateTimePickerFrom.Value,
                       DateTo = dateTimePickerTo.Value
                   });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
    }
}
