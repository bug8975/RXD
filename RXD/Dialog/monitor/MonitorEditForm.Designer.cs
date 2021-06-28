namespace RXD
{
    partial class MonitorEditForm
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(120, 207);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 29);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "保存";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(89, 101);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.BeepOnError = false;
            this.textEdit3.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.textEdit3.Properties.MaskSettings.Set("mask", "d");
            this.textEdit3.Properties.UseMaskAsDisplayFormat = true;
            this.textEdit3.Size = new System.Drawing.Size(125, 24);
            this.textEdit3.TabIndex = 17;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(14, 64);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(45, 18);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "监测线";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(89, 151);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(125, 24);
            this.comboBoxEdit1.TabIndex = 15;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(14, 154);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(45, 18);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "传感器";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(89, 58);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(125, 24);
            this.textEdit2.TabIndex = 12;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(89, 16);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(125, 24);
            this.textEdit1.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 107);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 18);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "频率(秒)";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 18);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "项目";
            // 
            // MonitorEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 260);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Name = "MonitorEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑";
            this.Load += new System.EventHandler(this.MonitorEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}