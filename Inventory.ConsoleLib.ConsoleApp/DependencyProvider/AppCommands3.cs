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
        RegisterCommand<HelpCommand<Image>, Image>(
            "Help ImagePath".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(Image.ItemId)
                , nameof(Image.Path)
            });
            
        Container.RegisterSingleton<IReadCommand<Image>, ImageReadCmd>();

        RegisterCommand<ImageReadCommand, Image>(
            "Image".ToLowerInvariant()
            , Container.Resolve<IReadCommand<Image>>());

        Container.RegisterSingleton<IInsertWizard<Image>, ImageInsertWizard>(
            nameof(ImageInsertWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<ImageInsertCommand, Image>(
            "Insert Image".ToLowerInvariant()
            , Container.Resolve<IInsertWizard<Image>>(nameof(ImageInsertWizard)));

        Container.RegisterSingleton<IUpdateWizard<Image>, ImageUpdateWizard>(
            nameof(ImageUpdateWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<ImageUpdateCommand, Image>(
            "Update Image".ToLowerInvariant()
            , Container.Resolve<IUpdateWizard<Image>>(nameof(ImageUpdateWizard)));
    }
}