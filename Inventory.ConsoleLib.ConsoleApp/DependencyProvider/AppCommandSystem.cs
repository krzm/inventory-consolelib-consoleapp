using CLIFramework;
using Inventory.Console.Lib;
using Unity;

namespace Inventory.ConsoleApp;

public class AppCommandSystem<TParser> 
    : CLIFramework.AppCommandSystem<TParser>
        where TParser : ICommandParser
{
    private ICommandRunner? commandRunner;

    public AppCommandSystem(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void SetCommandDependencies()
    {
        commandRunner = Container.Resolve<ICommandRunner>();
        ArgumentNullException.ThrowIfNull(commandRunner);
        SetCommandRunner<ItemInsertCommand>("insert item");
        SetCommandRunner<ItemUpdateCommand>("update item");
        SetCommandRunner<CategoryInsertCommand>("insert category");
        SetCommandRunner<CategoryUpdateCommand>("update category");
        SetCommandRunner<ImageInsertCommand>("insert image");
        SetCommandRunner<ImageUpdateCommand>("update image");
    }

    private void SetCommandRunner<TCmdType>(string key)
        where TCmdType : class, IDataCommand
    {
        var cmd = Container.Resolve<IAppCommand>(key) as TCmdType;
        ArgumentNullException.ThrowIfNull(cmd);
        cmd.SetCommandRunner(commandRunner!);
    }
}