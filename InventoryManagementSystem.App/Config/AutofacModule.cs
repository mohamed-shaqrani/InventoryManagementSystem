using Autofac;
using FluentValidation;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Repository;

namespace InventoryManagementSystem.App.Config;
public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>()
               .As<IUnitOfWork>()
               .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(BaseEndpointParam<>))
                .AsSelf()
                .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(ImageService.ImageService).Assembly)
                .Where(a => a.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();



    }
}