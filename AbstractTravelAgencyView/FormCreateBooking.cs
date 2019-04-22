using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbstractTravelAgencyView
{
    public partial class FormCreateBooking : Form
    {
        public FormCreateBooking()
        {
            InitializeComponent();
        }
        private void FormCreateBooking_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = APIClient.GetRequest<List<CustomerViewModel>>("api/Customer/GetList");
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomerFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<VoucherViewModel> listP = APIClient.GetRequest<List<VoucherViewModel>>("api/Voucher/GetList");
                if (listP != null)
                {
                    comboBoxVoucher.DisplayMember = "VoucherName";
                    comboBoxVoucher.ValueMember = "Id";
                    comboBoxVoucher.DataSource = listP;
                    comboBoxVoucher.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxVoucher.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxVoucher.SelectedValue);
                    VoucherViewModel item = APIClient.GetRequest<VoucherViewModel>("api/Voucher/Get/" + id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * item.Cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void comboBoxVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxVoucher.SelectedValue == null)
            {
                MessageBox.Show("Выберите печатное изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<BookingBindingModel, bool>("api/Main/CreateBooking", new BookingBindingModel
                {
                    CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    VoucherId = Convert.ToInt32(comboBoxVoucher.SelectedValue),
                    Amount = Convert.ToInt32(textBoxCount.Text),
                    TotalSum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
