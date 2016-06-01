using System;
using System.Collections.Generic;
using System.Windows;
using BudgetManager.Core.Configuration;
using BudgetManager.Data;
using BudgetManager.Data.Entities;
using BudgetManager.DbUpdater;
using BudgetManager.Domain;
using BudgetManager.Services;
using BudgetManager.Services.Mappings;
using Caliburn.Micro;
using Microsoft.Practices.Unity;

namespace BudgetManager
{
    public class UnityBootstrapper : BootstrapperBase
    {
        private IUnityContainer _container;

        public UnityBootstrapper()
        {
            Initialize();
        }

        protected override void BuildUp(object instance)
        {
            instance = _container.BuildUp(instance);
            base.BuildUp(instance);
        }

        protected override void Configure()
        {
            _container = new UnityContainer();
            _container.RegisterType<IWindowManager, WindowManager>();
            _container.RegisterType<IEventAggregator, EventAggregator>();
            
            _container.RegisterType<IMapper<Account, AccountEntity>, AccountMapper>();
            _container.RegisterType<IMapper<Transaction, TransactionEntity>, TransactionMapper>();

            _container.RegisterType<IAccountService, AccountService>();
            _container.RegisterType<ITransactionService, TransactionService>();

            Updater updater = new Updater(AppConfig.Config.ConnectionString);
            updater.Update();

            base.Configure();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.ResolveAll(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            object result = default(object);
            if (key != null)
            {
                result = _container.Resolve(service, key);
            }
            else
            {
                result = _container.Resolve(service);
            }

            return result;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}