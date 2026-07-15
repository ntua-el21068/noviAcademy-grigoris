using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using WorldRank.Application.Infrastructure;
using WorldRank.Application.Interfaces;
using WorldRank.Infrastructure.Persistence.Commands.Players;
using WorldRank.Infrastructure.Persistence.Commands.Wallets;
using WorldRank.Infrastructure.Persistence.Queries;
using WorldRank.Infrastructure.Persistence.Queries.Players;
using WorldRank.Infrastructure.Repositories;

namespace WorldRank.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreatePlayerPersistence>()
            .As<ICreatePlayerPersistence>()
            .InstancePerLifetimeScope();

            builder.RegisterType<GetPlayerByIdPersistence>()
            .As<IGetPlayerByIdPersistence>()
            .InstancePerLifetimeScope();

            builder.RegisterType<GetAllPlayersPersistence>()
           .As<IGetAllPlayersPersistence>()
           .InstancePerLifetimeScope();



            builder.RegisterType<CreateWalletPersistence>()
            .As<ICreateWalletPersistence>()
            .InstancePerLifetimeScope();

            builder.RegisterType<DepositToWalletPersistence>()
            .As<IDepositToWalletPersistence>()
            .InstancePerLifetimeScope();
        }
    }
}
