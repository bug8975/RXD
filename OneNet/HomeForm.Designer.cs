namespace OneNet
{
    partial class HomeForm
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
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.midPanel = new DevExpress.XtraEditors.SidePanel();
            this.monitorPanel = new DevExpress.XtraEditors.SidePanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.monitorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.onenetDataSet = new OneNet.onenetDataSet();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprojectid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.addLinkHiper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.monitorAdd = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.editLinkHiper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.monitorEdit = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.delLinkHiper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.monitorDel = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.monitorCheck = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.sensorPanel = new DevExpress.XtraEditors.SidePanel();
            this.sidePanel7 = new DevExpress.XtraEditors.SidePanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.sidePanel6 = new DevExpress.XtraEditors.SidePanel();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.sensorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colname1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmonitorid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.addSensorLinkHiper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sensorAddLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.editSensorLinkHiper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sensorEditLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.delSensorLinkHiper = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sensorDelLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.snPanel = new DevExpress.XtraEditors.SidePanel();
            this.gridControl4 = new DevExpress.XtraGrid.GridControl();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colid3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmonitorid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colmonitorname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsensorid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colsensorname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltype = new DevExpress.XtraGrid.Columns.GridColumn();
            this.snAddColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.snAddLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.snEditColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.snEditLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.snDelColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.snDelLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.ipPanel = new DevExpress.XtraEditors.SidePanel();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.urlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colid2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colport = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colprojectid1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ipAddColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ipAddLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.ipEditColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ipEditLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.ipDelColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ipDelLink = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.navBarPanel = new DevExpress.XtraEditors.SidePanel();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.titlePanel = new DevExpress.XtraEditors.SidePanel();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.monitorTableAdapter = new OneNet.onenetDataSetTableAdapters.monitorTableAdapter();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.sensorTableAdapter = new OneNet.onenetDataSetTableAdapters.sensorTableAdapter();
            this.urlTableAdapter = new OneNet.onenetDataSetTableAdapters.urlTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.midPanel.SuspendLayout();
            this.monitorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.onenetDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorDel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorCheck)).BeginInit();
            this.sensorPanel.SuspendLayout();
            this.sidePanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.sidePanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorAddLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorEditLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorDelLink)).BeginInit();
            this.snPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.snAddLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.snEditLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.snDelLink)).BeginInit();
            this.ipPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.urlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipAddLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipEditLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipDelLink)).BeginInit();
            this.navBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.titlePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSize = true;
            this.panelControl1.Controls.Add(this.midPanel);
            this.panelControl1.Controls.Add(this.navBarPanel);
            this.panelControl1.Controls.Add(this.titlePanel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(827, 356);
            this.panelControl1.TabIndex = 0;
            // 
            // midPanel
            // 
            this.midPanel.Controls.Add(this.monitorPanel);
            this.midPanel.Controls.Add(this.sensorPanel);
            this.midPanel.Controls.Add(this.snPanel);
            this.midPanel.Controls.Add(this.ipPanel);
            this.midPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midPanel.Location = new System.Drawing.Point(174, 64);
            this.midPanel.Name = "midPanel";
            this.midPanel.Size = new System.Drawing.Size(651, 290);
            this.midPanel.TabIndex = 2;
            this.midPanel.Text = "sidePanel3";
            // 
            // monitorPanel
            // 
            this.monitorPanel.Controls.Add(this.gridControl1);
            this.monitorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorPanel.Location = new System.Drawing.Point(0, 0);
            this.monitorPanel.Name = "monitorPanel";
            this.monitorPanel.Size = new System.Drawing.Size(651, 290);
            this.monitorPanel.TabIndex = 2;
            this.monitorPanel.Text = "sidePanel4";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.monitorBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.monitorCheck,
            this.monitorEdit,
            this.monitorDel,
            this.monitorAdd});
            this.gridControl1.Size = new System.Drawing.Size(651, 290);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // monitorBindingSource
            // 
            this.monitorBindingSource.DataMember = "monitor";
            this.monitorBindingSource.DataSource = this.onenetDataSet;
            // 
            // onenetDataSet
            // 
            this.onenetDataSet.DataSetName = "onenetDataSet";
            this.onenetDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colid,
            this.colname,
            this.colprojectid,
            this.addLinkHiper,
            this.editLinkHiper,
            this.delLinkHiper});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colid
            // 
            this.colid.AppearanceCell.Options.UseTextOptions = true;
            this.colid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colid.FieldName = "id";
            this.colid.MinWidth = 25;
            this.colid.Name = "colid";
            this.colid.Width = 94;
            // 
            // colname
            // 
            this.colname.AppearanceCell.Options.UseTextOptions = true;
            this.colname.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colname.Caption = "测线名称";
            this.colname.FieldName = "name";
            this.colname.MinWidth = 25;
            this.colname.Name = "colname";
            this.colname.OptionsColumn.AllowEdit = false;
            this.colname.Visible = true;
            this.colname.VisibleIndex = 0;
            this.colname.Width = 94;
            // 
            // colprojectid
            // 
            this.colprojectid.AppearanceCell.Options.UseTextOptions = true;
            this.colprojectid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colprojectid.FieldName = "projectid";
            this.colprojectid.MinWidth = 25;
            this.colprojectid.Name = "colprojectid";
            this.colprojectid.Width = 94;
            // 
            // addLinkHiper
            // 
            this.addLinkHiper.AppearanceCell.Options.UseTextOptions = true;
            this.addLinkHiper.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.addLinkHiper.Caption = "新增";
            this.addLinkHiper.ColumnEdit = this.monitorAdd;
            this.addLinkHiper.MinWidth = 25;
            this.addLinkHiper.Name = "addLinkHiper";
            this.addLinkHiper.Visible = true;
            this.addLinkHiper.VisibleIndex = 1;
            this.addLinkHiper.Width = 94;
            // 
            // monitorAdd
            // 
            this.monitorAdd.AutoHeight = false;
            this.monitorAdd.Name = "monitorAdd";
            this.monitorAdd.NullText = "新增";
            this.monitorAdd.Click += new System.EventHandler(this.monitorAdd_Click);
            // 
            // editLinkHiper
            // 
            this.editLinkHiper.AppearanceCell.Options.UseTextOptions = true;
            this.editLinkHiper.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.editLinkHiper.Caption = "编辑";
            this.editLinkHiper.ColumnEdit = this.monitorEdit;
            this.editLinkHiper.MinWidth = 25;
            this.editLinkHiper.Name = "editLinkHiper";
            this.editLinkHiper.Visible = true;
            this.editLinkHiper.VisibleIndex = 2;
            this.editLinkHiper.Width = 94;
            // 
            // monitorEdit
            // 
            this.monitorEdit.AutoHeight = false;
            this.monitorEdit.Name = "monitorEdit";
            this.monitorEdit.NullText = "编辑";
            this.monitorEdit.Click += new System.EventHandler(this.monitorEdit_Click);
            // 
            // delLinkHiper
            // 
            this.delLinkHiper.AppearanceCell.Options.UseTextOptions = true;
            this.delLinkHiper.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.delLinkHiper.Caption = "删除";
            this.delLinkHiper.ColumnEdit = this.monitorDel;
            this.delLinkHiper.MinWidth = 25;
            this.delLinkHiper.Name = "delLinkHiper";
            this.delLinkHiper.Visible = true;
            this.delLinkHiper.VisibleIndex = 3;
            this.delLinkHiper.Width = 94;
            // 
            // monitorDel
            // 
            this.monitorDel.AutoHeight = false;
            this.monitorDel.Name = "monitorDel";
            this.monitorDel.NullText = "删除";
            this.monitorDel.Click += new System.EventHandler(this.monitorDel_Click);
            // 
            // monitorCheck
            // 
            this.monitorCheck.AutoHeight = false;
            this.monitorCheck.Name = "monitorCheck";
            // 
            // sensorPanel
            // 
            this.sensorPanel.Controls.Add(this.sidePanel7);
            this.sensorPanel.Controls.Add(this.sidePanel6);
            this.sensorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sensorPanel.Location = new System.Drawing.Point(0, 0);
            this.sensorPanel.Name = "sensorPanel";
            this.sensorPanel.Size = new System.Drawing.Size(651, 290);
            this.sensorPanel.TabIndex = 2;
            this.sensorPanel.Text = "sidePanel5";
            this.sensorPanel.Visible = false;
            // 
            // sidePanel7
            // 
            this.sidePanel7.Controls.Add(this.labelControl1);
            this.sidePanel7.Controls.Add(this.comboBoxEdit1);
            this.sidePanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidePanel7.Location = new System.Drawing.Point(0, 0);
            this.sidePanel7.Name = "sidePanel7";
            this.sidePanel7.Size = new System.Drawing.Size(651, 36);
            this.sidePanel7.TabIndex = 0;
            this.sidePanel7.Text = "sidePanel7";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 18);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "请选择";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(83, 10);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(125, 24);
            this.comboBoxEdit1.TabIndex = 0;
            this.comboBoxEdit1.SelectedValueChanged += new System.EventHandler(this.comboBoxEdit1_SelectedValueChanged);
            // 
            // sidePanel6
            // 
            this.sidePanel6.Controls.Add(this.gridControl2);
            this.sidePanel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sidePanel6.Location = new System.Drawing.Point(0, 36);
            this.sidePanel6.Name = "sidePanel6";
            this.sidePanel6.Size = new System.Drawing.Size(651, 254);
            this.sidePanel6.TabIndex = 1;
            this.sidePanel6.Text = "sidePanel6";
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.sensorBindingSource;
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 1);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.sensorAddLink,
            this.sensorEditLink,
            this.sensorDelLink});
            this.gridControl2.Size = new System.Drawing.Size(651, 253);
            this.gridControl2.TabIndex = 2;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // sensorBindingSource
            // 
            this.sensorBindingSource.DataMember = "sensor";
            this.sensorBindingSource.DataSource = this.onenetDataSet;
            // 
            // gridView2
            // 
            this.gridView2.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colid1,
            this.colname1,
            this.colmonitorid,
            this.addSensorLinkHiper,
            this.editSensorLinkHiper,
            this.delSensorLinkHiper});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colid1
            // 
            this.colid1.AppearanceCell.Options.UseTextOptions = true;
            this.colid1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colid1.FieldName = "id";
            this.colid1.MinWidth = 25;
            this.colid1.Name = "colid1";
            this.colid1.Width = 94;
            // 
            // colname1
            // 
            this.colname1.AppearanceCell.Options.UseTextOptions = true;
            this.colname1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colname1.Caption = "测点名称";
            this.colname1.FieldName = "name";
            this.colname1.MinWidth = 25;
            this.colname1.Name = "colname1";
            this.colname1.OptionsColumn.AllowEdit = false;
            this.colname1.Visible = true;
            this.colname1.VisibleIndex = 0;
            this.colname1.Width = 94;
            // 
            // colmonitorid
            // 
            this.colmonitorid.AppearanceCell.Options.UseTextOptions = true;
            this.colmonitorid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colmonitorid.FieldName = "monitorid";
            this.colmonitorid.MinWidth = 25;
            this.colmonitorid.Name = "colmonitorid";
            this.colmonitorid.Width = 94;
            // 
            // addSensorLinkHiper
            // 
            this.addSensorLinkHiper.AppearanceCell.Options.UseTextOptions = true;
            this.addSensorLinkHiper.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.addSensorLinkHiper.Caption = "新增";
            this.addSensorLinkHiper.ColumnEdit = this.sensorAddLink;
            this.addSensorLinkHiper.MinWidth = 25;
            this.addSensorLinkHiper.Name = "addSensorLinkHiper";
            this.addSensorLinkHiper.Visible = true;
            this.addSensorLinkHiper.VisibleIndex = 1;
            this.addSensorLinkHiper.Width = 94;
            // 
            // sensorAddLink
            // 
            this.sensorAddLink.AutoHeight = false;
            this.sensorAddLink.Name = "sensorAddLink";
            this.sensorAddLink.NullText = "新增";
            this.sensorAddLink.Click += new System.EventHandler(this.sensorAddLink_Click);
            // 
            // editSensorLinkHiper
            // 
            this.editSensorLinkHiper.AppearanceCell.Options.UseTextOptions = true;
            this.editSensorLinkHiper.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.editSensorLinkHiper.Caption = "编辑";
            this.editSensorLinkHiper.ColumnEdit = this.sensorEditLink;
            this.editSensorLinkHiper.MinWidth = 25;
            this.editSensorLinkHiper.Name = "editSensorLinkHiper";
            this.editSensorLinkHiper.Visible = true;
            this.editSensorLinkHiper.VisibleIndex = 2;
            this.editSensorLinkHiper.Width = 94;
            // 
            // sensorEditLink
            // 
            this.sensorEditLink.AutoHeight = false;
            this.sensorEditLink.Name = "sensorEditLink";
            this.sensorEditLink.NullText = "编辑";
            this.sensorEditLink.Click += new System.EventHandler(this.sensorEditLink_Click);
            // 
            // delSensorLinkHiper
            // 
            this.delSensorLinkHiper.AppearanceCell.Options.UseTextOptions = true;
            this.delSensorLinkHiper.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.delSensorLinkHiper.Caption = "删除";
            this.delSensorLinkHiper.ColumnEdit = this.sensorDelLink;
            this.delSensorLinkHiper.MinWidth = 25;
            this.delSensorLinkHiper.Name = "delSensorLinkHiper";
            this.delSensorLinkHiper.Visible = true;
            this.delSensorLinkHiper.VisibleIndex = 3;
            this.delSensorLinkHiper.Width = 94;
            // 
            // sensorDelLink
            // 
            this.sensorDelLink.AutoHeight = false;
            this.sensorDelLink.Name = "sensorDelLink";
            this.sensorDelLink.NullText = "删除";
            this.sensorDelLink.Click += new System.EventHandler(this.sensorDelLink_Click);
            // 
            // snPanel
            // 
            this.snPanel.Controls.Add(this.gridControl4);
            this.snPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snPanel.Location = new System.Drawing.Point(0, 0);
            this.snPanel.Name = "snPanel";
            this.snPanel.Size = new System.Drawing.Size(651, 290);
            this.snPanel.TabIndex = 2;
            this.snPanel.Text = "sidePanel1";
            this.snPanel.Visible = false;
            // 
            // gridControl4
            // 
            this.gridControl4.DataMember = "Query";
            this.gridControl4.DataSource = this.sqlDataSource1;
            this.gridControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl4.Location = new System.Drawing.Point(0, 0);
            this.gridControl4.MainView = this.gridView4;
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.snAddLink,
            this.snEditLink,
            this.snDelLink});
            this.gridControl4.Size = new System.Drawing.Size(651, 290);
            this.gridControl4.TabIndex = 2;
            this.gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "localhost_onenet_Connection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "Query";
            customSqlQuery1.Sql = resources.GetString("customSqlQuery1.Sql");
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // gridView4
            // 
            this.gridView4.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView4.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colid3,
            this.colsn,
            this.colmonitorid1,
            this.colmonitorname,
            this.colsensorid,
            this.colsensorname,
            this.coltype,
            this.snAddColumn,
            this.snEditColumn,
            this.snDelColumn});
            this.gridView4.GridControl = this.gridControl4;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView4_CustomColumnDisplayText);
            // 
            // colid3
            // 
            this.colid3.AppearanceCell.Options.UseTextOptions = true;
            this.colid3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colid3.FieldName = "id";
            this.colid3.MinWidth = 25;
            this.colid3.Name = "colid3";
            this.colid3.Width = 94;
            // 
            // colsn
            // 
            this.colsn.AppearanceCell.Options.UseTextOptions = true;
            this.colsn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colsn.Caption = "SN编号";
            this.colsn.FieldName = "sn";
            this.colsn.MinWidth = 25;
            this.colsn.Name = "colsn";
            this.colsn.OptionsColumn.AllowEdit = false;
            this.colsn.Visible = true;
            this.colsn.VisibleIndex = 0;
            this.colsn.Width = 94;
            // 
            // colmonitorid1
            // 
            this.colmonitorid1.AppearanceCell.Options.UseTextOptions = true;
            this.colmonitorid1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colmonitorid1.FieldName = "monitorid";
            this.colmonitorid1.MinWidth = 25;
            this.colmonitorid1.Name = "colmonitorid1";
            this.colmonitorid1.Width = 94;
            // 
            // colmonitorname
            // 
            this.colmonitorname.AppearanceCell.Options.UseTextOptions = true;
            this.colmonitorname.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colmonitorname.Caption = "监测线";
            this.colmonitorname.FieldName = "monitorname";
            this.colmonitorname.MinWidth = 25;
            this.colmonitorname.Name = "colmonitorname";
            this.colmonitorname.OptionsColumn.AllowEdit = false;
            this.colmonitorname.Visible = true;
            this.colmonitorname.VisibleIndex = 1;
            this.colmonitorname.Width = 94;
            // 
            // colsensorid
            // 
            this.colsensorid.AppearanceCell.Options.UseTextOptions = true;
            this.colsensorid.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colsensorid.FieldName = "sensorid";
            this.colsensorid.MinWidth = 25;
            this.colsensorid.Name = "colsensorid";
            this.colsensorid.Width = 94;
            // 
            // colsensorname
            // 
            this.colsensorname.AppearanceCell.Options.UseTextOptions = true;
            this.colsensorname.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colsensorname.Caption = "监测点";
            this.colsensorname.FieldName = "sensorname";
            this.colsensorname.MinWidth = 25;
            this.colsensorname.Name = "colsensorname";
            this.colsensorname.OptionsColumn.AllowEdit = false;
            this.colsensorname.Visible = true;
            this.colsensorname.VisibleIndex = 2;
            this.colsensorname.Width = 94;
            // 
            // coltype
            // 
            this.coltype.AppearanceCell.Options.UseTextOptions = true;
            this.coltype.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coltype.Caption = "类型";
            this.coltype.FieldName = "type";
            this.coltype.MinWidth = 25;
            this.coltype.Name = "coltype";
            this.coltype.OptionsColumn.AllowEdit = false;
            this.coltype.Visible = true;
            this.coltype.VisibleIndex = 3;
            this.coltype.Width = 94;
            // 
            // snAddColumn
            // 
            this.snAddColumn.AppearanceCell.Options.UseTextOptions = true;
            this.snAddColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.snAddColumn.Caption = "新增";
            this.snAddColumn.ColumnEdit = this.snAddLink;
            this.snAddColumn.MinWidth = 25;
            this.snAddColumn.Name = "snAddColumn";
            this.snAddColumn.Visible = true;
            this.snAddColumn.VisibleIndex = 4;
            this.snAddColumn.Width = 94;
            // 
            // snAddLink
            // 
            this.snAddLink.AutoHeight = false;
            this.snAddLink.Name = "snAddLink";
            this.snAddLink.NullText = "新增";
            this.snAddLink.Click += new System.EventHandler(this.snAddLink_Click);
            // 
            // snEditColumn
            // 
            this.snEditColumn.AppearanceCell.Options.UseTextOptions = true;
            this.snEditColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.snEditColumn.Caption = "编辑";
            this.snEditColumn.ColumnEdit = this.snEditLink;
            this.snEditColumn.MinWidth = 25;
            this.snEditColumn.Name = "snEditColumn";
            this.snEditColumn.Visible = true;
            this.snEditColumn.VisibleIndex = 5;
            this.snEditColumn.Width = 94;
            // 
            // snEditLink
            // 
            this.snEditLink.AutoHeight = false;
            this.snEditLink.Name = "snEditLink";
            this.snEditLink.NullText = "编辑";
            this.snEditLink.Click += new System.EventHandler(this.snEditLink_Click);
            // 
            // snDelColumn
            // 
            this.snDelColumn.AppearanceCell.Options.UseTextOptions = true;
            this.snDelColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.snDelColumn.Caption = "删除";
            this.snDelColumn.ColumnEdit = this.snDelLink;
            this.snDelColumn.MinWidth = 25;
            this.snDelColumn.Name = "snDelColumn";
            this.snDelColumn.Visible = true;
            this.snDelColumn.VisibleIndex = 6;
            this.snDelColumn.Width = 94;
            // 
            // snDelLink
            // 
            this.snDelLink.AutoHeight = false;
            this.snDelLink.Name = "snDelLink";
            this.snDelLink.NullText = "删除";
            this.snDelLink.Click += new System.EventHandler(this.snDelLink_Click);
            // 
            // ipPanel
            // 
            this.ipPanel.Controls.Add(this.gridControl3);
            this.ipPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipPanel.Location = new System.Drawing.Point(0, 0);
            this.ipPanel.Name = "ipPanel";
            this.ipPanel.Size = new System.Drawing.Size(651, 290);
            this.ipPanel.TabIndex = 2;
            this.ipPanel.Text = "sidePanel1";
            this.ipPanel.Visible = false;
            // 
            // gridControl3
            // 
            this.gridControl3.DataSource = this.urlBindingSource;
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.Location = new System.Drawing.Point(0, 0);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ipAddLink,
            this.ipEditLink,
            this.ipDelLink});
            this.gridControl3.Size = new System.Drawing.Size(651, 290);
            this.gridControl3.TabIndex = 2;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // urlBindingSource
            // 
            this.urlBindingSource.DataMember = "url";
            this.urlBindingSource.DataSource = this.onenetDataSet;
            // 
            // gridView3
            // 
            this.gridView3.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView3.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colid2,
            this.colip,
            this.colport,
            this.colprojectid1,
            this.ipAddColumn,
            this.ipEditColumn,
            this.ipDelColumn});
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colid2
            // 
            this.colid2.AppearanceCell.Options.UseTextOptions = true;
            this.colid2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colid2.FieldName = "id";
            this.colid2.MinWidth = 25;
            this.colid2.Name = "colid2";
            this.colid2.Width = 94;
            // 
            // colip
            // 
            this.colip.AppearanceCell.Options.UseTextOptions = true;
            this.colip.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colip.Caption = "IP地址";
            this.colip.FieldName = "ip";
            this.colip.MinWidth = 25;
            this.colip.Name = "colip";
            this.colip.OptionsColumn.AllowEdit = false;
            this.colip.Visible = true;
            this.colip.VisibleIndex = 0;
            this.colip.Width = 94;
            // 
            // colport
            // 
            this.colport.AppearanceCell.Options.UseTextOptions = true;
            this.colport.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colport.Caption = "端口";
            this.colport.FieldName = "port";
            this.colport.MinWidth = 25;
            this.colport.Name = "colport";
            this.colport.OptionsColumn.AllowEdit = false;
            this.colport.Visible = true;
            this.colport.VisibleIndex = 1;
            this.colport.Width = 94;
            // 
            // colprojectid1
            // 
            this.colprojectid1.AppearanceCell.Options.UseTextOptions = true;
            this.colprojectid1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colprojectid1.FieldName = "projectid";
            this.colprojectid1.MinWidth = 25;
            this.colprojectid1.Name = "colprojectid1";
            this.colprojectid1.Width = 94;
            // 
            // ipAddColumn
            // 
            this.ipAddColumn.AppearanceCell.Options.UseTextOptions = true;
            this.ipAddColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ipAddColumn.Caption = "新增";
            this.ipAddColumn.ColumnEdit = this.ipAddLink;
            this.ipAddColumn.MinWidth = 25;
            this.ipAddColumn.Name = "ipAddColumn";
            this.ipAddColumn.Visible = true;
            this.ipAddColumn.VisibleIndex = 2;
            this.ipAddColumn.Width = 94;
            // 
            // ipAddLink
            // 
            this.ipAddLink.AutoHeight = false;
            this.ipAddLink.Name = "ipAddLink";
            this.ipAddLink.NullText = "新增";
            this.ipAddLink.Click += new System.EventHandler(this.ipAddLink_Click);
            // 
            // ipEditColumn
            // 
            this.ipEditColumn.AppearanceCell.Options.UseTextOptions = true;
            this.ipEditColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ipEditColumn.Caption = "编辑";
            this.ipEditColumn.ColumnEdit = this.ipEditLink;
            this.ipEditColumn.MinWidth = 25;
            this.ipEditColumn.Name = "ipEditColumn";
            this.ipEditColumn.Visible = true;
            this.ipEditColumn.VisibleIndex = 3;
            this.ipEditColumn.Width = 94;
            // 
            // ipEditLink
            // 
            this.ipEditLink.AutoHeight = false;
            this.ipEditLink.Name = "ipEditLink";
            this.ipEditLink.NullText = "编辑";
            this.ipEditLink.Click += new System.EventHandler(this.ipEditLink_Click);
            // 
            // ipDelColumn
            // 
            this.ipDelColumn.AppearanceCell.Options.UseTextOptions = true;
            this.ipDelColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ipDelColumn.Caption = "删除";
            this.ipDelColumn.ColumnEdit = this.ipDelLink;
            this.ipDelColumn.MinWidth = 25;
            this.ipDelColumn.Name = "ipDelColumn";
            this.ipDelColumn.Visible = true;
            this.ipDelColumn.VisibleIndex = 4;
            this.ipDelColumn.Width = 94;
            // 
            // ipDelLink
            // 
            this.ipDelLink.AutoHeight = false;
            this.ipDelLink.Name = "ipDelLink";
            this.ipDelLink.NullText = "删除";
            this.ipDelLink.Click += new System.EventHandler(this.ipDelLink_Click);
            // 
            // navBarPanel
            // 
            this.navBarPanel.Controls.Add(this.navBarControl1);
            this.navBarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarPanel.Location = new System.Drawing.Point(2, 64);
            this.navBarPanel.Name = "navBarPanel";
            this.navBarPanel.Size = new System.Drawing.Size(172, 290);
            this.navBarPanel.TabIndex = 1;
            this.navBarPanel.Text = "sidePanel2";
            // 
            // navBarControl1
            // 
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 171;
            this.navBarControl1.Size = new System.Drawing.Size(171, 290);
            this.navBarControl1.StoreDefaultPaintStyleName = true;
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // titlePanel
            // 
            this.titlePanel.Controls.Add(this.simpleButton4);
            this.titlePanel.Controls.Add(this.simpleButton3);
            this.titlePanel.Controls.Add(this.simpleButton2);
            this.titlePanel.Controls.Add(this.simpleButton1);
            this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titlePanel.Location = new System.Drawing.Point(2, 2);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(823, 62);
            this.titlePanel.TabIndex = 0;
            this.titlePanel.Text = "sidePanel1";
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(349, 10);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(94, 40);
            this.simpleButton4.TabIndex = 3;
            this.simpleButton4.Text = "SN绑定";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(232, 10);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(94, 40);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "IP地址";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(122, 10);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(94, 40);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "监测点";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(10, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(94, 40);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "监测线";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // monitorTableAdapter
            // 
            this.monitorTableAdapter.ClearBeforeFill = true;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "项目列表";
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // sensorTableAdapter
            // 
            this.sensorTableAdapter.ClearBeforeFill = true;
            // 
            // urlTableAdapter
            // 
            this.urlTableAdapter.ClearBeforeFill = true;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 356);
            this.Controls.Add(this.panelControl1);
            this.IsMdiContainer = true;
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.HomeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.midPanel.ResumeLayout(false);
            this.monitorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.onenetDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorDel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monitorCheck)).EndInit();
            this.sensorPanel.ResumeLayout(false);
            this.sidePanel7.ResumeLayout(false);
            this.sidePanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.sidePanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorAddLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorEditLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sensorDelLink)).EndInit();
            this.snPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.snAddLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.snEditLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.snDelLink)).EndInit();
            this.ipPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.urlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipAddLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipEditLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipDelLink)).EndInit();
            this.navBarPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.titlePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SidePanel midPanel;
        private DevExpress.XtraEditors.SidePanel navBarPanel;
        private DevExpress.XtraEditors.SidePanel titlePanel;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit monitorCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit monitorEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit monitorDel;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit monitorAdd;
        private onenetDataSet onenetDataSet;
        private System.Windows.Forms.BindingSource monitorBindingSource;
        private onenetDataSetTableAdapters.monitorTableAdapter monitorTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colid;
        private DevExpress.XtraGrid.Columns.GridColumn colname;
        private DevExpress.XtraGrid.Columns.GridColumn colprojectid;
        private DevExpress.XtraGrid.Columns.GridColumn addLinkHiper;
        private DevExpress.XtraGrid.Columns.GridColumn editLinkHiper;
        private DevExpress.XtraGrid.Columns.GridColumn delLinkHiper;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private DevExpress.XtraEditors.SidePanel monitorPanel;
        private DevExpress.XtraEditors.SidePanel sensorPanel;
        private DevExpress.XtraEditors.SidePanel sidePanel7;
        private DevExpress.XtraEditors.SidePanel sidePanel6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.BindingSource sensorBindingSource;
        private onenetDataSetTableAdapters.sensorTableAdapter sensorTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colid1;
        private DevExpress.XtraGrid.Columns.GridColumn colname1;
        private DevExpress.XtraGrid.Columns.GridColumn colmonitorid;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit sensorAddLink;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit sensorEditLink;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit sensorDelLink;
        private DevExpress.XtraEditors.SidePanel snPanel;
        private DevExpress.XtraEditors.SidePanel ipPanel;
        private DevExpress.XtraGrid.Columns.GridColumn addSensorLinkHiper;
        private DevExpress.XtraGrid.Columns.GridColumn editSensorLinkHiper;
        private DevExpress.XtraGrid.Columns.GridColumn delSensorLinkHiper;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.BindingSource urlBindingSource;
        private onenetDataSetTableAdapters.urlTableAdapter urlTableAdapter;
        private DevExpress.XtraGrid.Columns.GridColumn colid2;
        private DevExpress.XtraGrid.Columns.GridColumn colip;
        private DevExpress.XtraGrid.Columns.GridColumn colport;
        private DevExpress.XtraGrid.Columns.GridColumn colprojectid1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit ipAddLink;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit ipEditLink;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit ipDelLink;
        private DevExpress.XtraGrid.Columns.GridColumn ipAddColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ipEditColumn;
        private DevExpress.XtraGrid.Columns.GridColumn ipDelColumn;
        private DevExpress.XtraGrid.GridControl gridControl4;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit snAddLink;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit snEditLink;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit snDelLink;
        private DevExpress.XtraGrid.Columns.GridColumn snAddColumn;
        private DevExpress.XtraGrid.Columns.GridColumn snEditColumn;
        private DevExpress.XtraGrid.Columns.GridColumn snDelColumn;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraGrid.Columns.GridColumn colid3;
        private DevExpress.XtraGrid.Columns.GridColumn colsn;
        private DevExpress.XtraGrid.Columns.GridColumn colmonitorid1;
        private DevExpress.XtraGrid.Columns.GridColumn colmonitorname;
        private DevExpress.XtraGrid.Columns.GridColumn colsensorid;
        private DevExpress.XtraGrid.Columns.GridColumn colsensorname;
        private DevExpress.XtraGrid.Columns.GridColumn coltype;
    }
}