using System;
using System.Collections.Generic;
using System.Text;

namespace SmSQueue.Core.Commands
{
   public class SendSmsCommand
    {

        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public int errorSendingCount { get; set; }
    }
}
