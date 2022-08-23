namespace AmiralBattiV2
{
    partial class HomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rTBx = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxRoomName = new System.Windows.Forms.TextBox();
            this.CreateRoomBtn = new System.Windows.Forms.Button();
            this.RoomsListBtn = new System.Windows.Forms.Button();
            this.dgwRoomList = new System.Windows.Forms.DataGridView();
            this.ClientWorker = new System.ComponentModel.BackgroundWorker();
            this.ServerWorker = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GameScreenWorker = new System.ComponentModel.BackgroundWorker();
            this.lblHour = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRoomList)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1647, 696);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackgroundImage = global::AmiralBattiV2.Properties.Resources.world_of_warships;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.Controls.Add(this.rTBx);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1639, 667);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Hoşgeldiniz!";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rTBx
            // 
            this.rTBx.BackColor = System.Drawing.Color.Black;
            this.rTBx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTBx.ForeColor = System.Drawing.SystemColors.Window;
            this.rTBx.Location = new System.Drawing.Point(53, 65);
            this.rTBx.Name = "rTBx";
            this.rTBx.Size = new System.Drawing.Size(801, 257);
            this.rTBx.TabIndex = 2;
            this.rTBx.Text = resources.GetString("rTBx.Text");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(47, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "OYUN NASIL OYNANIR?";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.BackgroundImage = global::AmiralBattiV2.Properties.Resources.world_of_warships;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.lblHour);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.tbxRoomName);
            this.tabPage2.Controls.Add(this.CreateRoomBtn);
            this.tabPage2.Controls.Add(this.RoomsListBtn);
            this.tabPage2.Controls.Add(this.dgwRoomList);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1639, 667);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Oda listele/oluştur";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(86, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Oda ismi:";
            // 
            // tbxRoomName
            // 
            this.tbxRoomName.Location = new System.Drawing.Point(187, 547);
            this.tbxRoomName.Name = "tbxRoomName";
            this.tbxRoomName.Size = new System.Drawing.Size(100, 22);
            this.tbxRoomName.TabIndex = 3;
            // 
            // CreateRoomBtn
            // 
            this.CreateRoomBtn.Location = new System.Drawing.Point(303, 530);
            this.CreateRoomBtn.Name = "CreateRoomBtn";
            this.CreateRoomBtn.Size = new System.Drawing.Size(129, 56);
            this.CreateRoomBtn.TabIndex = 2;
            this.CreateRoomBtn.Text = "Oda Oluştur";
            this.CreateRoomBtn.UseVisualStyleBackColor = true;
            this.CreateRoomBtn.Click += new System.EventHandler(this.CreateRoomBtn_Click);
            // 
            // RoomsListBtn
            // 
            this.RoomsListBtn.Location = new System.Drawing.Point(87, 75);
            this.RoomsListBtn.Name = "RoomsListBtn";
            this.RoomsListBtn.Size = new System.Drawing.Size(129, 56);
            this.RoomsListBtn.TabIndex = 1;
            this.RoomsListBtn.Text = "Odaları Listele";
            this.RoomsListBtn.UseVisualStyleBackColor = true;
            this.RoomsListBtn.Click += new System.EventHandler(this.RoomsListBtn_Click);
            // 
            // dgwRoomList
            // 
            this.dgwRoomList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgwRoomList.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgwRoomList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgwRoomList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwRoomList.Location = new System.Drawing.Point(87, 137);
            this.dgwRoomList.Name = "dgwRoomList";
            this.dgwRoomList.RowHeadersWidth = 51;
            this.dgwRoomList.RowTemplate.Height = 24;
            this.dgwRoomList.Size = new System.Drawing.Size(504, 378);
            this.dgwRoomList.TabIndex = 0;
            this.dgwRoomList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwRoomList_CellDoubleClick);
            // 
            // ClientWorker
            // 
            this.ClientWorker.WorkerReportsProgress = true;
            this.ClientWorker.WorkerSupportsCancellation = true;
            this.ClientWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ClientWorker_DoWork);
            this.ClientWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ClientWorker_ProgressChanged);
            this.ClientWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ClientWorker_RunWorkerCompleted);
            // 
            // ServerWorker
            // 
            this.ServerWorker.WorkerReportsProgress = true;
            this.ServerWorker.WorkerSupportsCancellation = true;
            this.ServerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ServerWorker_DoWork);
            this.ServerWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ServerWorker_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GameScreenWorker
            // 
            this.GameScreenWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GameScreenWorker_DoWork);
            this.GameScreenWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GameScreenWorker_RunWorkerCompleted);
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHour.Location = new System.Drawing.Point(82, 19);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(75, 25);
            this.lblHour.TabIndex = 6;
            this.lblHour.Text = "---------";
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1405, 670);
            this.Controls.Add(this.tabControl1);
            this.Name = "HomeScreen";
            this.Text = "Fleet Battle";
            this.Load += new System.EventHandler(this.HomeScreen_Load);
            this.SizeChanged += new System.EventHandler(this.HomeScreen_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwRoomList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rTBx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxRoomName;
        private System.Windows.Forms.Button CreateRoomBtn;
        private System.Windows.Forms.Button RoomsListBtn;
        private System.Windows.Forms.DataGridView dgwRoomList;
        private System.ComponentModel.BackgroundWorker ClientWorker;
        private System.ComponentModel.BackgroundWorker ServerWorker;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker GameScreenWorker;
        private System.Windows.Forms.Label lblHour;
    }
}