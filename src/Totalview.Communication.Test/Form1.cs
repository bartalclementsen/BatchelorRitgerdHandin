using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Totalview.Communication;

namespace Totalview.Communication.Test
{
    public partial class Form1 : Form
    {
        public static Form1 EgVeitEgVeit { get; private set; }
        public Form1()
        {
            EgVeitEgVeit = this;
            InitializeComponent();
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services
            .AddLogging(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning);

            })
            .AddTransient<ILogger, LocalLogger>();

            return services;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var services = ConfigureServices(new ServiceCollection());
            services.AddTcpConnectionService();
            services.AddTotalviewStateService();
            services.AddTotalviewBasicDataService(); 
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger>();
            logger.LogInformation("We have logging");
            
            
            var tcp = serviceProvider.GetService<ITcpConnection>();

            var basicData = serviceProvider.GetService<ITotalviewBasicData>();

            tcp.Connect();

        }


        private void AddLogLine(string message)
        {
            if (tbLog.InvokeRequired)
            {
                tbLog.Invoke( new MethodInvoker ( delegate { AddLogLine(message); }));
            }
            else
                tbLog.Text += message + Environment.NewLine;
        }

        internal void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            AddLogLine(state.ToString());
        }
    }

    class LocalLogger : ILogger
    {
        class ScopedReturns : IDisposable
        {
            public void Dispose()
            {
                
            }
        }

        public IDisposable BeginScope<TState>(TState state) =>
            new ScopedReturns();

        public bool IsEnabled(LogLevel logLevel) =>
            Form1.EgVeitEgVeit != null;


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Form1.EgVeitEgVeit?.Log<TState>(logLevel, eventId, state, exception, formatter);
        }
    }
}
