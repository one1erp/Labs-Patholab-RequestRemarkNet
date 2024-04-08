namespace RequestRemarkNet
{
    partial class RequestRemarkForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtFreeTextToUpdate = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtImportantTextTemplate = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtFreeTextTemplate = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdImportant = new System.Windows.Forms.Button();
            this.CmdComplete = new System.Windows.Forms.Button();
            this.CmdClear = new System.Windows.Forms.Button();
            this.CmdSave = new System.Windows.Forms.Button();
            this.CmdClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CmdUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Controls.Add(this.TxtFreeTextToUpdate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.Location = new System.Drawing.Point(12, 453);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(565, 136);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "כתיבת הערה חדשה";
            // 
            // TxtFreeTextToUpdate
            // 
            this.TxtFreeTextToUpdate.Font = new System.Drawing.Font("Arial Narrow", 12.75F);
            this.TxtFreeTextToUpdate.Location = new System.Drawing.Point(5, 16);
            this.TxtFreeTextToUpdate.Name = "TxtFreeTextToUpdate";
            this.TxtFreeTextToUpdate.Size = new System.Drawing.Size(554, 114);
            this.TxtFreeTextToUpdate.TabIndex = 0;
            this.TxtFreeTextToUpdate.Text = "";
            this.TxtFreeTextToUpdate.TextChanged += new System.EventHandler(this.TxtFreeTextToUpdate_Change);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Controls.Add(this.txtImportantTextTemplate);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox2.Location = new System.Drawing.Point(12, 302);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(565, 145);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "הערות חשובות";
            // 
            // txtImportantTextTemplate
            // 
            this.txtImportantTextTemplate.BackColor = System.Drawing.SystemColors.Window;
            this.txtImportantTextTemplate.Font = new System.Drawing.Font("Arial Narrow", 12.75F);
            this.txtImportantTextTemplate.Location = new System.Drawing.Point(6, 17);
            this.txtImportantTextTemplate.Name = "txtImportantTextTemplate";
            this.txtImportantTextTemplate.ReadOnly = true;
            this.txtImportantTextTemplate.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtImportantTextTemplate.ShowSelectionMargin = true;
            this.txtImportantTextTemplate.Size = new System.Drawing.Size(553, 122);
            this.txtImportantTextTemplate.TabIndex = 0;
            this.txtImportantTextTemplate.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox3.Controls.Add(this.TxtFreeTextTemplate);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox3.Size = new System.Drawing.Size(565, 284);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "הערות רגילות";
            // 
            // TxtFreeTextTemplate
            // 
            this.TxtFreeTextTemplate.BackColor = System.Drawing.SystemColors.Window;
            this.TxtFreeTextTemplate.Font = new System.Drawing.Font("Arial Narrow", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtFreeTextTemplate.Location = new System.Drawing.Point(6, 16);
            this.TxtFreeTextTemplate.Name = "TxtFreeTextTemplate";
            this.TxtFreeTextTemplate.ReadOnly = true;
            this.TxtFreeTextTemplate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TxtFreeTextTemplate.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.TxtFreeTextTemplate.ShowSelectionMargin = true;
            this.TxtFreeTextTemplate.Size = new System.Drawing.Size(553, 262);
            this.TxtFreeTextTemplate.TabIndex = 0;
            this.TxtFreeTextTemplate.Text = "";
            this.TxtFreeTextTemplate.Click += new System.EventHandler(this.TxtFreeTextTemplate_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.cmdImportant);
            this.panel1.Controls.Add(this.CmdComplete);
            this.panel1.Controls.Add(this.CmdClear);
            this.panel1.Controls.Add(this.CmdSave);
            this.panel1.Controls.Add(this.CmdClose);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(583, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(106, 405);
            this.panel1.TabIndex = 10;
            // 
            // cmdImportant
            // 
            this.cmdImportant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cmdImportant.Location = new System.Drawing.Point(16, 256);
            this.cmdImportant.Name = "cmdImportant";
            this.cmdImportant.Size = new System.Drawing.Size(75, 53);
            this.cmdImportant.TabIndex = 3;
            this.cmdImportant.Text = "חשוב";
            this.cmdImportant.UseVisualStyleBackColor = false;
            this.cmdImportant.Click += new System.EventHandler(this.cmdImportant_Click);
            // 
            // CmdComplete
            // 
            this.CmdComplete.BackColor = System.Drawing.Color.Lime;
            this.CmdComplete.Location = new System.Drawing.Point(16, 179);
            this.CmdComplete.Name = "CmdComplete";
            this.CmdComplete.Size = new System.Drawing.Size(75, 53);
            this.CmdComplete.TabIndex = 2;
            this.CmdComplete.Text = "משוחרר";
            this.CmdComplete.UseVisualStyleBackColor = false;
            this.CmdComplete.Click += new System.EventHandler(this.CmdComplete_Click);
            // 
            // CmdClear
            // 
            this.CmdClear.Location = new System.Drawing.Point(15, 335);
            this.CmdClear.Name = "CmdClear";
            this.CmdClear.Size = new System.Drawing.Size(75, 53);
            this.CmdClear.TabIndex = 4;
            this.CmdClear.Text = "נקה";
            this.CmdClear.UseVisualStyleBackColor = true;
            this.CmdClear.Click += new System.EventHandler(this.CmdClear_Click);
            // 
            // CmdSave
            // 
            this.CmdSave.BackColor = System.Drawing.Color.Red;
            this.CmdSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.CmdSave.Location = new System.Drawing.Point(15, 100);
            this.CmdSave.Name = "CmdSave";
            this.CmdSave.Size = new System.Drawing.Size(75, 53);
            this.CmdSave.TabIndex = 1;
            this.CmdSave.Text = "מעוכב";
            this.CmdSave.UseVisualStyleBackColor = false;
            this.CmdSave.Click += new System.EventHandler(this.CmdSave_Click);
            // 
            // CmdClose
            // 
            this.CmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.CmdClose.ForeColor = System.Drawing.Color.Black;
            this.CmdClose.Location = new System.Drawing.Point(15, 16);
            this.CmdClose.Name = "CmdClose";
            this.CmdClose.Size = new System.Drawing.Size(75, 53);
            this.CmdClose.TabIndex = 0;
            this.CmdClose.Text = "סגור";
            this.CmdClose.UseVisualStyleBackColor = true;
            this.CmdClose.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel2.Controls.Add(this.CmdUpdate);
            this.panel2.Location = new System.Drawing.Point(583, 459);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(106, 83);
            this.panel2.TabIndex = 11;
            // 
            // CmdUpdate
            // 
            this.CmdUpdate.Location = new System.Drawing.Point(16, 15);
            this.CmdUpdate.Name = "CmdUpdate";
            this.CmdUpdate.Size = new System.Drawing.Size(75, 53);
            this.CmdUpdate.TabIndex = 5;
            this.CmdUpdate.Text = "עדכן";
            this.CmdUpdate.UseVisualStyleBackColor = true;
            this.CmdUpdate.Click += new System.EventHandler(this.CmdUpdate_Click);
            // 
            // RequestRemarkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(701, 596);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RequestRemarkForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "RequestRemarkForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox TxtFreeTextToUpdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtImportantTextTemplate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox TxtFreeTextTemplate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdImportant;
        private System.Windows.Forms.Button CmdComplete;
        private System.Windows.Forms.Button CmdClear;
        private System.Windows.Forms.Button CmdSave;
        private System.Windows.Forms.Button CmdClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button CmdUpdate;
    }
}