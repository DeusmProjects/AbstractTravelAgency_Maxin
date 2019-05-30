namespace AbstractTravelAgencyView
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.условияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.путевкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.городаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пополнитьГородToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.прайсПутевокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загруженностьГородовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заказыКлиентовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запускРаботToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonPayOrder = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.письмаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.пополнитьГородToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.запускРаботToolStripMenuItem,
            this.письмаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1429, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.условияToolStripMenuItem,
            this.путевкиToolStripMenuItem,
            this.городаToolStripMenuItem,
            this.сотрудникиToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.справочникToolStripMenuItem.Text = "Справочник";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.клиентыToolStripMenuItem.Text = "Заказчики";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // условияToolStripMenuItem
            // 
            this.условияToolStripMenuItem.Name = "условияToolStripMenuItem";
            this.условияToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.условияToolStripMenuItem.Text = "Условия";
            this.условияToolStripMenuItem.Click += new System.EventHandler(this.компонентыToolStripMenuItem_Click);
            // 
            // путевкиToolStripMenuItem
            // 
            this.путевкиToolStripMenuItem.Name = "путевкиToolStripMenuItem";
            this.путевкиToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.путевкиToolStripMenuItem.Text = "Путевки";
            this.путевкиToolStripMenuItem.Click += new System.EventHandler(this.путевкиToolStripMenuItem_Click);
            // 
            // городаToolStripMenuItem
            // 
            this.городаToolStripMenuItem.Name = "городаToolStripMenuItem";
            this.городаToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.городаToolStripMenuItem.Text = "Города";
            this.городаToolStripMenuItem.Click += new System.EventHandler(this.городаToolStripMenuItem_Click);
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            this.сотрудникиToolStripMenuItem.Click += new System.EventHandler(this.сотрудникиToolStripMenuItem_Click);
            // 
            // пополнитьГородToolStripMenuItem
            // 
            this.пополнитьГородToolStripMenuItem.Name = "пополнитьГородToolStripMenuItem";
            this.пополнитьГородToolStripMenuItem.Size = new System.Drawing.Size(144, 24);
            this.пополнитьГородToolStripMenuItem.Text = "Пополнить город";
            this.пополнитьГородToolStripMenuItem.Click += new System.EventHandler(this.пополнитьГородToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.прайсПутевокToolStripMenuItem,
            this.загруженностьГородовToolStripMenuItem,
            this.заказыКлиентовToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(71, 24);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // прайсПутевокToolStripMenuItem
            // 
            this.прайсПутевокToolStripMenuItem.Name = "прайсПутевокToolStripMenuItem";
            this.прайсПутевокToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.прайсПутевокToolStripMenuItem.Text = "Прайс путевок";
            this.прайсПутевокToolStripMenuItem.Click += new System.EventHandler(this.прайсПутевокToolStripMenuItem_Click);
            // 
            // загруженностьГородовToolStripMenuItem
            // 
            this.загруженностьГородовToolStripMenuItem.Name = "загруженностьГородовToolStripMenuItem";
            this.загруженностьГородовToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.загруженностьГородовToolStripMenuItem.Text = "Загруженность городов";
            this.загруженностьГородовToolStripMenuItem.Click += new System.EventHandler(this.загруженностьГородовToolStripMenuItem_Click);
            // 
            // заказыКлиентовToolStripMenuItem
            // 
            this.заказыКлиентовToolStripMenuItem.Name = "заказыКлиентовToolStripMenuItem";
            this.заказыКлиентовToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.заказыКлиентовToolStripMenuItem.Text = "Заказы клиентов";
            this.заказыКлиентовToolStripMenuItem.Click += new System.EventHandler(this.заказыКлиентовToolStripMenuItem_Click);
            // 
            // запускРаботToolStripMenuItem
            // 
            this.запускРаботToolStripMenuItem.Name = "запускРаботToolStripMenuItem";
            this.запускРаботToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.запускРаботToolStripMenuItem.Text = "Запуск работ";
            this.запускРаботToolStripMenuItem.Click += new System.EventHandler(this.запускРаботToolStripMenuItem_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(13, 46);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(1173, 541);
            this.dataGridView.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1209, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 56);
            this.button1.TabIndex = 2;
            this.button1.Text = "Заказать бронь";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonCreateBooking_Click);
            // 
            // buttonPayOrder
            // 
            this.buttonPayOrder.Location = new System.Drawing.Point(1209, 281);
            this.buttonPayOrder.Name = "buttonPayOrder";
            this.buttonPayOrder.Size = new System.Drawing.Size(178, 56);
            this.buttonPayOrder.TabIndex = 5;
            this.buttonPayOrder.Text = "Бронь оплачена";
            this.buttonPayOrder.UseVisualStyleBackColor = true;
            this.buttonPayOrder.Click += new System.EventHandler(this.buttonPayBooking_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(1209, 354);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(178, 56);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Обновить список";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // письмаToolStripMenuItem
            // 
            this.письмаToolStripMenuItem.Name = "письмаToolStripMenuItem";
            this.письмаToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.письмаToolStripMenuItem.Text = "Письма";
            this.письмаToolStripMenuItem.Click += new System.EventHandler(this.письмаToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 599);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonPayOrder);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Абстрактное туристическое агенство";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem условияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem путевкиToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonPayOrder;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.ToolStripMenuItem городаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пополнитьГородToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem прайсПутевокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загруженностьГородовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заказыКлиентовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запускРаботToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem письмаToolStripMenuItem;
    }
}

