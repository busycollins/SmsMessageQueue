using System;
using System.Collections.Generic;
using System.Text;

namespace SmSQueue.Core.Notification
{
   public class SmsSent
    {
        public string Message { get; }

        private readonly DateTime DateSent;
        public SmsSent(string phoneNumber)
        {
            DateSent = DateTime.Now.Date;
            Message = $"Sms sent to phone number:{phoneNumber} at {DateSent}";
        }
    }
}
