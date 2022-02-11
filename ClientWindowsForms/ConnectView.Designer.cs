namespace ClientWindowsForms
{
    partial class ConnectView
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
            this.bt_Connect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.cb_MultiThread = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bt_Connect
            // 
            this.bt_Connect.Location = new System.Drawing.Point(93, 219);
            this.bt_Connect.Name = "bt_Connect";
            this.bt_Connect.Size = new System.Drawing.Size(94, 29);
            this.bt_Connect.TabIndex = 9;
            this.bt_Connect.Text = "Connect";
            this.bt_Connect.UseVisualStyleBackColor = true;
            this.bt_Connect.Click += new System.EventHandler(this.bt_Connect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP:";
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(79, 128);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(125, 27);
            this.tb_Port.TabIndex = 6;
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(79, 52);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(125, 27);
            this.tb_IP.TabIndex = 5;
            // 
            // cb_MultiThread
            // 
            this.cb_MultiThread.AutoSize = true;
            this.cb_MultiThread.Location = new System.Drawing.Point(79, 172);
            this.cb_MultiThread.Name = "cb_MultiThread";
            this.cb_MultiThread.Size = new System.Drawing.Size(131, 24);
            this.cb_MultiThread.TabIndex = 10;
            this.cb_MultiThread.Text = "Use all Threads";
            this.cb_MultiThread.UseVisualStyleBackColor = true;
            // 
            // ConnectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 303);
            this.Controls.Add(this.cb_MultiThread);
            this.Controls.Add(this.bt_Connect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Port);
            this.Controls.Add(this.tb_IP);
            this.Name = "ConnectView";
            this.Text = "ShowCalculation_cs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button bt_Connect;
        private Label label2;
        private Label label1;
        private TextBox tb_Port;
        private TextBox tb_IP;
        private CheckBox cb_MultiThread;
    }
}