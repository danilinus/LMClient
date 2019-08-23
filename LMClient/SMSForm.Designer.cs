namespace Client
{
    partial class SMSForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SMSForm));
            this.LogintextBox = new System.Windows.Forms.TextBox();
            this.PasstextBox = new System.Windows.Forms.TextBox();
            this.PassLabel = new System.Windows.Forms.Label();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.LoginPanel = new System.Windows.Forms.Panel();
            this.Reboot = new System.Windows.Forms.Button();
            this.ShablonTextBox = new System.Windows.Forms.RichTextBox();
            this.Shablon = new System.Windows.Forms.Label();
            this.StaticLabel = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Shablon2 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LoginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogintextBox
            // 
            this.LogintextBox.Location = new System.Drawing.Point(6, 21);
            this.LogintextBox.Name = "LogintextBox";
            this.LogintextBox.Size = new System.Drawing.Size(118, 20);
            this.LogintextBox.TabIndex = 0;
            // 
            // PasstextBox
            // 
            this.PasstextBox.Location = new System.Drawing.Point(6, 62);
            this.PasstextBox.Name = "PasstextBox";
            this.PasstextBox.PasswordChar = '*';
            this.PasstextBox.Size = new System.Drawing.Size(118, 20);
            this.PasstextBox.TabIndex = 1;
            // 
            // PassLabel
            // 
            this.PassLabel.AutoSize = true;
            this.PassLabel.Location = new System.Drawing.Point(3, 46);
            this.PassLabel.Name = "PassLabel";
            this.PassLabel.Size = new System.Drawing.Size(48, 13);
            this.PassLabel.TabIndex = 2;
            this.PassLabel.Text = "Пароль:";
            // 
            // LoginLabel
            // 
            this.LoginLabel.AutoSize = true;
            this.LoginLabel.Location = new System.Drawing.Point(3, 5);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(41, 13);
            this.LoginLabel.TabIndex = 3;
            this.LoginLabel.Text = "Логин:";
            // 
            // LoginPanel
            // 
            this.LoginPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginPanel.Controls.Add(this.LoginLabel);
            this.LoginPanel.Controls.Add(this.LogintextBox);
            this.LoginPanel.Controls.Add(this.PassLabel);
            this.LoginPanel.Controls.Add(this.PasstextBox);
            this.LoginPanel.Location = new System.Drawing.Point(12, 12);
            this.LoginPanel.Name = "LoginPanel";
            this.LoginPanel.Size = new System.Drawing.Size(134, 94);
            this.LoginPanel.TabIndex = 4;
            // 
            // Reboot
            // 
            this.Reboot.Location = new System.Drawing.Point(12, 109);
            this.Reboot.Name = "Reboot";
            this.Reboot.Size = new System.Drawing.Size(134, 23);
            this.Reboot.TabIndex = 5;
            this.Reboot.Text = "Войти";
            this.Reboot.UseVisualStyleBackColor = true;
            this.Reboot.Click += new System.EventHandler(this.Reboot_Click);
            // 
            // ShablonTextBox
            // 
            this.ShablonTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ShablonTextBox.Location = new System.Drawing.Point(173, 139);
            this.ShablonTextBox.Name = "ShablonTextBox";
            this.ShablonTextBox.Size = new System.Drawing.Size(119, 131);
            this.ShablonTextBox.TabIndex = 6;
            this.ShablonTextBox.Text = "";
            this.toolTip1.SetToolTip(this.ShablonTextBox, "<num> - номер заказа\r\n<time> - текущее время\r\n<date> - текущая дата\r\n<dateZ> - да" +
        "та заказа\r\n<name> - имя заказчика\r\n<nomber> - номер заказчика\r\n<obj> - товар\r\n<q" +
        "ua> - количество");
            // 
            // Shablon
            // 
            this.Shablon.AutoSize = true;
            this.Shablon.Location = new System.Drawing.Point(170, 123);
            this.Shablon.Name = "Shablon";
            this.Shablon.Size = new System.Drawing.Size(118, 13);
            this.Shablon.TabIndex = 7;
            this.Shablon.Text = "Шаблон сообщения 1:";
            // 
            // StaticLabel
            // 
            this.StaticLabel.AutoSize = true;
            this.StaticLabel.Location = new System.Drawing.Point(418, 12);
            this.StaticLabel.Name = "StaticLabel";
            this.StaticLabel.Size = new System.Drawing.Size(68, 13);
            this.StaticLabel.TabIndex = 12;
            this.StaticLabel.Text = "Статистика:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBox1.Location = new System.Drawing.Point(421, 28);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(331, 232);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 250;
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 250;
            this.toolTip1.ReshowDelay = 50;
            this.toolTip1.ShowAlways = true;
            // 
            // Shablon2
            // 
            this.Shablon2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Shablon2.Location = new System.Drawing.Point(298, 139);
            this.Shablon2.Name = "Shablon2";
            this.Shablon2.Size = new System.Drawing.Size(119, 131);
            this.Shablon2.TabIndex = 23;
            this.Shablon2.Text = "";
            this.toolTip1.SetToolTip(this.Shablon2, "<num> - номер заказа\r\n<time> - текущее время\r\n<date> - текущая дата\r\n<dateE> - да" +
        "та заказа\r\n<name> - имя заказчика\r\n<nomber> - номер заказчика\r\n<obj> - товар\r\n<q" +
        "ua> - количество");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 14;
            this.button1.Text = "Применить изменения";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(254, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 36);
            this.button2.TabIndex = 15;
            this.button2.Text = "Узнать баланс";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(335, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 36);
            this.button3.TabIndex = 16;
            this.button3.Text = "Установить лимит СМС";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Пароль менеджера:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Новое значение:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(173, 96);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(239, 24);
            this.button4.TabIndex = 20;
            this.button4.Text = "Сменить пароль";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(173, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(119, 20);
            this.textBox1.TabIndex = 21;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(293, 28);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(119, 20);
            this.textBox2.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Шаблон сообщения 2:";
            // 
            // SMSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(764, 271);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Shablon2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.StaticLabel);
            this.Controls.Add(this.Shablon);
            this.Controls.Add(this.ShablonTextBox);
            this.Controls.Add(this.Reboot);
            this.Controls.Add(this.LoginPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(813, 310);
            this.Name = "SMSForm";
            this.Text = "СМС кабинет";
            this.Load += new System.EventHandler(this.SMSForm_Load);
            this.LoginPanel.ResumeLayout(false);
            this.LoginPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogintextBox;
        private System.Windows.Forms.TextBox PasstextBox;
        private System.Windows.Forms.Label PassLabel;
        private System.Windows.Forms.Label LoginLabel;
        private System.Windows.Forms.Panel LoginPanel;
        private System.Windows.Forms.Button Reboot;
        private System.Windows.Forms.RichTextBox ShablonTextBox;
        private System.Windows.Forms.Label Shablon;
        private System.Windows.Forms.Label StaticLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RichTextBox Shablon2;
        private System.Windows.Forms.Label label4;
    }
}