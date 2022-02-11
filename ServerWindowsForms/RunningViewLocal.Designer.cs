namespace ServerWindowsForms
{
    partial class RunningViewLocal
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
            this.pb_Bitmap = new System.Windows.Forms.PictureBox();
            this.bt_Start = new System.Windows.Forms.Button();
            this.cb_MultiThreaded = new System.Windows.Forms.CheckBox();
            this.timer_Redraw = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Bitmap)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Bitmap
            // 
            this.pb_Bitmap.Location = new System.Drawing.Point(27, 12);
            this.pb_Bitmap.Name = "pb_Bitmap";
            this.pb_Bitmap.Size = new System.Drawing.Size(474, 349);
            this.pb_Bitmap.TabIndex = 0;
            this.pb_Bitmap.TabStop = false;
            // 
            // bt_Start
            // 
            this.bt_Start.Location = new System.Drawing.Point(563, 91);
            this.bt_Start.Name = "bt_Start";
            this.bt_Start.Size = new System.Drawing.Size(94, 29);
            this.bt_Start.TabIndex = 1;
            this.bt_Start.Text = "Start";
            this.bt_Start.UseVisualStyleBackColor = true;
            this.bt_Start.Click += new System.EventHandler(this.bt_Start_Click);
            // 
            // cb_MultiThreaded
            // 
            this.cb_MultiThreaded.AutoSize = true;
            this.cb_MultiThreaded.Location = new System.Drawing.Point(556, 48);
            this.cb_MultiThreaded.Name = "cb_MultiThreaded";
            this.cb_MultiThreaded.Size = new System.Drawing.Size(133, 24);
            this.cb_MultiThreaded.TabIndex = 2;
            this.cb_MultiThreaded.Text = "Use All Threads";
            this.cb_MultiThreaded.UseVisualStyleBackColor = true;
            // 
            // timer_Redraw
            // 
            this.timer_Redraw.Interval = 1000;
            this.timer_Redraw.Tick += new System.EventHandler(this.timer_Redraw_Tick);
            // 
            // RunningViewLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 400);
            this.Controls.Add(this.cb_MultiThreaded);
            this.Controls.Add(this.bt_Start);
            this.Controls.Add(this.pb_Bitmap);
            this.Name = "RunningViewLocal";
            this.Text = "RunningView";
            ((System.ComponentModel.ISupportInitialize)(this.pb_Bitmap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pb_Bitmap;
        private Button bt_Start;
        private CheckBox cb_MultiThreaded;
        private System.Windows.Forms.Timer timer_Redraw;
    }
}