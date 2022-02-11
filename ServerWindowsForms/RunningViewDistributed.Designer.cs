namespace ServerWindowsForms
{
    partial class RunningViewDistributed
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
            this.pb_Bitmap = new System.Windows.Forms.PictureBox();
            this.bt_Start = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Bitmap)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Bitmap
            // 
            this.pb_Bitmap.Location = new System.Drawing.Point(27, 12);
            this.pb_Bitmap.Name = "pb_Bitmap";
            this.pb_Bitmap.Size = new System.Drawing.Size(475, 349);
            this.pb_Bitmap.TabIndex = 0;
            this.pb_Bitmap.TabStop = false;
            // 
            // bt_Start
            // 
            this.bt_Start.Location = new System.Drawing.Point(561, 59);
            this.bt_Start.Name = "bt_Start";
            this.bt_Start.Size = new System.Drawing.Size(94, 29);
            this.bt_Start.TabIndex = 1;
            this.bt_Start.Text = "Start";
            this.bt_Start.UseVisualStyleBackColor = true;
            this.bt_Start.Click += new System.EventHandler(this.bt_Start_Click);
            // 
            // RunningViewDistributed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 399);
            this.Controls.Add(this.bt_Start);
            this.Controls.Add(this.pb_Bitmap);
            this.Name = "RunningViewDistributed";
            this.Text = "RunningView";
            ((System.ComponentModel.ISupportInitialize)(this.pb_Bitmap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pb_Bitmap;
        private Button bt_Start;
    }
}