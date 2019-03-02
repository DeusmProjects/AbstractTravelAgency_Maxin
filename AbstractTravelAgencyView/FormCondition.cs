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
using Unity;

namespace AbstractTravelAgencyView
{
    public partial class FormCondition : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IConditionService service;
        private int? id;

        public FormCondition(IConditionService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormCondition_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ConditionViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxCondition.Text = view.ConditionName;
                    }
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
                MessageBox.Show("Заполните условие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new ConditionBindingModel
                    {
                        Id = id.Value,
                        ConditionName = textBoxCondition.Text
                    });
                }
                else
                {
                    service.AddElement(new ConditionBindingModel
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
