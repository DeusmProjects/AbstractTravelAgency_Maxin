using AbstractTravelAgencyModel;
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
using Unity;

namespace AbstractTravelAgencyView
{
    public partial class FormVoucherCondition : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public VoucherConditionViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private readonly IConditionService service;
        private VoucherConditionViewModel model;
        public FormVoucherCondition(IConditionService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormProductComponent_Load(object sender, EventArgs e)
        {
            try
            {
                List<ConditionViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBox.DisplayMember = "ConditionName";
                    comboBox.ValueMember = "Id";
                    comboBox.DataSource = list;
                    comboBox.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBox.Enabled = false;
                comboBox.SelectedValue = model.ConditionId;
                textBoxCount.Text = model.Amount.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите условие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new VoucherConditionViewModel
                    {
                        ConditionId = Convert.ToInt32(comboBox.SelectedValue),
                        ConditionName = comboBox.Text,
                        Amount = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Amount = Convert.ToInt32(textBoxCount.Text);
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
