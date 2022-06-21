using Inventory.Data;
using Inventory.Console.Lib;
using Unity;
using CLIFramework;
using CLIHelper;
using CLIReader;
using CRUDCommandHelper;
using CLIWizardHelper;
using Inventory.Wizard.Lib;
using Unity.Injection;
using Serilog;

namespace Inventory.ConsoleApp;

public class AppCommands 
    : CLIFramework.AppCommands
{
    public AppCommands(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();
		RegisterItemCommands();
    }

    private void RegisterItemCommands()
    {
        RegisterCommand<HelpCommand<Item>, Item>(
            "Help Item".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(Item.CategoryId)
                , nameof(Item.Name)
            });
        
        Container.RegisterSingleton<IReadCommand<Item>, ItemReadCmd>();

        RegisterCommand<ItemReadCommand, Item>(
            "Item".ToLowerInvariant()
            , Container.Resolve<IReadCommand<Item>>());

        Container.RegisterSingleton<IInsertWizard<Item>, ItemInsertWizard>(
            nameof(ItemInsertWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<ItemInsertCommand, Item>(
            "Insert Item".ToLowerInvariant()
            , Container.Resolve<IInsertWizard<Item>>(nameof(ItemInsertWizard)));

        Container.RegisterSingleton<IUpdateWizard<Item>, ItemUpdateWizard>(
            nameof(ItemUpdateWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<ItemUpdateCommand, Item>(
            "Update Item".ToLowerInvariant()
            , Container.Resolve<IUpdateWizard<Item>>(nameof(ItemUpdateWizard)));
    }
}