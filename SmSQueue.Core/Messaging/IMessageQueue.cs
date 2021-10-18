using System;
using System.Collections.Generic;
using System.Text;

namespace SmSQueue.Core.Messaging
{

    /// <summary>
    /// Abstraction to represent a connection to a message queue
    /// </summary>
    public interface IMessageQueue
    {
        /// <summary>
        /// Message Received event to be handled when a message is available on the queue
        /// </summary>
        /// 
        public delegate void QueueEventHandler(IMessageQueue sender, QueueEventArgs eventArgs);
        public event QueueEventHandler MessageRecieved;
        public bool Publish<T>(T data);
        public void OnMessageRecieved(QueueEventArgs eventArgs);

    }
}
