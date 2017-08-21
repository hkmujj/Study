using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace General.CIR.Resource
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	public class InFoResource
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				bool flag = resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("General.CIR.Resource.InFoResource", typeof(InFoResource).Assembly);
					resourceMan = resourceManager;
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		public static string 亮度调节提示信息
		{
			get
			{
				return ResourceManager.GetString("亮度调节提示信息", resourceCulture);
			}
		}

		public static string 启动报警命令已发送
		{
			get
			{
				return ResourceManager.GetString("启动报警命令已发送", resourceCulture);
			}
		}

		public static string 命令查询界面提示信息
		{
			get
			{
				return ResourceManager.GetString("命令查询界面提示信息", resourceCulture);
			}
		}

		public static string 正在发送报警信息
		{
			get
			{
				return ResourceManager.GetString("正在发送报警信息", resourceCulture);
			}
		}

		public static string 设置主界面提示信息
		{
			get
			{
				return ResourceManager.GetString("设置主界面提示信息", resourceCulture);
			}
		}

		public static string 音量调节提示信息
		{
			get
			{
				return ResourceManager.GetString("音量调节提示信息", resourceCulture);
			}
		}

		internal InFoResource()
		{
		}
	}
}
