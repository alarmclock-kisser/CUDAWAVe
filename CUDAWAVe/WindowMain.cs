namespace CUDAWAVe
{
	public partial class WindowMain : Form
	{
		// ~~~~~ ~~~~~ ~~~~~ ATTRIBUTES ~~~~~ ~~~~~ ~~~~~ \\
		public AudioHandling? AudioH = null;
		public CudaHandling CudaH;



		private int oldChunkSize = 1024;




		// ~~~~~ ~~~~~ ~~~~~ CONSTRUCTOR ~~~~~ ~~~~~ ~~~~~ \}
		public WindowMain()
		{
			InitializeComponent();

			// Window position
			this.StartPosition = FormStartPosition.CenterScreen;
			Location = new Point(0, 0);

			// CudaHandling
			CudaH = new CudaHandling(0, listBox_log, comboBox_cudaDevices);
		}








		// ~~~~~ ~~~~~ ~~~~~ METHODS ~~~~~ ~~~~~ ~~~~~ \\
		public void Log(string message, string inner = "", int layer = 0)
		{
			string msg = "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] ";
			string tab = new('\t', layer);
			msg += tab + message;
			if (inner != "")
			{
				msg += " (" + inner + ")";
			}
			listBox_log.Items.Add(msg);
			listBox_log.SelectedIndex = listBox_log.Items.Count - 1;
		}

		public void DrawWaveform()
		{
			// Check AudioHandling
			if (AudioH == null)
			{
				pictureBox_waveform.Image = null;
				return;
			}

			// Draw
			int resolution = AudioH.GetFitResolution(pictureBox_waveform.Width);
			AudioH.DrawWaveformSmooth(pictureBox_waveform, 0, resolution, true);

			// Toggle buttons
			ToggleButtons();
		}

		public void ToggleButtons()
		{
			// Button move
			button_move.Enabled = AudioH?.Floats.Length > 0 || CudaH.PtrsLengths.Count > 0;
			button_move.Text = AudioH?.Floats.Length > 0 ? "-> Cuda" : "Host <-";

			// Button fft
			button_fft.Enabled = CudaH.PtrsLengths.Count > 0;
			button_fft.Text = CudaH.IsTimeDomain ? "IFFT" : "FFT";

		}




		// ~~~~~ ~~~~~ ~~~~~ EVENTS ~~~~~ ~~~~~ ~~~~~ \\
		private void comboBox_cudaDevices_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Update UI
			DrawWaveform();
		}

		private void numericUpDown_chunkSize_ValueChanged(object sender, EventArgs e)
		{
			int chunkSize = (int) numericUpDown_chunkSize.Value;

			if (chunkSize > oldChunkSize)
			{
				chunkSize = oldChunkSize * 2;
				numericUpDown_chunkSize.Value = Math.Min(chunkSize, numericUpDown_chunkSize.Maximum);
			}
			else if (chunkSize < oldChunkSize)
			{
				chunkSize = oldChunkSize / 2;
				numericUpDown_chunkSize.Value = Math.Max(chunkSize, numericUpDown_chunkSize.Minimum);
			}

			oldChunkSize = chunkSize;
		}

		private void button_import_Click(object sender, EventArgs e)
		{
			// OFD multi audio files at MyMusic
			OpenFileDialog ofd = new();
			ofd.Title = "Select audio file";
			ofd.Filter = "Audio files|*.wav;*.mp3;*.flac";
			ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
			ofd.Multiselect = false;

			// OFD show -> AudioHandling
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				AudioH = new AudioHandling(ofd.FileName);
			}

			// Update UI
			DrawWaveform();

			// Log
			Log("Imported audio file " + Path.GetFileName(ofd.FileName), AudioH?.Floats.Length + " samples");
		}

		private void button_move_Click(object sender, EventArgs e)
		{
			// Abort if no AudioH
			if (AudioH == null)
			{
				return;
			}

			// If Floats on AudioH
			if (AudioH.Floats.Length > 0)
			{
				int chunkSize = (int) numericUpDown_chunkSize.Value;

				// Move chunks to Cuda
				List<float[]> floats = AudioH.MakeChunks(chunkSize);
				CudaH.PtrsLengths = CudaH.PushToCuda(floats);

				if (CudaH.PtrsLengths.Count > 0)
				{
					AudioH.Floats = [];
					GC.Collect();
					Log("Pushed " + CudaH.PtrsLengths.Count + " chunks to CUDA");
				}
			}
			else if (CudaH.PtrsLengths.Count > 0)
			{
				int chunksCount = CudaH.PtrsLengths.Count;

				// Move to Host
				AudioH.Floats = AudioH.AggregateChunks(CudaH.PullFromCuda(CudaH.PtrsLengths));

				if (AudioH.Floats.Length > 0)
				{
					CudaH.PtrsLengths.Clear();
					GC.Collect();
					Log("Pulled " + chunksCount + " chunks to Host");
				}
			}

			// Update UI
			DrawWaveform();
		}

		private void button_fft_Click(object sender, EventArgs e)
		{
			// Abort if no PtrsLengths
			if (CudaH.PtrsLengths.Count == 0)
			{
				return;
			}

			// Perform FFT if not in Time Domain
			if (!CudaH.IsTimeDomain)
			{
				CudaH.PerformFFT();
			}
			else
			{
				CudaH.PerformIFFT();
			}

			// Update UI
			DrawWaveform();
		}
	}
}
