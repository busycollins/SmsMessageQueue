using Microsoft.Extensions.Logging;
using SmSQueue.Core.Commands;
using SmSQueue.Core.Messaging;
using SmSQueue.Core.Notification;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SmSQueue.Core.Providers
{
    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;
        private readonly IMessageQueue _messageQueue;
        private readonly INotification _notificationBus;
        private readonly ISmsProvider _smsProvider;
        public SmsService(ILogger<SmsService> logger, IMessageQueue messageHandler, INotification notificationBus, ISmsProvider smsProvider)
        {
            _logger = logger;
            _messageQueue = messageHandler;
            _notificationBus = notificationBus;
            _smsProvider = smsProvider;
        }
        public void InitializeService()
        {
            _messageQueue.MessageRecieved += MessangHandler_MessageReceived;
        }

        private void MessangHandler_MessageReceived(IMessageQueue sender, QueueEventArgs eventArgs)
        {
            try
            {
                _logger.LogDebug($"Sms Command Received from {eventArgs.Exchange}");
                byte[] message = eventArgs.Body.ToArray();
                string text = Encoding.UTF8.GetString(message);
                var smsCommand = JsonSerializer.Deserialize<SendSmsCommand>(text);
                var smsSentSuccessfully = SendSms(smsCommand);
                if (smsSentSuccessfully)
                {
                    _notificationBus.BroadCast(new SmsSent(smsCommand.PhoneNumber));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private bool SendSms(SendSmsCommand sendSmsCommand)
        {
            bool smsSent = _smsProvider.Send(sendSmsCommand.Message, sendSmsCommand.PhoneNumber);
            if (!smsSent)
            {
                _messageQueue.Publish(sendSmsCommand);
            }
            return smsSent;
        }
    }

}

