using System;
using System.Collections.Generic;
using System.Text;

namespace SmSQueue.Core.Notification
{
    /// <summary>
    /// Abstraction for Broadcasting notifications
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// Broadcast data to multiple subscribers
        /// </summary>
        /// <typeparam name="T">Message Type</typeparam>
        /// <param name="data">Message to be broadcasted</param>
        ///
        public void BroadCast<T>(T data);

    }
}
