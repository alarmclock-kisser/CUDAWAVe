namespace CUDAWAVe
{
	public partial class WindowMain : Form
	{
		// ~~~~~ ~~~~~ ~~~~~ ATTRIBUTES ~~~~~ ~~~~~ ~~~~~ \\
		public AudioHandling? AudioH = null;
		public CudaHandling CudaH;




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





		// ~~~~~ ~~~~~ ~~~~~ EVENTS ~~~~~ ~~~~~ ~~~~~ \\






	}
}
