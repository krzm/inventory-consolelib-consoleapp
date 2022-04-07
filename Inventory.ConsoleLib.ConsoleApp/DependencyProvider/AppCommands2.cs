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

public class AppCommands2 
    : AppCommands
{
    public AppCommands2(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();
        RegisterItemCategoryCommands();
    }

    private void RegisterItemCategoryCommands()
    {
        RegisterCommand<HelpCommand<ItemCategory>, ItemCategory>(
            "Help ItemCategory".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(ItemCategory.Name)
                , nameof(ItemCategory.Description)
                , nameof(ItemCategory.ParentId)
            });

        Container.RegisterSingleton<IReadCommand<ItemCategory>, ItemCategoryReadCmd>();

        RegisterCommand<ItemCategoryReadCommand, ItemCategory>(
            "ItemCategory".ToLowerInvariant()
            , Container.Resolve<IReadCommand<ItemCategory>>());

        Container.RegisterSingleton<IInsertWizard<ItemCategory>, ItemCategoryInsertWizard>(
            nameof(ItemCategoryInsertWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<IReader<string>>(nameof(OptionalTextReader))
                //, GetModelAReadConfig()
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<ItemCategoryInsertCommand, ItemCategory>(
            "Insert ItemCategory".ToLowerInvariant()
            , Container.Resolve<IInsertWizard<ItemCategory>>(nameof(ItemCategoryInsertWizard)));

        Container.RegisterSingleton<IUpdateWizard<ItemCategory>, ItemCategoryUpdateWizard>(
            nameof(ItemCategoryUpdateWizard)
            , new InjectionConstructor(
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<IReader<string>>(nameof(OptionalTextReader))
                //, GetModelAReadConfig()
                , Container.Resolve<ILogger>()
            ));

        RegisterCommand<ItemCategoryUpdateCommand, ItemCategory>(
            "Update ItemCategory".ToLowerInvariant()
            , Container.Resolve<IUpdateWizard<ItemCategory>>(nameof(ItemCategoryUpdateWizard)));
    }
}