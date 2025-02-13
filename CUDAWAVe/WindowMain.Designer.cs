﻿namespace CUDAWAVe
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
			button_fft = new Button();
			label_chunkSize = new Label();
			numericUpDown_chunkSize = new NumericUpDown();
			button_move = new Button();
			button_import = new Button();
			comboBox_cudaDevices = new ComboBox();
			label_vram = new Label();
			progressBar_vram = new ProgressBar();
			((System.ComponentModel.ISupportInitialize) pictureBox_waveform).BeginInit();
			groupBox_memory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) numericUpDown_chunkSize).BeginInit();
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
			groupBox_memory.Controls.Add(button_fft);
			groupBox_memory.Controls.Add(label_chunkSize);
			groupBox_memory.Controls.Add(numericUpDown_chunkSize);
			groupBox_memory.Controls.Add(button_move);
			groupBox_memory.Controls.Add(button_import);
			groupBox_memory.Location = new Point(12, 238);
			groupBox_memory.Name = "groupBox_memory";
			groupBox_memory.Size = new Size(200, 180);
			groupBox_memory.TabIndex = 2;
			groupBox_memory.TabStop = false;
			groupBox_memory.Text = "Memory";
			// 
			// button_fft
			// 
			button_fft.Location = new Point(138, 151);
			button_fft.Name = "button_fft";
			button_fft.Size = new Size(55, 23);
			button_fft.TabIndex = 4;
			button_fft.Text = "FFT";
			button_fft.UseVisualStyleBackColor = true;
			button_fft.Click += button_fft_Click;
			// 
			// label_chunkSize
			// 
			label_chunkSize.AutoSize = true;
			label_chunkSize.Location = new Point(113, 19);
			label_chunkSize.Name = "label_chunkSize";
			label_chunkSize.Size = new Size(64, 15);
			label_chunkSize.TabIndex = 4;
			label_chunkSize.Text = "Chunk size";
			// 
			// numericUpDown_chunkSize
			// 
			numericUpDown_chunkSize.Location = new Point(113, 37);
			numericUpDown_chunkSize.Maximum = new decimal(new int[] { 1048576, 0, 0, 0 });
			numericUpDown_chunkSize.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
			numericUpDown_chunkSize.Name = "numericUpDown_chunkSize";
			numericUpDown_chunkSize.Size = new Size(80, 23);
			numericUpDown_chunkSize.TabIndex = 4;
			numericUpDown_chunkSize.Value = new decimal(new int[] { 65536, 0, 0, 0 });
			numericUpDown_chunkSize.ValueChanged += numericUpDown_chunkSize_ValueChanged;
			// 
			// button_move
			// 
			button_move.Location = new Point(67, 151);
			button_move.Name = "button_move";
			button_move.Size = new Size(65, 23);
			button_move.TabIndex = 4;
			button_move.Text = "Move";
			button_move.UseVisualStyleBackColor = true;
			button_move.Click += button_move_Click;
			// 
			// button_import
			// 
			button_import.Location = new Point(6, 151);
			button_import.Name = "button_import";
			button_import.Size = new Size(55, 23);
			button_import.TabIndex = 3;
			button_import.Text = "Import";
			button_import.UseVisualStyleBackColor = true;
			button_import.Click += button_import_Click;
			// 
			// comboBox_cudaDevices
			// 
			comboBox_cudaDevices.FormattingEnabled = true;
			comboBox_cudaDevices.Location = new Point(12, 12);
			comboBox_cudaDevices.Name = "comboBox_cudaDevices";
			comboBox_cudaDevices.Size = new Size(200, 23);
			comboBox_cudaDevices.TabIndex = 3;
			comboBox_cudaDevices.SelectedIndexChanged += comboBox_cudaDevices_SelectedIndexChanged;
			// 
			// label_vram
			// 
			label_vram.AutoSize = true;
			label_vram.Location = new Point(12, 38);
			label_vram.Name = "label_vram";
			label_vram.Size = new Size(90, 15);
			label_vram.TabIndex = 4;
			label_vram.Text = "VRAM: 0 / 0 MB";
			// 
			// progressBar_vram
			// 
			progressBar_vram.Location = new Point(12, 56);
			progressBar_vram.Name = "progressBar_vram";
			progressBar_vram.Size = new Size(200, 10);
			progressBar_vram.TabIndex = 5;
			// 
			// WindowMain
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(704, 681);
			Controls.Add(progressBar_vram);
			Controls.Add(label_vram);
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
			groupBox_memory.PerformLayout();
			((System.ComponentModel.ISupportInitialize) numericUpDown_chunkSize).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ListBox listBox_log;
		private PictureBox pictureBox_waveform;
		private GroupBox groupBox_memory;
		private Button button_import;
		private ComboBox comboBox_cudaDevices;
		private Button button_move;
		private Label label_chunkSize;
		private NumericUpDown numericUpDown_chunkSize;
		private Button button_fft;
		private Label label_vram;
		private ProgressBar progressBar_vram;
	}
}
