using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StockMarket.Business.Abstract;

namespace StockMarket.API.BackgroundServices
{
    public class StockUpdateService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public StockUpdateService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var stockService = scope.ServiceProvider.GetRequiredService<IStockDataService>();

                    // Burada hisse senedi verilerini çekip veritabanını güncelleyebilirsiniz
                    await stockService.UpdateStockDataAsync();

                    // Belirli bir aralıkta çalışmasını sağlamak için bekleme yapın
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }
    }
}
