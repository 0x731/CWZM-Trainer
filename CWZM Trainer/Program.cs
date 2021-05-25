using System;
using System.Windows.Forms;

namespace learn_c___in_cs
{
	// Token: 0x02000003 RID: 3
	internal static class Program
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000079A7 File Offset: 0x00005BA7
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
