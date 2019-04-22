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
    public partial class FormCity : Form
    {
        public int Id { set { id = value; } }
        private int? id;

        public FormCity()
        {
            InitializeComponent();
        }
        private void FormCity_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CityViewModel client = APIClient.GetRequest<CityViewModel>("api/City/Get/" + id.Value);
                    textBoxCity.Text = client.CityName;
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
            if (string.IsNullOrEmpty(textBoxCity.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<CityBindingModel,
                   bool>("api/City/UpdElement", new CityBindingModel
                   {
                       Id = id.Value,
                       CityName = textBoxCity.Text
                   });
                }
                else
                {
                    APIClient.PostRequest<CityBindingModel,
                   bool>("api/City/AddElement", new CityBindingModel
                   {
                       CityName = textBoxCity.Text
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
