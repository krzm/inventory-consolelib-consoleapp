using Console.Lib;
using Inventory.Modern.Lib;
using Unity;

namespace Inventory.ConsoleApp;

public class UnityDependencyCollection : global::Console.Lib.UnityDependencyCollection
{
	public UnityDependencyCollection(
		IUnityContainer unityContainer) :
			base(unityContainer)
	{
	}

	public override void RegisterDependencies()
    {
        RegisterDependencyProvider<AppDatabase>();
        base.RegisterDependencies();
    }

    protected override void RegisterAppData() => 
		RegisterDependencyProvider<AppData>();

    protected override void RegisterConsoleOutput() => 
        RegisterDependencyProvider<AppOutput>();

    protected override void RegisterCommands() => 
        RegisterDependencyProvider<AppCommands2>();
        
    protected override void RegisterCommandSystem() => 
		RegisterDependencyProvider<AppCommandSystem<ParamCommandParser>>();
}