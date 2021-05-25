using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cw
{
	// Token: 0x02000006 RID: 6
	internal class cwapi
	{
		// Token: 0x0600004D RID: 77
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr OpenProcess(cwapi.ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

		// Token: 0x0600004E RID: 78
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, long nSize, out IntPtr lpNumberOfBytesRead);

		// Token: 0x0600004F RID: 79
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [MarshalAs(UnmanagedType.AsAny)] object lpBuffer, long nSize, out IntPtr lpNumberOfBytesWritten);

		// Token: 0x06000050 RID: 80
		[DllImport("kernel32.dll")]
		private static extern bool Process32First(IntPtr hSnapshot, ref cwapi.PROCESSENTRY32 lppe);

		// Token: 0x06000051 RID: 81
		[DllImport("kernel32.dll")]
		private static extern bool Process32Next(IntPtr hSnapshot, ref cwapi.PROCESSENTRY32 lppe);

		// Token: 0x06000052 RID: 82
		[DllImport("kernel32.dll")]
		private static extern bool Module32First(IntPtr hSnapshot, ref cwapi.MODULEENTRY32 lpme);

		// Token: 0x06000053 RID: 83
		[DllImport("kernel32.dll")]
		private static extern bool Module32Next(IntPtr hSnapshot, ref cwapi.MODULEENTRY32 lpme);

		// Token: 0x06000054 RID: 84
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hHandle);

		// Token: 0x06000055 RID: 85
		[DllImport("user32.dll")]
		public static extern short GetAsyncKeyState(Keys vKey);

		// Token: 0x06000056 RID: 86
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot(cwapi.SnapshotFlags dwFlags, int th32ProcessID);

		// Token: 0x06000057 RID: 87 RVA: 0x00007A28 File Offset: 0x00005C28
		public static IntPtr GetModuleBaseAddress(Process proc, string modName)
		{
			IntPtr result = IntPtr.Zero;
			foreach (object obj in proc.Modules)
			{
				ProcessModule processModule = (ProcessModule)obj;
				if (processModule.ModuleName == modName)
				{
					result = processModule.BaseAddress;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00007A98 File Offset: 0x00005C98
		public static IntPtr GetModuleBaseAddress(int procId, string modName)
		{
			IntPtr result = IntPtr.Zero;
			IntPtr intPtr = cwapi.CreateToolhelp32Snapshot(cwapi.SnapshotFlags.Module | cwapi.SnapshotFlags.Module32, procId);
			if (intPtr.ToInt64() != -1L)
			{
				cwapi.MODULEENTRY32 moduleentry = default(cwapi.MODULEENTRY32);
				moduleentry.dwSize = (uint)Marshal.SizeOf(typeof(cwapi.MODULEENTRY32));
				if (cwapi.Module32First(intPtr, ref moduleentry))
				{
					while (!moduleentry.szModule.Equals(modName))
					{
						if (!cwapi.Module32Next(intPtr, ref moduleentry))
						{
							goto IL_63;
						}
					}
					result = moduleentry.modBaseAddr;
				}
			}
			IL_63:
			cwapi.CloseHandle(intPtr);
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00007B10 File Offset: 0x00005D10
		public static IntPtr FindDMAAddy(IntPtr hProc, IntPtr ptr, int[] offsets)
		{
			byte[] array = new byte[IntPtr.Size];
			foreach (int offset in offsets)
			{
				IntPtr intPtr;
				cwapi.ReadProcessMemory(hProc, ptr, array, (long)array.Length, out intPtr);
				ptr = ((IntPtr.Size == 4) ? IntPtr.Add(new IntPtr(BitConverter.ToInt32(array, 0)), offset) : (ptr = IntPtr.Add(new IntPtr(BitConverter.ToInt64(array, 0)), offset)));
			}
			return ptr;
		}

		// Token: 0x040000A3 RID: 163
		private const int INVALID_HANDLE_VALUE = -1;

		// Token: 0x02000008 RID: 8
		[Flags]
		public enum ProcessAccessFlags : uint
		{
			// Token: 0x040000A8 RID: 168
			All = 2035711U,
			// Token: 0x040000A9 RID: 169
			Terminate = 1U,
			// Token: 0x040000AA RID: 170
			CreateThread = 2U,
			// Token: 0x040000AB RID: 171
			VirtualMemoryOperation = 8U,
			// Token: 0x040000AC RID: 172
			VirtualMemoryRead = 16U,
			// Token: 0x040000AD RID: 173
			VirtualMemoryWrite = 32U,
			// Token: 0x040000AE RID: 174
			DuplicateHandle = 64U,
			// Token: 0x040000AF RID: 175
			CreateProcess = 128U,
			// Token: 0x040000B0 RID: 176
			SetQuota = 256U,
			// Token: 0x040000B1 RID: 177
			SetInformation = 512U,
			// Token: 0x040000B2 RID: 178
			QueryInformation = 1024U,
			// Token: 0x040000B3 RID: 179
			QueryLimitedInformation = 4096U,
			// Token: 0x040000B4 RID: 180
			Synchronize = 1048576U
		}

		// Token: 0x02000009 RID: 9
		public struct PROCESSENTRY32
		{
			// Token: 0x040000B5 RID: 181
			public uint dwSize;

			// Token: 0x040000B6 RID: 182
			public uint cntUsage;

			// Token: 0x040000B7 RID: 183
			public uint th32ProcessID;

			// Token: 0x040000B8 RID: 184
			public IntPtr th32DefaultHeapID;

			// Token: 0x040000B9 RID: 185
			public uint th32ModuleID;

			// Token: 0x040000BA RID: 186
			public uint cntThreads;

			// Token: 0x040000BB RID: 187
			public uint th32ParentProcessID;

			// Token: 0x040000BC RID: 188
			public int pcPriClassBase;

			// Token: 0x040000BD RID: 189
			public uint dwFlags;

			// Token: 0x040000BE RID: 190
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szExeFile;
		}

		// Token: 0x0200000A RID: 10
		public struct MODULEENTRY32
		{
			// Token: 0x040000BF RID: 191
			internal uint dwSize;

			// Token: 0x040000C0 RID: 192
			internal uint th32ModuleID;

			// Token: 0x040000C1 RID: 193
			internal uint th32ProcessID;

			// Token: 0x040000C2 RID: 194
			internal uint GlblcntUsage;

			// Token: 0x040000C3 RID: 195
			internal uint ProccntUsage;

			// Token: 0x040000C4 RID: 196
			internal IntPtr modBaseAddr;

			// Token: 0x040000C5 RID: 197
			internal uint modBaseSize;

			// Token: 0x040000C6 RID: 198
			internal IntPtr hModule;

			// Token: 0x040000C7 RID: 199
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string szModule;

			// Token: 0x040000C8 RID: 200
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string szExePath;
		}

		// Token: 0x0200000B RID: 11
		[Flags]
		private enum SnapshotFlags : uint
		{
			// Token: 0x040000CA RID: 202
			HeapList = 1U,
			// Token: 0x040000CB RID: 203
			Process = 2U,
			// Token: 0x040000CC RID: 204
			Thread = 4U,
			// Token: 0x040000CD RID: 205
			Module = 8U,
			// Token: 0x040000CE RID: 206
			Module32 = 16U,
			// Token: 0x040000CF RID: 207
			Inherit = 2147483648U,
			// Token: 0x040000D0 RID: 208
			All = 31U,
			// Token: 0x040000D1 RID: 209
			NoHeaps = 1073741824U
		}
	}
}
