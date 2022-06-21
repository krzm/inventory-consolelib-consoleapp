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
        RegisterCommand<HelpCommand<Category>, Category>(
            "Help Category".ToLowerInvariant()
            , Container.Resolve<IOutput>()
            , new string[]
            {
                nameof(Category.Name)
                , nameof(Category.Description)
                , nameof(Category.ParentId)
            });

        Container.RegisterSingleton<IReadCommand<Category>, CategoryReadCmd>();

        RegisterCommand<CategoryReadCommand, Category>(
            "Category".ToLowerInvariant()
            , Container.Resolve<IReadCommand<Category>>());

        Container.RegisterSingleton<IInsertWizard<Category>, CategoryInsertWizard>(
            nameof(CategoryInsertWizard)
            , new InjectionConstructor( new object[] {
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<IReader<string>>(nameof(OptionalTextReader))
                , Container.Resolve<ILogger>()
            }));

        RegisterCommand<CategoryInsertCommand, Category>(
            "Insert Category".ToLowerInvariant()
            , Container.Resolve<IInsertWizard<Category>>(nameof(CategoryInsertWizard)));

        Container.RegisterSingleton<IUpdateWizard<Category>, CategoryUpdateWizard>(
            nameof(CategoryUpdateWizard)
            , new InjectionConstructor(
                Container.Resolve<IInventoryUnitOfWork>()
                , Container.Resolve<IReader<string>>(nameof(RequiredTextReader))
                , Container.Resolve<IReader<string>>(nameof(OptionalTextReader))
                , Container.Resolve<ILogger>()
            ));

        RegisterCommand<CategoryUpdateCommand, Category>(
            "Update Category".ToLowerInvariant()
            , Container.Resolve<IUpdateWizard<Category>>(nameof(CategoryUpdateWizard)));
    }
}