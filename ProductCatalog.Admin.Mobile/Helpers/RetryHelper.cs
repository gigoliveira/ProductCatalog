using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Admin.Mobile.Helpers
{
    public static class RetryHelper
    {
        public static async Task<T?> ExecuteWithRetryAsync<T>(
            Func<CancellationToken, Task<T>> action,
            int maxRetries = 3,
            TimeSpan? delay = null,
            TimeSpan? timeoutPerTry = null)
        {
            delay ??= TimeSpan.FromSeconds(2);
            timeoutPerTry ??= TimeSpan.FromSeconds(5);

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                using var cts = new CancellationTokenSource(timeoutPerTry.Value);
                try
                {
                    return await action(cts.Token);
                }
                catch (OperationCanceledException) when (cts.IsCancellationRequested)
                {
                    Console.WriteLine($"Attempt {attempt} timed out after {timeoutPerTry.Value.TotalSeconds} seconds.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Attempt {attempt} failed: {ex.Message}");
                }

                if (attempt < maxRetries)
                    await Task.Delay(delay.Value);
            }

            return default;
        }
    }

}
