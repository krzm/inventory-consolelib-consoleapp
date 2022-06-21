using CLIFramework;
using CLIHelper.Unity;
using CLIReader;
using Config.Wrapper.Unity;
using Inventory.Data;
using Inventory.Table.Unity;
using Serilog.Wrapper.Unity;
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

    protected override void RegisterAppData()
    {
        RegisterSet<AppLoggerSet>();
        RegisterSet<AppConfigSet>();
		RegisterSet<AppData>();
    }

    protected override void RegisterDatabase() => 
        RegisterSet<InventoryDatabase>();

    protected override void RegisterConsoleInput()
    {
        RegisterSet<CliIOSet>();
        RegisterSet<CLIReaderSet>();
    }
    
    protected override void RegisterConsoleOutput() => 
        RegisterSet<InventoryTableSet>();

    protected override void RegisterCommands() => 
        RegisterSet<AppCommands3>();
        
    protected override void RegisterCommandSystem() => 
		RegisterSet<AppCommandSystem<ParamCommandParser>>();
}