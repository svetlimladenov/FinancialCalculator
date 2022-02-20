using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Consumers
{
    public class ReversePaymentConsumer : IConsumer<ReversePaymentRequested>
    {
        private readonly ILogger<ReversePaymentConsumer> logger;

        public ReversePaymentConsumer(ILogger<ReversePaymentConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ReversePaymentRequested> context)
        {
            this.logger.LogInformation("Reversing payment with id {0}", context.Message.PaymentId);
        }
    }
}
