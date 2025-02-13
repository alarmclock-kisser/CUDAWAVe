using ManagedCuda;
using ManagedCuda.BasicTypes;
using ManagedCuda.CudaFFT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUDAWAVe
{
	public class CudaHandling
	{
		// ~~~~~ ~~~~~ ~~~~~ ATTRIBUTES ~~~~~ ~~~~~ ~~~~~ \\
		public ListBox LogBox;
		public ComboBox DeviceBox;

		public PrimaryContext? Ctx = null;

		public Dictionary<CUdeviceptr, int> PtrsLengths = [];
		public bool IsTimeDomain = false;



		// ~~~~~ ~~~~~ ~~~~~ CONSTRUCTOR ~~~~~ ~~~~~ ~~~~~ \\
		public CudaHandling(int deviceId = 0, ListBox? logBox = null, ComboBox? deviceBox = null)
		{
			LogBox = logBox ?? new ListBox();
			DeviceBox = deviceBox ?? new ComboBox();

			// Register events
			DeviceBox.SelectedIndexChanged += (sender, e) => InitDevice(DeviceBox.SelectedIndex);

			// Fill devices box
			FillDevicesBox(deviceId);
		}




		// ~~~~~ ~~~~~ ~~~~~ METHODS ~~~~~ ~~~~~ ~~~~~ \\
		// ~~~~~ UI ~~~~~ \\
		public void Log(string message, string inner = "", int layer = 1)
		{
			string msg = "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] ";
			string tab = new string('\t', layer);
			msg += tab + message;
			if (inner != "")
			{
				msg += " (" + inner + ")";
			}
			LogBox.Items.Add(msg);

			LogBox.SelectedIndex = LogBox.Items.Count - 1;
		}

		public void FillDevicesBox(int init = 0)
		{
			DeviceBox.Items.Clear();
			DeviceBox.Items.AddRange(GetDeviceNames());
			DeviceBox.Items.Add("No CUDA device (!)");
			DeviceBox.SelectedIndex = init;
		}


		// ~~~~~ Pre-init info ~~~~~ \\
		public int GetDeviceCount()
		{
			int count = 0;

			try
			{
				count = CudaContext.GetDeviceCount();
			}
			catch (Exception e)
			{
				LogBox.Items.Add(e.Message);
			}

			return count;
		}

		public string[] GetDeviceNames()
		{
			DeviceBox.Items.Clear();

			int count = GetDeviceCount();
			string[] names = new string[count];

			try
			{
				for (int i = 0; i < count; i++)
				{
					names[i] = CudaContext.GetDeviceName(i);
				}
			}
			catch (Exception e)
			{
				LogBox.Items.Add(e.Message);
			}

			return names;
		}


		// ~~~~~ Init ~~~~~ \\
		public void InitDevice(int deviceId)
		{
			Dispose();

			// Abort if no device selected
			if (deviceId < 0 || deviceId >= GetDeviceCount())
			{
				Log("No CUDA device selected");
				return;
			}

			try
			{
				Ctx = new PrimaryContext(deviceId);
				Ctx.SetCurrent();
				Log("CUDA device initialized", Ctx.GetDeviceName());
			}
			catch (Exception e)
			{
				Log("CUDA device initialization failed", e.Message);
			}
		}

		public void Dispose()
		{
			Ctx?.Dispose();
			Ctx = null;
		}


		// ~~~~~ Memory ~~~~~ \\
		public Dictionary<CUdeviceptr, int> PushToCuda(List<float[]> chunks)
		{
			Dictionary<CUdeviceptr, int> ptrs = [];

			// Abort if no Ctx
			if (Ctx == null)
			{
				Log("No CUDA device initialized");
				return ptrs;
			}

			// Push chunks
			try
			{
				foreach (float[] chunk in chunks)
				{
					CUdeviceptr ptr = Ctx.AllocateMemory(chunk.Length * sizeof(float));

					Ctx.CopyToDevice(ptr, chunk);
					ptrs[ptr] = chunk.Length;
				}
			}
			catch (Exception e)
			{
				Log("CUDA memory push failed", e.Message);
			}

			return ptrs;
		}

		public List<float[]> PullFromCuda(Dictionary<CUdeviceptr, int> ptrsLengths)
		{
			List<float[]> chunks = [];

			// Abort if no Ctx
			if (Ctx == null)
			{
				Log("No CUDA device initialized");
				return chunks;
			}

			// Pull chunks
			try
			{
				foreach (KeyValuePair<CUdeviceptr, int> ptrLength in ptrsLengths)
				{
					float[] chunk = new float[ptrLength.Value];

					Ctx.CopyToHost(chunk, ptrLength.Key);
					chunks.Add(chunk);

					Ctx.FreeMemory(ptrLength.Key);
				}
			}
			catch (Exception e)
			{
				Log("CUDA memory pull failed", e.Message);
			}

			return chunks;
		}

		public void PerformFFT()
		{
			// Abort if no Ctx, no PtrsLengths or in Time Domain
			if (Ctx == null || PtrsLengths.Count == 0 || IsTimeDomain)
			{
				Log("No CUDA device initialized or no chunks in frequency domain");
				return;
			}

			// Toggle domain and calculate FFT lengths
			IsTimeDomain = true;
			int[] fftLengths = PtrsLengths.Values.Select(length => length / 2 + 1).ToArray();

			// Perform FFT
			try
			{
				for (int i = 0; i < PtrsLengths.Count; i++)
				{
					CUdeviceptr ptr = PtrsLengths.ElementAt(i).Key;
					int length = fftLengths[i];

					CudaFFTPlan1D plan = new(length, cufftType.R2C, 1);
					plan.Exec(ptr, ptr);

					PtrsLengths[ptr] = length;
				}
			}
			catch (Exception e)
			{
				Log("CUDA FFT failed", e.Message);
				return;
			}

			// Log
			Log("CUDA FFT performed on " + PtrsLengths.Count + " chunks");
			return;
		}

		public void PerformIFFT()
		{
			// Abort if no Ctx, no PtrsLengths or not in Time Domain
			if (Ctx == null || PtrsLengths.Count == 0 || !IsTimeDomain)
			{
				Log("No CUDA device initialized or chunks not in time domain");
				return;
			}

			// Toggle domain and calculate IFFT lengths
			IsTimeDomain = false;
			int[] ifftLengths = PtrsLengths.Values.Select(length => (length - 1) * 2).ToArray();

			// Perform IFFT
			try
			{
				for (int i = 0; i < PtrsLengths.Count; i++)
				{
					CUdeviceptr ptr = PtrsLengths.ElementAt(i).Key;
					int length = ifftLengths[i];
					CudaFFTPlan1D plan = new(length, cufftType.C2R, 1);
					plan.Exec(ptr, ptr);
					PtrsLengths[ptr] = length;
				}
			}
			catch (Exception e)
			{
				Log("CUDA IFFT failed", e.Message);
				return;
			}

			// Log
			Log("CUDA IFFT performed on " + PtrsLengths.Count + " chunks");
			return;
		}
	}
}
