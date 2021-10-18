using System;
using System.Collections.Generic;
using System.Text;

namespace SmSQueue.Core.Providers
{
   public interface ISmsProvider
    {
        public bool Send(string sms, string phoneNumber);
    }
}
}
