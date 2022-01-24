using Console.Lib;
using Inventory.Console.Lib;
using Unity;

namespace Inventory.ConsoleApp;

public class AppCommandSystem<TParser> : global::Console.Lib.AppCommandSystem<TParser>
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

        (Container.Resolve<IAppCommand>("insert item") as ItemInsertCommand).SetCommandRunner(commandRunner);
        (Container.Resolve<IAppCommand>("update item") as ItemUpdateCommand).SetCommandRunner(commandRunner);

        (Container.Resolve<IAppCommand>("insert itemcategory") as ItemCategoryInsertCommand).SetCommandRunner(commandRunner);

        (Container.Resolve<IAppCommand>("insert imagepath") as ItemImageInsertCommand).SetCommandRunner(commandRunner);
        (Container.Resolve<IAppCommand>("update imagepath") as ItemImageUpdateCommand).SetCommandRunner(commandRunner);
    }
}