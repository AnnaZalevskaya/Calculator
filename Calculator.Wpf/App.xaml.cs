using System;
using System.Windows;
using Calculator.Application.Evaluator;
using Calculator.Application.Services.Implementations;
using Calculator.Application.Services.Interfaces;
using Calculator.Core.Extensions;
using Calculator.Infrastructure.Data;
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

            services.AddSingleton<ExpressionContext>();
            services.AddSingleton<ExpressionParser>();
            services.AddSingleton<ExpressionEvaluator>();
            
            services.AddScoped<IVariableService, VariableService>();
            services.AddScoped<IExpressionParsingService, ExpressionParsingService>();

            _serviceProvider = services.BuildServiceProvider();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Initialize(_serviceProvider.GetRequiredService<ExpressionEvaluator>());
            mainWindow.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();
        }
    }
}
