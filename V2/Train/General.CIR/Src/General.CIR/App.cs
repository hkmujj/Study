using System;
using System.Windows;
using General.CIR.Models;

namespace General.CIR
{
	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			var cIrmmiBootStrapper = new CIRMMIBootStrapper();
			cIrmmiBootStrapper.Run();
		    
		}

		protected override void OnExit(ExitEventArgs e)
		{
			GlobalParams.Instance.UnInit();
			base.OnExit(e);
		}

		
	}
}
