namespace ED_Systems
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.useLocalBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEmptySystemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteEmptySystemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.universalCartographicPricesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblLogFolder = new System.Windows.Forms.Label();
            this.lblCurrentCoordinates = new System.Windows.Forms.Label();
            this.lblCurrentSystem = new System.Windows.Forms.Label();
            this.pnlSystemInfo = new System.Windows.Forms.Panel();
            this.spcSystemInfo = new System.Windows.Forms.SplitContainer();
            this.tbxSystemFilter = new System.Windows.Forms.TextBox();
            this.lblSystems = new System.Windows.Forms.Label();
            this.dgvSystems = new System.Windows.Forms.DataGridView();
            this.sysSystemAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sysLastVisit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sysStarSystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsSystemsCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSystemsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.getEDSMDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setAsCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastVisitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sysStarPosX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sysStarPosY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sysStarPosZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sysDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sysHasImg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sysComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spcPlanetInfo = new System.Windows.Forms.SplitContainer();
            this.lblPlanets = new System.Windows.Forms.Label();
            this.dgvPlanets = new System.Windows.Forms.DataGridView();
            this.plSystemAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plPlanetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plPlanetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plPlanetClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plVolcanism = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plLandable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.plBiological = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plGeological = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plReserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plHasImg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvRAW = new System.Windows.Forms.DataGridView();
            this.rawSystemAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rawPlanetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rawName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rawNameLocalised = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rawPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRAW = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvRings = new System.Windows.Forms.DataGridView();
            this.rgSystemAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rgPlanetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rgRingName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rgClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblRings = new System.Windows.Forms.Label();
            this.lblSignsls = new System.Windows.Forms.Label();
            this.dgvSignals = new System.Windows.Forms.DataGridView();
            this.sigSystemAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigPlanetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigRing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigTypeLocalised = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sigHasImg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxFilter = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.prbUpload = new System.Windows.Forms.ProgressBar();
            this.grbFilter = new System.Windows.Forms.GroupBox();
            this.rbSignals = new System.Windows.Forms.RadioButton();
            this.rbRaw = new System.Windows.Forms.RadioButton();
            this.cmsPlanets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsSignals = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showImagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.addImageToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.showImagesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.pnlSystemInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcSystemInfo)).BeginInit();
            this.spcSystemInfo.Panel1.SuspendLayout();
            this.spcSystemInfo.Panel2.SuspendLayout();
            this.spcSystemInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSystems)).BeginInit();
            this.cmsSystemsCopy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcPlanetInfo)).BeginInit();
            this.spcPlanetInfo.Panel1.SuspendLayout();
            this.spcPlanetInfo.Panel2.SuspendLayout();
            this.spcPlanetInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRAW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignals)).BeginInit();
            this.grbFilter.SuspendLayout();
            this.cmsPlanets.SuspendLayout();
            this.cmsSignals.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.ControlDark;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useLocalBaseToolStripMenuItem,
            this.folderToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.resetSelectionToolStripMenuItem,
            this.showEmptySystemsToolStripMenuItem,
            this.deleteEmptySystemsToolStripMenuItem,
            this.universalCartographicPricesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(984, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "Menu";
            // 
            // useLocalBaseToolStripMenuItem
            // 
            this.useLocalBaseToolStripMenuItem.CheckOnClick = true;
            this.useLocalBaseToolStripMenuItem.Name = "useLocalBaseToolStripMenuItem";
            this.useLocalBaseToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.useLocalBaseToolStripMenuItem.Text = "Use local base";
            this.useLocalBaseToolStripMenuItem.Click += new System.EventHandler(this.useLocalBaseToolStripMenuItem_Click);
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.folderToolStripMenuItem.Text = "Select Log Folder";
            this.folderToolStripMenuItem.Click += new System.EventHandler(this.folderToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.loadToolStripMenuItem.Text = "Load Log File";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // resetSelectionToolStripMenuItem
            // 
            this.resetSelectionToolStripMenuItem.Name = "resetSelectionToolStripMenuItem";
            this.resetSelectionToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.resetSelectionToolStripMenuItem.Text = "Reset the filter";
            this.resetSelectionToolStripMenuItem.Click += new System.EventHandler(this.resetSelectionToolStripMenuItem_Click);
            // 
            // showEmptySystemsToolStripMenuItem
            // 
            this.showEmptySystemsToolStripMenuItem.Name = "showEmptySystemsToolStripMenuItem";
            this.showEmptySystemsToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.showEmptySystemsToolStripMenuItem.Text = "Show empty systems";
            this.showEmptySystemsToolStripMenuItem.Click += new System.EventHandler(this.showEmptySystemsToolStripMenuItem_Click);
            // 
            // deleteEmptySystemsToolStripMenuItem
            // 
            this.deleteEmptySystemsToolStripMenuItem.Name = "deleteEmptySystemsToolStripMenuItem";
            this.deleteEmptySystemsToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.deleteEmptySystemsToolStripMenuItem.Text = "Delete empty systems";
            this.deleteEmptySystemsToolStripMenuItem.Click += new System.EventHandler(this.deleteEmptySystemsToolStripMenuItem_Click);
            // 
            // universalCartographicPricesToolStripMenuItem
            // 
            this.universalCartographicPricesToolStripMenuItem.Name = "universalCartographicPricesToolStripMenuItem";
            this.universalCartographicPricesToolStripMenuItem.Size = new System.Drawing.Size(173, 20);
            this.universalCartographicPricesToolStripMenuItem.Text = "Universal Cartographic prices";
            this.universalCartographicPricesToolStripMenuItem.Click += new System.EventHandler(this.universalCartographicPricesToolStripMenuItem_Click);
            // 
            // lblLogFolder
            // 
            this.lblLogFolder.Location = new System.Drawing.Point(12, 24);
            this.lblLogFolder.Name = "lblLogFolder";
            this.lblLogFolder.Size = new System.Drawing.Size(648, 20);
            this.lblLogFolder.TabIndex = 1;
            this.lblLogFolder.Text = "label1";
            this.lblLogFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentCoordinates
            // 
            this.lblCurrentCoordinates.AutoSize = true;
            this.lblCurrentCoordinates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentCoordinates.Location = new System.Drawing.Point(12, 57);
            this.lblCurrentCoordinates.Name = "lblCurrentCoordinates";
            this.lblCurrentCoordinates.Size = new System.Drawing.Size(99, 16);
            this.lblCurrentCoordinates.TabIndex = 2;
            this.lblCurrentCoordinates.Text = "Current system:";
            // 
            // lblCurrentSystem
            // 
            this.lblCurrentSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentSystem.Location = new System.Drawing.Point(117, 57);
            this.lblCurrentSystem.Name = "lblCurrentSystem";
            this.lblCurrentSystem.Size = new System.Drawing.Size(251, 19);
            this.lblCurrentSystem.TabIndex = 6;
            this.lblCurrentSystem.Text = "label1";
            // 
            // pnlSystemInfo
            // 
            this.pnlSystemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSystemInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSystemInfo.Controls.Add(this.spcSystemInfo);
            this.pnlSystemInfo.Location = new System.Drawing.Point(12, 87);
            this.pnlSystemInfo.Name = "pnlSystemInfo";
            this.pnlSystemInfo.Size = new System.Drawing.Size(960, 462);
            this.pnlSystemInfo.TabIndex = 7;
            // 
            // spcSystemInfo
            // 
            this.spcSystemInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spcSystemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcSystemInfo.Location = new System.Drawing.Point(0, 0);
            this.spcSystemInfo.Name = "spcSystemInfo";
            // 
            // spcSystemInfo.Panel1
            // 
            this.spcSystemInfo.Panel1.Controls.Add(this.tbxSystemFilter);
            this.spcSystemInfo.Panel1.Controls.Add(this.lblSystems);
            this.spcSystemInfo.Panel1.Controls.Add(this.dgvSystems);
            // 
            // spcSystemInfo.Panel2
            // 
            this.spcSystemInfo.Panel2.Controls.Add(this.spcPlanetInfo);
            this.spcSystemInfo.Size = new System.Drawing.Size(956, 458);
            this.spcSystemInfo.SplitterDistance = 303;
            this.spcSystemInfo.SplitterWidth = 5;
            this.spcSystemInfo.TabIndex = 4;
            // 
            // tbxSystemFilter
            // 
            this.tbxSystemFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSystemFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbxSystemFilter.Location = new System.Drawing.Point(3, 25);
            this.tbxSystemFilter.Name = "tbxSystemFilter";
            this.tbxSystemFilter.Size = new System.Drawing.Size(293, 22);
            this.tbxSystemFilter.TabIndex = 2;
            this.tbxSystemFilter.TextChanged += new System.EventHandler(this.tbxSystemFilter_TextChanged);
            // 
            // lblSystems
            // 
            this.lblSystems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSystems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSystems.Location = new System.Drawing.Point(0, 0);
            this.lblSystems.Name = "lblSystems";
            this.lblSystems.Size = new System.Drawing.Size(299, 22);
            this.lblSystems.TabIndex = 1;
            this.lblSystems.Text = "Systems";
            this.lblSystems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSystems
            // 
            this.dgvSystems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSystems.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvSystems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSystems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sysSystemAddress,
            this.sysLastVisit,
            this.sysStarSystem,
            this.sysStarPosX,
            this.sysStarPosY,
            this.sysStarPosZ,
            this.sysDistance,
            this.sysHasImg,
            this.sysComment});
            this.dgvSystems.Location = new System.Drawing.Point(0, 53);
            this.dgvSystems.Name = "dgvSystems";
            this.dgvSystems.Size = new System.Drawing.Size(296, 405);
            this.dgvSystems.TabIndex = 1;
            this.dgvSystems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSystems_CellEndEdit);
            this.dgvSystems.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSystems_CellEnter);
            // 
            // sysSystemAddress
            // 
            this.sysSystemAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sysSystemAddress.DataPropertyName = "SysAddr";
            this.sysSystemAddress.HeaderText = "SystemAddress";
            this.sysSystemAddress.Name = "sysSystemAddress";
            this.sysSystemAddress.ReadOnly = true;
            this.sysSystemAddress.Visible = false;
            // 
            // sysLastVisit
            // 
            this.sysLastVisit.DataPropertyName = "LastVisit";
            this.sysLastVisit.HeaderText = "Last visit";
            this.sysLastVisit.Name = "sysLastVisit";
            this.sysLastVisit.ReadOnly = true;
            this.sysLastVisit.Visible = false;
            // 
            // sysStarSystem
            // 
            this.sysStarSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sysStarSystem.ContextMenuStrip = this.cmsSystemsCopy;
            this.sysStarSystem.DataPropertyName = "SystemName";
            this.sysStarSystem.HeaderText = "Star system";
            this.sysStarSystem.Name = "sysStarSystem";
            this.sysStarSystem.ReadOnly = true;
            this.sysStarSystem.Width = 86;
            // 
            // cmsSystemsCopy
            // 
            this.cmsSystemsCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystemsCopy,
            this.getEDSMDataToolStripMenuItem,
            this.setAsCurrentToolStripMenuItem,
            this.lastVisitToolStripMenuItem,
            this.addImageToolStripMenuItem,
            this.showImagesToolStripMenuItem});
            this.cmsSystemsCopy.Name = "cmsSystemsCopy";
            this.cmsSystemsCopy.Size = new System.Drawing.Size(170, 136);
            // 
            // tsmiSystemsCopy
            // 
            this.tsmiSystemsCopy.Name = "tsmiSystemsCopy";
            this.tsmiSystemsCopy.Size = new System.Drawing.Size(169, 22);
            this.tsmiSystemsCopy.Text = "Copy to clipboard";
            this.tsmiSystemsCopy.Click += new System.EventHandler(this.tsmiSystemsCopy_Click);
            // 
            // getEDSMDataToolStripMenuItem
            // 
            this.getEDSMDataToolStripMenuItem.Name = "getEDSMDataToolStripMenuItem";
            this.getEDSMDataToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.getEDSMDataToolStripMenuItem.Text = "Get EDSM data";
            this.getEDSMDataToolStripMenuItem.Click += new System.EventHandler(this.getEDSMDataToolStripMenuItem_Click);
            // 
            // setAsCurrentToolStripMenuItem
            // 
            this.setAsCurrentToolStripMenuItem.Name = "setAsCurrentToolStripMenuItem";
            this.setAsCurrentToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.setAsCurrentToolStripMenuItem.Text = "Set as current";
            this.setAsCurrentToolStripMenuItem.Click += new System.EventHandler(this.setAsCurrentToolStripMenuItem_Click);
            // 
            // lastVisitToolStripMenuItem
            // 
            this.lastVisitToolStripMenuItem.CheckOnClick = true;
            this.lastVisitToolStripMenuItem.Name = "lastVisitToolStripMenuItem";
            this.lastVisitToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.lastVisitToolStripMenuItem.Text = "Last visit";
            this.lastVisitToolStripMenuItem.CheckedChanged += new System.EventHandler(this.lastVisitToolStripMenuItem_CheckedChanged);
            // 
            // addImageToolStripMenuItem
            // 
            this.addImageToolStripMenuItem.Name = "addImageToolStripMenuItem";
            this.addImageToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.addImageToolStripMenuItem.Text = "Add image";
            this.addImageToolStripMenuItem.Click += new System.EventHandler(this.addImageToolStripMenuItem_Click);
            // 
            // showImagesToolStripMenuItem
            // 
            this.showImagesToolStripMenuItem.Name = "showImagesToolStripMenuItem";
            this.showImagesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.showImagesToolStripMenuItem.Text = "Show images";
            this.showImagesToolStripMenuItem.Click += new System.EventHandler(this.showImagesToolStripMenuItem_Click);
            // 
            // sysStarPosX
            // 
            this.sysStarPosX.DataPropertyName = "StarPosX";
            this.sysStarPosX.HeaderText = "StarPosX";
            this.sysStarPosX.Name = "sysStarPosX";
            this.sysStarPosX.ReadOnly = true;
            this.sysStarPosX.Visible = false;
            // 
            // sysStarPosY
            // 
            this.sysStarPosY.DataPropertyName = "StarPosY";
            this.sysStarPosY.HeaderText = "StarPosY";
            this.sysStarPosY.Name = "sysStarPosY";
            this.sysStarPosY.ReadOnly = true;
            this.sysStarPosY.Visible = false;
            // 
            // sysStarPosZ
            // 
            this.sysStarPosZ.DataPropertyName = "StarPosZ";
            this.sysStarPosZ.HeaderText = "StarPosZ";
            this.sysStarPosZ.Name = "sysStarPosZ";
            this.sysStarPosZ.ReadOnly = true;
            this.sysStarPosZ.Visible = false;
            // 
            // sysDistance
            // 
            this.sysDistance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.sysDistance.DataPropertyName = "Distance";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.sysDistance.DefaultCellStyle = dataGridViewCellStyle1;
            this.sysDistance.HeaderText = "Distance (ly)";
            this.sysDistance.Name = "sysDistance";
            this.sysDistance.ReadOnly = true;
            this.sysDistance.Width = 90;
            // 
            // sysHasImg
            // 
            this.sysHasImg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.sysHasImg.DataPropertyName = "HasImg";
            this.sysHasImg.HeaderText = "Img";
            this.sysHasImg.Name = "sysHasImg";
            this.sysHasImg.ReadOnly = true;
            this.sysHasImg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sysHasImg.Width = 49;
            // 
            // sysComment
            // 
            this.sysComment.DataPropertyName = "Comment";
            this.sysComment.HeaderText = "Comment";
            this.sysComment.Name = "sysComment";
            // 
            // spcPlanetInfo
            // 
            this.spcPlanetInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spcPlanetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcPlanetInfo.Location = new System.Drawing.Point(0, 0);
            this.spcPlanetInfo.Name = "spcPlanetInfo";
            this.spcPlanetInfo.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcPlanetInfo.Panel1
            // 
            this.spcPlanetInfo.Panel1.Controls.Add(this.lblPlanets);
            this.spcPlanetInfo.Panel1.Controls.Add(this.dgvPlanets);
            // 
            // spcPlanetInfo.Panel2
            // 
            this.spcPlanetInfo.Panel2.Controls.Add(this.splitContainer1);
            this.spcPlanetInfo.Size = new System.Drawing.Size(648, 458);
            this.spcPlanetInfo.SplitterDistance = 222;
            this.spcPlanetInfo.SplitterWidth = 5;
            this.spcPlanetInfo.TabIndex = 0;
            // 
            // lblPlanets
            // 
            this.lblPlanets.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlanets.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPlanets.Location = new System.Drawing.Point(0, 0);
            this.lblPlanets.Name = "lblPlanets";
            this.lblPlanets.Size = new System.Drawing.Size(644, 22);
            this.lblPlanets.TabIndex = 3;
            this.lblPlanets.Text = "Planets";
            this.lblPlanets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPlanets
            // 
            this.dgvPlanets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPlanets.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvPlanets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlanets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.plSystemAddress,
            this.plPlanetID,
            this.plPlanetName,
            this.plDistance,
            this.plPlanetClass,
            this.plVolcanism,
            this.plLandable,
            this.plBiological,
            this.plGeological,
            this.plReserve,
            this.plHasImg});
            this.dgvPlanets.Location = new System.Drawing.Point(0, 25);
            this.dgvPlanets.Name = "dgvPlanets";
            this.dgvPlanets.Size = new System.Drawing.Size(646, 194);
            this.dgvPlanets.TabIndex = 2;
            this.dgvPlanets.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlanets_CellEnter);
            // 
            // plSystemAddress
            // 
            this.plSystemAddress.DataPropertyName = "SysAddr";
            this.plSystemAddress.HeaderText = "SystemAddress";
            this.plSystemAddress.Name = "plSystemAddress";
            this.plSystemAddress.ReadOnly = true;
            this.plSystemAddress.Visible = false;
            // 
            // plPlanetID
            // 
            this.plPlanetID.DataPropertyName = "PlanetID";
            this.plPlanetID.HeaderText = "PlanetID";
            this.plPlanetID.Name = "plPlanetID";
            this.plPlanetID.ReadOnly = true;
            this.plPlanetID.Visible = false;
            // 
            // plPlanetName
            // 
            this.plPlanetName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.plPlanetName.DataPropertyName = "PlanetName";
            this.plPlanetName.HeaderText = "Planet";
            this.plPlanetName.Name = "plPlanetName";
            this.plPlanetName.ReadOnly = true;
            this.plPlanetName.Width = 62;
            // 
            // plDistance
            // 
            this.plDistance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.plDistance.DataPropertyName = "Distance";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.plDistance.DefaultCellStyle = dataGridViewCellStyle2;
            this.plDistance.HeaderText = "Distance (ls)";
            this.plDistance.Name = "plDistance";
            this.plDistance.ReadOnly = true;
            this.plDistance.Width = 90;
            // 
            // plPlanetClass
            // 
            this.plPlanetClass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.plPlanetClass.DataPropertyName = "Class";
            this.plPlanetClass.HeaderText = "Planet class";
            this.plPlanetClass.Name = "plPlanetClass";
            this.plPlanetClass.ReadOnly = true;
            this.plPlanetClass.Width = 89;
            // 
            // plVolcanism
            // 
            this.plVolcanism.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.plVolcanism.DataPropertyName = "Volcanism";
            this.plVolcanism.HeaderText = "Volcanism";
            this.plVolcanism.Name = "plVolcanism";
            this.plVolcanism.ReadOnly = true;
            this.plVolcanism.Width = 80;
            // 
            // plLandable
            // 
            this.plLandable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.plLandable.DataPropertyName = "Landable";
            this.plLandable.FalseValue = "0";
            this.plLandable.HeaderText = "Landable";
            this.plLandable.Name = "plLandable";
            this.plLandable.ReadOnly = true;
            this.plLandable.TrueValue = "1";
            this.plLandable.Width = 57;
            // 
            // plBiological
            // 
            this.plBiological.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.plBiological.DataPropertyName = "Biological";
            this.plBiological.HeaderText = "Biological";
            this.plBiological.Name = "plBiological";
            this.plBiological.ReadOnly = true;
            this.plBiological.Width = 77;
            // 
            // plGeological
            // 
            this.plGeological.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.plGeological.DataPropertyName = "Geological";
            this.plGeological.HeaderText = "Geological";
            this.plGeological.Name = "plGeological";
            this.plGeological.ReadOnly = true;
            this.plGeological.Width = 82;
            // 
            // plReserve
            // 
            this.plReserve.DataPropertyName = "Reserve";
            this.plReserve.HeaderText = "Reserve";
            this.plReserve.Name = "plReserve";
            this.plReserve.ReadOnly = true;
            // 
            // plHasImg
            // 
            this.plHasImg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.plHasImg.DataPropertyName = "HasImg";
            this.plHasImg.HeaderText = "Img";
            this.plHasImg.Name = "plHasImg";
            this.plHasImg.ReadOnly = true;
            this.plHasImg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.plHasImg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.plHasImg.Width = 30;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvRAW);
            this.splitContainer1.Panel1.Controls.Add(this.lblRAW);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(648, 231);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvRAW
            // 
            this.dgvRAW.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRAW.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRAW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRAW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rawSystemAddress,
            this.rawPlanetID,
            this.rawName,
            this.rawNameLocalised,
            this.rawPercent});
            this.dgvRAW.Location = new System.Drawing.Point(0, 21);
            this.dgvRAW.Name = "dgvRAW";
            this.dgvRAW.Size = new System.Drawing.Size(211, 206);
            this.dgvRAW.TabIndex = 1;
            // 
            // rawSystemAddress
            // 
            this.rawSystemAddress.DataPropertyName = "SysAddr";
            this.rawSystemAddress.HeaderText = "SystemAddress";
            this.rawSystemAddress.Name = "rawSystemAddress";
            this.rawSystemAddress.ReadOnly = true;
            this.rawSystemAddress.Visible = false;
            // 
            // rawPlanetID
            // 
            this.rawPlanetID.DataPropertyName = "PlanetID";
            this.rawPlanetID.HeaderText = "PlanetID";
            this.rawPlanetID.Name = "rawPlanetID";
            this.rawPlanetID.ReadOnly = true;
            this.rawPlanetID.Visible = false;
            // 
            // rawName
            // 
            this.rawName.DataPropertyName = "Name";
            this.rawName.HeaderText = "Name (en)";
            this.rawName.Name = "rawName";
            this.rawName.ReadOnly = true;
            // 
            // rawNameLocalised
            // 
            this.rawNameLocalised.DataPropertyName = "NameLocalised";
            this.rawNameLocalised.HeaderText = "Name (loc)";
            this.rawNameLocalised.Name = "rawNameLocalised";
            this.rawNameLocalised.ReadOnly = true;
            // 
            // rawPercent
            // 
            this.rawPercent.DataPropertyName = "Percent";
            dataGridViewCellStyle3.Format = "N1";
            dataGridViewCellStyle3.NullValue = null;
            this.rawPercent.DefaultCellStyle = dataGridViewCellStyle3;
            this.rawPercent.HeaderText = "Percent";
            this.rawPercent.Name = "rawPercent";
            this.rawPercent.ReadOnly = true;
            // 
            // lblRAW
            // 
            this.lblRAW.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRAW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRAW.Location = new System.Drawing.Point(0, 0);
            this.lblRAW.Name = "lblRAW";
            this.lblRAW.Size = new System.Drawing.Size(211, 18);
            this.lblRAW.TabIndex = 0;
            this.lblRAW.Text = "RAW";
            this.lblRAW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvRings);
            this.splitContainer2.Panel1.Controls.Add(this.lblRings);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblSignsls);
            this.splitContainer2.Panel2.Controls.Add(this.dgvSignals);
            this.splitContainer2.Size = new System.Drawing.Size(429, 231);
            this.splitContainer2.SplitterDistance = 205;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvRings
            // 
            this.dgvRings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRings.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rgSystemAddress,
            this.rgPlanetID,
            this.rgRingName,
            this.rgClass});
            this.dgvRings.Location = new System.Drawing.Point(0, 21);
            this.dgvRings.Name = "dgvRings";
            this.dgvRings.Size = new System.Drawing.Size(200, 206);
            this.dgvRings.TabIndex = 2;
            this.dgvRings.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRings_CellEnter);
            // 
            // rgSystemAddress
            // 
            this.rgSystemAddress.DataPropertyName = "SysAddr";
            this.rgSystemAddress.HeaderText = "SystemAddress";
            this.rgSystemAddress.Name = "rgSystemAddress";
            this.rgSystemAddress.ReadOnly = true;
            this.rgSystemAddress.Visible = false;
            // 
            // rgPlanetID
            // 
            this.rgPlanetID.DataPropertyName = "PlanetID";
            this.rgPlanetID.HeaderText = "PlanetID";
            this.rgPlanetID.Name = "rgPlanetID";
            this.rgPlanetID.ReadOnly = true;
            this.rgPlanetID.Visible = false;
            // 
            // rgRingName
            // 
            this.rgRingName.DataPropertyName = "RingName";
            this.rgRingName.HeaderText = "Ring";
            this.rgRingName.Name = "rgRingName";
            this.rgRingName.ReadOnly = true;
            // 
            // rgClass
            // 
            this.rgClass.DataPropertyName = "Class";
            this.rgClass.HeaderText = "Class";
            this.rgClass.Name = "rgClass";
            this.rgClass.ReadOnly = true;
            // 
            // lblRings
            // 
            this.lblRings.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRings.Location = new System.Drawing.Point(0, 0);
            this.lblRings.Name = "lblRings";
            this.lblRings.Size = new System.Drawing.Size(201, 18);
            this.lblRings.TabIndex = 1;
            this.lblRings.Text = "Rings";
            this.lblRings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSignsls
            // 
            this.lblSignsls.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSignsls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSignsls.Location = new System.Drawing.Point(0, 0);
            this.lblSignsls.Name = "lblSignsls";
            this.lblSignsls.Size = new System.Drawing.Size(216, 18);
            this.lblSignsls.TabIndex = 2;
            this.lblSignsls.Text = "Mining points";
            this.lblSignsls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvSignals
            // 
            this.dgvSignals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSignals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvSignals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSignals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sigSystemAddress,
            this.sigPlanetID,
            this.sigRing,
            this.sigType,
            this.sigTypeLocalised,
            this.sigCount,
            this.sigComment,
            this.sigHasImg});
            this.dgvSignals.Location = new System.Drawing.Point(0, 21);
            this.dgvSignals.Name = "dgvSignals";
            this.dgvSignals.Size = new System.Drawing.Size(212, 206);
            this.dgvSignals.TabIndex = 3;
            this.dgvSignals.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSignals_CellEndEdit);
            // 
            // sigSystemAddress
            // 
            this.sigSystemAddress.DataPropertyName = "SysAddr";
            this.sigSystemAddress.HeaderText = "SystemAddress";
            this.sigSystemAddress.Name = "sigSystemAddress";
            this.sigSystemAddress.ReadOnly = true;
            this.sigSystemAddress.Visible = false;
            // 
            // sigPlanetID
            // 
            this.sigPlanetID.DataPropertyName = "PlanetID";
            this.sigPlanetID.HeaderText = "PlanetID";
            this.sigPlanetID.Name = "sigPlanetID";
            this.sigPlanetID.ReadOnly = true;
            this.sigPlanetID.Visible = false;
            // 
            // sigRing
            // 
            this.sigRing.DataPropertyName = "RingName";
            this.sigRing.HeaderText = "Ring";
            this.sigRing.Name = "sigRing";
            this.sigRing.ReadOnly = true;
            this.sigRing.Visible = false;
            // 
            // sigType
            // 
            this.sigType.DataPropertyName = "Type";
            this.sigType.HeaderText = "Type (en)";
            this.sigType.Name = "sigType";
            this.sigType.ReadOnly = true;
            // 
            // sigTypeLocalised
            // 
            this.sigTypeLocalised.DataPropertyName = "TypeLocalised";
            this.sigTypeLocalised.HeaderText = "Type (loc)";
            this.sigTypeLocalised.Name = "sigTypeLocalised";
            this.sigTypeLocalised.ReadOnly = true;
            // 
            // sigCount
            // 
            this.sigCount.DataPropertyName = "Count";
            this.sigCount.HeaderText = "Count";
            this.sigCount.Name = "sigCount";
            this.sigCount.ReadOnly = true;
            // 
            // sigComment
            // 
            this.sigComment.DataPropertyName = "Comment";
            this.sigComment.HeaderText = "Comment";
            this.sigComment.Name = "sigComment";
            // 
            // sigHasImg
            // 
            this.sigHasImg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.sigHasImg.DataPropertyName = "HasImg";
            this.sigHasImg.HeaderText = "Img";
            this.sigHasImg.Name = "sigHasImg";
            this.sigHasImg.ReadOnly = true;
            this.sigHasImg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sigHasImg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sigHasImg.Width = 30;
            // 
            // cbxFilter
            // 
            this.cbxFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbxFilter.FormattingEnabled = true;
            this.cbxFilter.Location = new System.Drawing.Point(612, 54);
            this.cbxFilter.Name = "cbxFilter";
            this.cbxFilter.Size = new System.Drawing.Size(294, 24);
            this.cbxFilter.Sorted = true;
            this.cbxFilter.TabIndex = 8;
            this.cbxFilter.SelectedIndexChanged += new System.EventHandler(this.cbxFilter_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFilter.Location = new System.Drawing.Point(374, 57);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(40, 16);
            this.lblFilter.TabIndex = 9;
            this.lblFilter.Text = "Filter:";
            // 
            // prbUpload
            // 
            this.prbUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbUpload.Location = new System.Drawing.Point(666, 27);
            this.prbUpload.Name = "prbUpload";
            this.prbUpload.Size = new System.Drawing.Size(306, 17);
            this.prbUpload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbUpload.TabIndex = 12;
            this.prbUpload.Visible = false;
            // 
            // grbFilter
            // 
            this.grbFilter.Controls.Add(this.rbSignals);
            this.grbFilter.Controls.Add(this.rbRaw);
            this.grbFilter.Location = new System.Drawing.Point(420, 46);
            this.grbFilter.Name = "grbFilter";
            this.grbFilter.Size = new System.Drawing.Size(186, 35);
            this.grbFilter.TabIndex = 13;
            this.grbFilter.TabStop = false;
            // 
            // rbSignals
            // 
            this.rbSignals.AutoSize = true;
            this.rbSignals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbSignals.Location = new System.Drawing.Point(70, 9);
            this.rbSignals.Name = "rbSignals";
            this.rbSignals.Size = new System.Drawing.Size(104, 20);
            this.rbSignals.TabIndex = 1;
            this.rbSignals.TabStop = true;
            this.rbSignals.Text = "Mining points";
            this.rbSignals.UseVisualStyleBackColor = true;
            this.rbSignals.CheckedChanged += new System.EventHandler(this.rbSignals_CheckedChanged);
            // 
            // rbRaw
            // 
            this.rbRaw.AutoSize = true;
            this.rbRaw.Checked = true;
            this.rbRaw.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rbRaw.Location = new System.Drawing.Point(6, 9);
            this.rbRaw.Name = "rbRaw";
            this.rbRaw.Size = new System.Drawing.Size(58, 20);
            this.rbRaw.TabIndex = 0;
            this.rbRaw.TabStop = true;
            this.rbRaw.Text = "RAW";
            this.rbRaw.UseVisualStyleBackColor = true;
            this.rbRaw.CheckedChanged += new System.EventHandler(this.rbRaw_CheckedChanged);
            // 
            // cmsPlanets
            // 
            this.cmsPlanets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageToolStripMenuItem1,
            this.showImagesToolStripMenuItem1});
            this.cmsPlanets.Name = "cmsPlanets";
            this.cmsPlanets.Size = new System.Drawing.Size(145, 48);
            // 
            // cmsSignals
            // 
            this.cmsSignals.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addImageToolStripMenuItem2,
            this.showImagesToolStripMenuItem2});
            this.cmsSignals.Name = "cmsSignals";
            this.cmsSignals.Size = new System.Drawing.Size(145, 48);
            // 
            // addImageToolStripMenuItem1
            // 
            this.addImageToolStripMenuItem1.Name = "addImageToolStripMenuItem1";
            this.addImageToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.addImageToolStripMenuItem1.Text = "Add image";
            // 
            // showImagesToolStripMenuItem1
            // 
            this.showImagesToolStripMenuItem1.Name = "showImagesToolStripMenuItem1";
            this.showImagesToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.showImagesToolStripMenuItem1.Text = "Show images";
            // 
            // addImageToolStripMenuItem2
            // 
            this.addImageToolStripMenuItem2.Name = "addImageToolStripMenuItem2";
            this.addImageToolStripMenuItem2.Size = new System.Drawing.Size(144, 22);
            this.addImageToolStripMenuItem2.Text = "Add image";
            // 
            // showImagesToolStripMenuItem2
            // 
            this.showImagesToolStripMenuItem2.Name = "showImagesToolStripMenuItem2";
            this.showImagesToolStripMenuItem2.Size = new System.Drawing.Size(144, 22);
            this.showImagesToolStripMenuItem2.Text = "Show images";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.grbFilter);
            this.Controls.Add(this.prbUpload);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.cbxFilter);
            this.Controls.Add(this.pnlSystemInfo);
            this.Controls.Add(this.lblCurrentSystem);
            this.Controls.Add(this.lblCurrentCoordinates);
            this.Controls.Add(this.lblLogFolder);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "ED Systems";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlSystemInfo.ResumeLayout(false);
            this.spcSystemInfo.Panel1.ResumeLayout(false);
            this.spcSystemInfo.Panel1.PerformLayout();
            this.spcSystemInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcSystemInfo)).EndInit();
            this.spcSystemInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSystems)).EndInit();
            this.cmsSystemsCopy.ResumeLayout(false);
            this.spcPlanetInfo.Panel1.ResumeLayout(false);
            this.spcPlanetInfo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcPlanetInfo)).EndInit();
            this.spcPlanetInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanets)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRAW)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSignals)).EndInit();
            this.grbFilter.ResumeLayout(false);
            this.grbFilter.PerformLayout();
            this.cmsPlanets.ResumeLayout(false);
            this.cmsSignals.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.Label lblLogFolder;
        private System.Windows.Forms.Label lblCurrentCoordinates;
        private System.Windows.Forms.Label lblCurrentSystem;
        private System.Windows.Forms.Panel pnlSystemInfo;
        private System.Windows.Forms.DataGridView dgvSystems;
        private System.Windows.Forms.Label lblPlanets;
        private System.Windows.Forms.DataGridView dgvPlanets;
        private System.Windows.Forms.SplitContainer spcSystemInfo;
        private System.Windows.Forms.Label lblSystems;
        private System.Windows.Forms.SplitContainer spcPlanetInfo;
        private System.Windows.Forms.Label lblRAW;
        private System.Windows.Forms.Label lblRings;
        private System.Windows.Forms.Label lblSignsls;
        private System.Windows.Forms.DataGridView dgvRAW;
        private System.Windows.Forms.DataGridView dgvRings;
        private System.Windows.Forms.DataGridView dgvSignals;
        private System.Windows.Forms.ComboBox cbxFilter;
        private System.Windows.Forms.TextBox tbxSystemFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ProgressBar prbUpload;
        private System.Windows.Forms.GroupBox grbFilter;
        private System.Windows.Forms.RadioButton rbSignals;
        private System.Windows.Forms.RadioButton rbRaw;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ContextMenuStrip cmsSystemsCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemsCopy;
        private System.Windows.Forms.ToolStripMenuItem resetSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEmptySystemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteEmptySystemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getEDSMDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lastVisitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setAsCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem universalCartographicPricesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useLocalBaseToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn rawSystemAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn rawPlanetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn rawName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rawNameLocalised;
        private System.Windows.Forms.DataGridViewTextBoxColumn rawPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn rgSystemAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn rgPlanetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn rgRingName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rgClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigSystemAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigPlanetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigRing;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigType;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigTypeLocalised;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn sigHasImg;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysSystemAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysLastVisit;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysStarSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysStarPosX;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysStarPosY;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysStarPosZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysHasImg;
        private System.Windows.Forms.DataGridViewTextBoxColumn sysComment;
        private System.Windows.Forms.DataGridViewTextBoxColumn plSystemAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn plPlanetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn plPlanetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn plDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn plPlanetClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn plVolcanism;
        private System.Windows.Forms.DataGridViewCheckBoxColumn plLandable;
        private System.Windows.Forms.DataGridViewTextBoxColumn plBiological;
        private System.Windows.Forms.DataGridViewTextBoxColumn plGeological;
        private System.Windows.Forms.DataGridViewTextBoxColumn plReserve;
        private System.Windows.Forms.DataGridViewTextBoxColumn plHasImg;
        private System.Windows.Forms.ContextMenuStrip cmsPlanets;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showImagesToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip cmsSignals;
        private System.Windows.Forms.ToolStripMenuItem addImageToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem showImagesToolStripMenuItem2;
    }
}

