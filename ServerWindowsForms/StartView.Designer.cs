namespace ServerWindowsForms
{
    partial class StartView
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
            this.bt_Start = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.FileOpener = new System.Windows.Forms.OpenFileDialog();
            this.bt_MatrixA = new System.Windows.Forms.Button();
            this.lb_MatrixA = new System.Windows.Forms.Label();
            this.lb_MatrixB = new System.Windows.Forms.Label();
            this.bt_MatrixB = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.bt_StartLocal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_Start
            // 
            this.bt_Start.Location = new System.Drawing.Point(205, 181);
            this.bt_Start.Name = "bt_Start";
            this.bt_Start.Size = new System.Drawing.Size(94, 29);
            this.bt_Start.TabIndex = 0;
            this.bt_Start.Text = "Start";
            this.bt_Start.UseVisualStyleBackColor = true;
            this.bt_Start.Click += new System.EventHandler(this.bt_Start_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "IP:";
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(185, 129);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(125, 27);
            this.tb_Port.TabIndex = 10;
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(185, 53);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(125, 27);
            this.tb_IP.TabIndex = 9;
            // 
            // FileOpener
            // 
            this.FileOpener.FileName = "openFileDialog1";
            // 
            // bt_MatrixA
            // 
            this.bt_MatrixA.Location = new System.Drawing.Point(12, 53);
            this.bt_MatrixA.Name = "bt_MatrixA";
            this.bt_MatrixA.Size = new System.Drawing.Size(94, 29);
            this.bt_MatrixA.TabIndex = 13;
            this.bt_MatrixA.Text = "Open";
            this.bt_MatrixA.UseVisualStyleBackColor = true;
            this.bt_MatrixA.Click += new System.EventHandler(this.bt_MatrixA_Click);
            // 
            // lb_MatrixA
            // 
            this.lb_MatrixA.AutoSize = true;
            this.lb_MatrixA.Location = new System.Drawing.Point(12, 18);
            this.lb_MatrixA.Name = "lb_MatrixA";
            this.lb_MatrixA.Size = new System.Drawing.Size(95, 20);
            this.lb_MatrixA.TabIndex = 14;
            this.lb_MatrixA.Text = "Matrix A File:";
            // 
            // lb_MatrixB
            // 
            this.lb_MatrixB.AutoSize = true;
            this.lb_MatrixB.Location = new System.Drawing.Point(13, 108);
            this.lb_MatrixB.Name = "lb_MatrixB";
            this.lb_MatrixB.Size = new System.Drawing.Size(94, 20);
            this.lb_MatrixB.TabIndex = 16;
            this.lb_MatrixB.Text = "Matrix B File:";
            // 
            // bt_MatrixB
            // 
            this.bt_MatrixB.Location = new System.Drawing.Point(13, 143);
            this.bt_MatrixB.Name = "bt_MatrixB";
            this.bt_MatrixB.Size = new System.Drawing.Size(94, 29);
            this.bt_MatrixB.TabIndex = 15;
            this.bt_MatrixB.Text = "Open";
            this.bt_MatrixB.UseVisualStyleBackColor = true;
            this.bt_MatrixB.Click += new System.EventHandler(this.bt_MatrixB_Click);
            // 
            // bt_StartLocal
            // 
            this.bt_StartLocal.Location = new System.Drawing.Point(205, 239);
            this.bt_StartLocal.Name = "bt_StartLocal";
            this.bt_StartLocal.Size = new System.Drawing.Size(94, 29);
            this.bt_StartLocal.TabIndex = 17;
            this.bt_StartLocal.Text = "Start Local";
            this.bt_StartLocal.UseVisualStyleBackColor = true;
            this.bt_StartLocal.Click += new System.EventHandler(this.bt_StartLocal_Click);
            // 
            // StartView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 351);
            this.Controls.Add(this.bt_StartLocal);
            this.Controls.Add(this.lb_MatrixB);
            this.Controls.Add(this.bt_MatrixB);
            this.Controls.Add(this.lb_MatrixA);
            this.Controls.Add(this.bt_MatrixA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Port);
            this.Controls.Add(this.tb_IP);
            this.Controls.Add(this.bt_Start);
            this.Name = "StartView";
            this.Text = "StartView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button bt_Start;
        private Label label2;
        private Label label1;
        private TextBox tb_Port;
        private TextBox tb_IP;
        private OpenFileDialog FileOpener;
        private Button bt_MatrixA;
        private Label lb_MatrixA;
        private Label lb_MatrixB;
        private Button bt_MatrixB;
        private ColorDialog colorDialog1;
        private Button bt_StartLocal;
    }
}