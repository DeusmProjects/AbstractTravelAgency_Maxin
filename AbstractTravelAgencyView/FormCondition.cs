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
    public partial class FormCondition : Form
    {
        public int Id { set { id = value; } }
        private int? id;

        public FormCondition()
        {
            InitializeComponent();
        }
        private void FormCondition_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ConditionViewModel client = APIClient.GetRequest<ConditionViewModel>("api/Condition/Get/" + id.Value);
                    textBoxCondition.Text = client.ConditionName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCondition.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<ConditionBindingModel,
                   bool>("api/Condition/UpdElement", new ConditionBindingModel
                   {
                       Id = id.Value,
                       ConditionName = textBoxCondition.Text
                   });
                }
                else
                {
                    APIClient.PostRequest<ConditionBindingModel,
                   bool>("api/Condition/AddElement", new ConditionBindingModel
                   {
                       ConditionName = textBoxCondition.Text
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
