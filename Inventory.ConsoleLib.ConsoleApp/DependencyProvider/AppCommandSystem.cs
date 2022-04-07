using CLIFramework;
using Inventory.Console.Lib;
using Unity;

namespace Inventory.ConsoleApp;

public class AppCommandSystem<TParser> 
    : CLIFramework.AppCommandSystem<TParser>
    where TParser : ICommandParser
{
    public AppCommandSystem(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void SetCommandDependencies()
    {
        var commandRunner = Container.Resolve<ICommandRunner>();

        (Container.Resolve<IAppCommand>("insert item") as ItemInsertCommand)
            .SetCommandRunner(commandRunner);
        (Container.Resolve<IAppCommand>("update item") as ItemUpdateCommand)
            .SetCommandRunner(commandRunner);

        (Container.Resolve<IAppCommand>("insert itemcategory") as ItemCategoryInsertCommand)
            .SetCommandRunner(commandRunner);
        (Container.Resolve<IAppCommand>("update itemcategory") as ItemCategoryUpdateCommand)
            .SetCommandRunner(commandRunner);

        (Container.Resolve<IAppCommand>("insert itemimage") as ItemImageInsertCommand)
            .SetCommandRunner(commandRunner);
        (Container.Resolve<IAppCommand>("update itemimage") as ItemImageUpdateCommand)
            .SetCommandRunner(commandRunner);
    }
}