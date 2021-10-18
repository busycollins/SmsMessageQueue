using System;
using System.Collections.Generic;
using System.Text;

namespace SmSQueue.Core.Messaging
{
   public class QueueEventArgs
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public bool ReDelivered { get; set; }
        public string ConsumerTag { get; set; }
        public ReadOnlyMemory<byte> Body { get; set; }
        public string DeliveryTag { get; set; }
    }
}
