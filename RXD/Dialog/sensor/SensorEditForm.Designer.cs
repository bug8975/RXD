namespace RXD
{
    partial class SensorEditForm
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(116, 257);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 29);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "保存";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(85, 103);
            this.textEdit3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(125, 24);
            this.textEdit3.TabIndex = 26;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(28, 106);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(45, 18);
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "端口号";
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Location = new System.Drawing.Point(85, 197);
            this.comboBoxEdit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit2.Size = new System.Drawing.Size(125, 24);
            this.comboBoxEdit2.TabIndex = 24;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(13, 200);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 18);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "是否基站";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(85, 150);
            this.comboBoxEdit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(125, 24);
            this.comboBoxEdit1.TabIndex = 22;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 153);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 18);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "启用状态";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(85, 56);
            this.textEdit2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(125, 24);
            this.textEdit2.TabIndex = 19;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(85, 9);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 20);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(125, 24);
            this.textEdit1.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(28, 59);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 18);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "监测点";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 18);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "监测线";
            // 
            // SensorEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 316);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.comboBoxEdit2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.Name = "SensorEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑";
            this.Load += new System.EventHandler(this.SensorEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
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
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}