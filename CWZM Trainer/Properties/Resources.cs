using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace learn_c___in_cs.Properties
{
	// Token: 0x02000004 RID: 4
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000079BE File Offset: 0x00005BBE
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000079C6 File Offset: 0x00005BC6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("learn_c___in_cs.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000079F2 File Offset: 0x00005BF2
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000079F9 File Offset: 0x00005BF9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x040000A0 RID: 160
		private static ResourceManager resourceMan;

		// Token: 0x040000A1 RID: 161
		private static CultureInfo resourceCulture;
	}
}
