using AbstractTravelAgencyModel;
using AbstractTravelAgencyServiceDAL.BindingModel;
using AbstractTravelAgencyServiceDAL.Interfaces;
using AbstractTravelAgencyServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AbstractTravelAgencyView
{
    public partial class FormVoucher : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IVoucherService service;
        private int? id;
        private List<VoucherConditionViewModel> voucherConditions;
        public FormVoucher(IVoucherService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    VoucherViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.VoucherName;
                        textBoxPrice.Text = view.Cost.ToString();
                        voucherConditions = view.VoucherConditions;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                voucherConditions = new List<VoucherConditionViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (voucherConditions != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = voucherConditions;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormVoucherCondition>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.VoucherId = id.Value;
                    }
                    voucherConditions.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormVoucherCondition>();
                form.Model =
               voucherConditions[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    voucherConditions[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        voucherConditions.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
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
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (voucherConditions == null || voucherConditions.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<VoucherConditionBindingModel> voucherConditionBM = new List<VoucherConditionBindingModel>();
                for (int i = 0; i < voucherConditions.Count; ++i)
                {
                    voucherConditionBM.Add(new VoucherConditionBindingModel
                    {
                        Id = voucherConditions[i].Id,
                        VoucherId = voucherConditions[i].VoucherId,
                        ConditionId = voucherConditions[i].ConditionId,
                        Amount = voucherConditions[i].Amount
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new VoucherBindingModel
                    {
                        Id = id.Value,
                        VoucherName = textBoxName.Text,
                        Cost = Convert.ToInt32(textBoxPrice.Text),
                        VoucherConditions = voucherConditionBM
                    });
                }
                else
                {
                     service.AddElement(new VoucherBindingModel
                     {
                         VoucherName = textBoxName.Text,
                         Cost = Convert.ToInt32(textBoxPrice.Text),
                         VoucherConditions = voucherConditionBM
                     });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
