namespace Client
{
    partial class Find
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Find));
            this.panelX = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tags = new System.Windows.Forms.TextBox();
            this.FindButton = new System.Windows.Forms.Button();
            this.Loading = new System.Windows.Forms.Panel();
            this.Pot = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panelX
            // 
            this.panelX.AutoScroll = true;
            this.panelX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelX.Location = new System.Drawing.Point(12, 33);
            this.panelX.Name = "panelX";
            this.panelX.Size = new System.Drawing.Size(341, 285);
            this.panelX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(353, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 0;
            // 
            // tags
            // 
            this.tags.Location = new System.Drawing.Point(12, 7);
            this.tags.Name = "tags";
            this.tags.Size = new System.Drawing.Size(261, 20);
            this.tags.TabIndex = 3;
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(279, 6);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(74, 20);
            this.FindButton.TabIndex = 4;
            this.FindButton.Text = "Поиск";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // Loading
            // 
            this.Loading.BackColor = System.Drawing.SystemColors.Highlight;
            this.Loading.Location = new System.Drawing.Point(0, 0);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(312, 3);
            this.Loading.TabIndex = 5;
            // 
            // Pot
            // 
            this.Pot.Interval = 1;
            this.Pot.Tick += new System.EventHandler(this.Pot_Tick);
            // 
            // Find
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 330);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.tags);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelX);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(381, 369);
            this.Name = "Find";
            this.Text = "Поиск заказов";
            this.Load += new System.EventHandler(this.Find_Load);
            this.ResizeBegin += new System.EventHandler(this.Find_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Find_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tags;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.Panel Loading;
        private System.Windows.Forms.Timer Pot;
    }
}