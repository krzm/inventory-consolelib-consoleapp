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

public class AppCommands3
    : AppCommands2
{
    public AppCommands3(
        IUnityContainer container) 
        : base(container)
    {
    }

    protected override void RegisterCommands()
    {
        base.RegisterCommands();
		RegisterImagePathCommands();
    }

    private void RegisterImagePathCommands()
    {
        RegisterCommand<HelpCommand<ItemImage>, ItemImage>(
            "Help ImagePath".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(ItemImage.ItemId)
                , nameof(ItemImage.Path)
            });
            
        Container.RegisterSingleton<IReadCommand<ItemImage>, ItemImageReadCmd>();

        RegisterCommand<ItemImageReadCommand, ItemImage>(
            "ItemImage".ToLowerInvariant()
            , Container.Resolve<IReadCommand<ItemImage>>());

        Container.RegisterSingleton<IInsertWizard<ItemImage>, ItemImageInsertWizard>(
            nameof(ItemImageInsertWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                //, GetModelAReadConfig()
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<ItemImageInsertCommand, ItemImage>(
            "Insert ItemImage".ToLowerInvariant()
            , Container.Resolve<IInsertWizard<ItemImage>>(nameof(ItemImageInsertWizard)));

        Container.RegisterSingleton<IUpdateWizard<ItemImage>, ItemImageUpdateWizard>(
            nameof(ItemImageUpdateWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                //, GetModelAReadConfig()
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<ItemImageUpdateCommand, ItemImage>(
            "Update ItemImage".ToLowerInvariant()
            , Container.Resolve<IUpdateWizard<ItemImage>>(nameof(ItemImageUpdateWizard)));
    }
}