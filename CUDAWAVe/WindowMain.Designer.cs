namespace CUDAWAVe
{
    partial class WindowMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			listBox_log = new ListBox();
			pictureBox_waveform = new PictureBox();
			groupBox_memory = new GroupBox();
			button_import = new Button();
			comboBox_cudaDevices = new ComboBox();
			((System.ComponentModel.ISupportInitialize) pictureBox_waveform).BeginInit();
			groupBox_memory.SuspendLayout();
			SuspendLayout();
			// 
			// listBox_log
			// 
			listBox_log.FormattingEnabled = true;
			listBox_log.ItemHeight = 15;
			listBox_log.Location = new Point(12, 530);
			listBox_log.Name = "listBox_log";
			listBox_log.Size = new Size(680, 139);
			listBox_log.TabIndex = 0;
			// 
			// pictureBox_waveform
			// 
			pictureBox_waveform.BackColor = Color.White;
			pictureBox_waveform.Location = new Point(12, 424);
			pictureBox_waveform.Name = "pictureBox_waveform";
			pictureBox_waveform.Size = new Size(680, 100);
			pictureBox_waveform.TabIndex = 1;
			pictureBox_waveform.TabStop = false;
			// 
			// groupBox_memory
			// 
			groupBox_memory.Controls.Add(button_import);
			groupBox_memory.Location = new Point(12, 238);
			groupBox_memory.Name = "groupBox_memory";
			groupBox_memory.Size = new Size(200, 180);
			groupBox_memory.TabIndex = 2;
			groupBox_memory.TabStop = false;
			groupBox_memory.Text = "Memory";
			// 
			// button_import
			// 
			button_import.Location = new Point(6, 151);
			button_import.Name = "button_import";
			button_import.Size = new Size(55, 23);
			button_import.TabIndex = 3;
			button_import.Text = "Import";
			button_import.UseVisualStyleBackColor = true;
			// 
			// comboBox_cudaDevices
			// 
			comboBox_cudaDevices.FormattingEnabled = true;
			comboBox_cudaDevices.Location = new Point(12, 12);
			comboBox_cudaDevices.Name = "comboBox_cudaDevices";
			comboBox_cudaDevices.Size = new Size(200, 23);
			comboBox_cudaDevices.TabIndex = 3;
			// 
			// WindowMain
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(704, 681);
			Controls.Add(comboBox_cudaDevices);
			Controls.Add(groupBox_memory);
			Controls.Add(pictureBox_waveform);
			Controls.Add(listBox_log);
			MaximizeBox = false;
			MaximumSize = new Size(720, 720);
			MinimumSize = new Size(720, 720);
			Name = "WindowMain";
			Text = "CUDAWAVe (GUI)";
			((System.ComponentModel.ISupportInitialize) pictureBox_waveform).EndInit();
			groupBox_memory.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private ListBox listBox_log;
		private PictureBox pictureBox_waveform;
		private GroupBox groupBox_memory;
		private Button button_import;
		private ComboBox comboBox_cudaDevices;
	}
}
