namespace RequestRemarkNet
{
    partial class RequestRemarkControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CmdShowRemark = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.CmdShowRemark)).BeginInit();
            this.SuspendLayout();
            // 
            // CmdShowRemark
            // 
            this.CmdShowRemark.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CmdShowRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.CmdShowRemark.Location = new System.Drawing.Point(1, 1);
            this.CmdShowRemark.Name = "CmdShowRemark";
            // 
            // 
            // 
            this.CmdShowRemark.RootElement.AccessibleDescription = null;
            this.CmdShowRemark.RootElement.AccessibleName = null;
            this.CmdShowRemark.RootElement.ControlBounds = new System.Drawing.Rectangle(1, 1, 110, 24);
            this.CmdShowRemark.Size = new System.Drawing.Size(98, 29);
            this.CmdShowRemark.TabIndex = 1;
            this.CmdShowRemark.Text = "הערות";
            this.CmdShowRemark.Click += new System.EventHandler(this.CmdShowRemark_Click);
            // 
            // RequestRemarkControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CmdShowRemark);
            this.Name = "RequestRemarkControl";
            this.Size = new System.Drawing.Size(100, 31);
            this.Load += new System.EventHandler(this.RequestRemarkControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CmdShowRemark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton CmdShowRemark;
    }
}
