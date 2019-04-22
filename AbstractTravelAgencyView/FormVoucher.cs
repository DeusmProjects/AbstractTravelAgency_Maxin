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
    public partial class FormVoucher : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        private List<VoucherConditionViewModel> productComponents;
        public FormVoucher()
        {
            InitializeComponent();
        }
        private void FormVoucher_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    VoucherViewModel view = APIClient.GetRequest<VoucherViewModel>("api/Voucher/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.VoucherName;
                        textBoxPrice.Text = view.Cost.ToString();
                        productComponents = view.VoucherCondition;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                productComponents = new List<VoucherConditionViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productComponents;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormVoucherCondition();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.VoucherId = id.Value;
                    }
                    productComponents.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormVoucherCondition();
                form.Model = productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productComponents.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<VoucherConditionBindingModel> productComponentBM = new List<VoucherConditionBindingModel>();
                for (int i = 0; i < productComponents.Count; ++i)
                {
                    productComponentBM.Add(new VoucherConditionBindingModel
                    {
                        Id = productComponents[i].Id,
                        VoucherId = productComponents[i].VoucherId,
                        ConditionId = productComponents[i].ConditionId,
                        Amount = productComponents[i].Amount
                    });
                }
                if (id.HasValue)
                {
                    APIClient.PostRequest<VoucherBindingModel, bool>("api/Voucher/UpdElement", new VoucherBindingModel
                    {
                        Id = id.Value,
                        VoucherName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxPrice.Text),
                        VoucherConditions = productComponentBM
                    });
                }
                else
                {
                    APIClient.PostRequest<VoucherBindingModel, bool>("api/Voucher/AddElement", new VoucherBindingModel
                    {
                        VoucherName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxPrice.Text),
                        VoucherConditions = productComponentBM
                    });
                }
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
