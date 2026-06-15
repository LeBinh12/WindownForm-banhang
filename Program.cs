using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuanLyCuaHangTapHoa.Application.Interfaces;
using QuanLyCuaHangTapHoa.Application.UseCases;
using QuanLyCuaHangTapHoa.Domain.Interfaces;
using QuanLyCuaHangTapHoa.Infrastructure;
using QuanLyCuaHangTapHoa.Presentation;
using System;
using System.IO;
using System.Windows.Forms;

namespace QuanLyCuaHangTapHoa
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Setup DI container
            var services = new ServiceCollection();
            ConfigureServices(services);

            using (var serviceProvider = services.BuildServiceProvider())
            {
                // Run EF Migrations check (just to ensure DB exists, optional but highly robust)
                try
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        context.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cảnh báo: Không thể tự động kiểm tra/chạy migration. Chi tiết: " + ex.Message, 
                        "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Resolve and run LoginForm
                var loginForm = serviceProvider.GetRequiredService<LoginForm>();
                System.Windows.Forms.Application.Run(loginForm);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Register AppDbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            // Register Use Cases
            services.AddScoped<IProductUseCase, ProductUseCase>();
            services.AddScoped<IOrderUseCase, OrderUseCase>();
            services.AddScoped<IInvoiceUseCase, InvoiceUseCase>();
            services.AddScoped<IAccountUseCase, AccountUseCase>();

            // Register Forms
            services.AddTransient<LoginForm>();
        }
    }
}