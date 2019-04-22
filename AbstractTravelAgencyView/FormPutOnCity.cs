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

namespace AbstractTravelAgencyView
{
    public partial class FormPutOnCity : Form
    {
        public FormPutOnCity()
        {
            InitializeComponent();
        }

        private void FormPutOnCity_Load(object sender, EventArgs e)
        {
            try
            {
                List<ConditionViewModel> listC = APIClient.GetRequest<List<ConditionViewModel>>("api/Condition/GetList");
                if (listC != null)
                {
                    comboBoxCondition.DisplayMember = "ConditionName";
                    comboBoxCondition.ValueMember = "Id";
                    comboBoxCondition.DataSource = listC;
                    comboBoxCondition.SelectedItem = null;
                }
                List<CityViewModel> listS = APIClient.GetRequest<List<CityViewModel>>("api/City/GetList");
                if (listS != null)
                {
                    comboBoxCity.DisplayMember = "CityName";
                    comboBoxCity.ValueMember = "Id";
                    comboBoxCity.DataSource = listS;
                    comboBoxCity.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAmount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCondition.SelectedValue == null)
            {
                MessageBox.Show("Выберите условие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCity.SelectedValue == null)
            {
                MessageBox.Show("Выберите город", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<CityConditionBindingModel, bool>("api/Main/PutConditionOnCity", new CityConditionBindingModel
                {
                    ConditionId = Convert.ToInt32(comboBoxCondition.SelectedValue),
                    CityId = Convert.ToInt32(comboBoxCity.SelectedValue),
                    Amount = Convert.ToInt32(textBoxAmount.Text)
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
