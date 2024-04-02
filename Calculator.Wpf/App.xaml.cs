using System;
using System.Windows;
using Calculator.Application.Services.Implementations;
using Calculator.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.Wpf
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider _serviceProvider { get; private set; }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<IVariableService, VariableService>();
            

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();
        }
    }
}
