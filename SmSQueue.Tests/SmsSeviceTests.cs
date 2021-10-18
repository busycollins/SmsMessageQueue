using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SmSQueue.Core.Commands;
using SmSQueue.Core.Messaging;
using SmSQueue.Core.Notification;
using SmSQueue.Core.Providers;
using System;
using System.Text.Json;

namespace SmSQueue.Tests
{
    using static It;
    public class SmsSeviceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SmsShouldBeSentToProvider_AndEventShouldBeBroadCasted()
        {
            var mockNotificationBus = new Mock<INotification>();
            mockNotificationBus.Setup(m => m.BroadCast(IsAny<SmsSent>()));

            var mockLogger = new Mock<ILogger<SmsService>>();

            var mockSmsProvider = new Mock<ISmsProvider>();
            mockSmsProvider.Setup(m => m.Send(IsAny<string>(), IsAny<string>())).Returns(true);

            var messageQueue = new TestQueue();

            var smsService = new SmsService(mockLogger.Object, messageQueue, mockNotificationBus.Object, mockSmsProvider.Object);
            smsService.InitializeService();

            var msgBody = JsonSerializer.SerializeToUtf8Bytes(new SendSmsCommand
            {
                PhoneNumber = "+2348148657415",
                Message = "Test message"
            });

            messageQueue.OnMessageRecieved(new QueueEventArgs
            {
                DeliveryTag = "Test Tag",
                ReDelivered = false,
                Body = msgBody,
                ConsumerTag = "Test consumer",
                Exchange = "Test Exchange",
                RoutingKey = "Test key"
            });

            mockSmsProvider.Verify(v => v.Send(IsAny<string>(), IsAny<string>()), Times.Once);
            mockNotificationBus.Verify(v => v.BroadCast(IsAny<SmsSent>()), Times.Once);
        }
    }

    public class TestQueue : IMessageQueue
    {
        public event IMessageQueue.QueueEventHandler MessageRecieved;

        public bool Publish<T>(T data)
        {
            return true;
        }

        public void OnMessageRecieved(QueueEventArgs eventArgs)
        {
            MessageRecieved(this, eventArgs);
        }
    }
}
