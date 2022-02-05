using CLIFramework;
using Inventory.Modern.Lib;
using Unity;

namespace Inventory.ConsoleApp;

public class UnityDependencySuite 
    : CLIFramework.UnityDependencySuite
{
	public UnityDependencySuite(
		IUnityContainer unityContainer) :
			base(unityContainer)
	{
	}

	public override void Register()
    {
        RegisterSet<AppDatabase>();
        base.Register();
    }

    protected override void RegisterAppData() => 
		RegisterSet<AppData>();

    protected override void RegisterConsoleOutput() => 
        RegisterSet<AppOutput>();

    protected override void RegisterCommands() => 
        RegisterSet<AppCommands2>();
        
    protected override void RegisterCommandSystem() => 
		RegisterSet<AppCommandSystem<ParamCommandParser>>();
}