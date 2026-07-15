using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Decorators;

namespace WorldRank.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configuration = MediatRConfigurationBuilder
            .Create(ThisAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();

            builder.RegisterMediatR(configuration);

            builder.RegisterGenericDecorator(
            typeof(LoggingRequestHandler<,>),
            typeof(IRequestHandler<,>));

            builder.RegisterGenericDecorator(
            typeof(CachingRequestHandler<,>),
            typeof(IRequestHandler<,>));
        }
    }
}
