using CLIFramework;
using Unity;

namespace Inventory.ConsoleApp;

public class AppData 
    : CLIFramework.AppData
{
    public AppData(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void SetAppConfigData()
    {
        Config["AppName"] = "Inventory";
        Config["CommandParser"] = nameof(ParamCommandParser);
    }
}