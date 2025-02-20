using Autofac;
using DotNetCore.CAP;
using FluentValidation;
using InventoryManagementSystem.App.Features.Common;
using InventoryManagementSystem.App.Features.Common.ConsumeMessages;
using InventoryManagementSystem.App.Features.Common.RabbitMQServices.RabbitMQPublisherService;
using InventoryManagementSystem.App.Features.Products.AddProduct.Command;
using InventoryManagementSystem.App.Repository;
using MediatR;

namespace InventoryManagementSystem.App.Config;
public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>()
               .As<IUnitOfWork>()
               .InstancePerLifetimeScope();


        builder.RegisterType<ProductDecreasOrchConsumer>()
       .As<ICapSubscribe>()
       .InstancePerDependency();
        //builder.RegisterType<CapConsumerService>()
        //         .As<ICapSubscribe>()
        //          .InstancePerDependency();

        builder.RegisterType<MessagePublisher>()
             .As<IMessagePublisher>()
             .InstancePerLifetimeScope();



        builder.RegisterType<Mediator>().As<IMediator>();

        builder.RegisterGeneric(typeof(BaseEndpointParam<>))
                .AsSelf()
                .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(ThisAssembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(ImageService.ImageService).Assembly)
                .Where(a => a.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(AddProductHandler).Assembly);


        builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


    }
}