using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace demo3
{
    public class Worker: BackgroundService
    {
        // 这里的硬编码的依赖项会产生问题（这就是直接写死了）
        private readonly MessageWriter _messageWriter = new MessageWriter();

        /// <summary>
        /// ExecuteAsync 继承的是BackgroundService里面的ExecuteAsync方法
        /// override 是对 protected abstract Task ExecuteAsync(CancellationToken stoppingToken);抽象方法的重写
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _messageWriter.Write($"Worker running at: {DateTimeOffset.Now}");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
