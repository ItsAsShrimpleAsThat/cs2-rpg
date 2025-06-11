using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2_rpg.csinterop
{
    public static class ChatSender
    {
        public static Queue<EnqueuedMessage> messageQueue = new Queue<EnqueuedMessage>();
        public static string? awaitingMessage;

        public static void SendChatMessage(string message)
        {
            messageQueue.Enqueue(new EnqueuedMessage(message, DateTimeOffset.Now.ToUnixTimeMilliseconds()));

            Console.WriteLine("Enqueueing Message: " + message);
        }

        public static void StartMessageSender()
        {
            while(true)
            {
                if(messageQueue.Count == 0)
                {
                    Thread.Sleep(100);
                    continue;
                }

                if(awaitingMessage == null)
                {
                    if (messageQueue.Count > 0)
                    {
                        awaitingMessage = messageQueue.Dequeue().message;
                        CFG.SetMessage(awaitingMessage);
                    }
                    else
                    {
                        CFG.ClearMessage();
                    }
                }
                else
                {
                    if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - messageQueue.First().queuedTime > Constants.sendMessageTimeoutMs)
                    {
                        Console.WriteLine("MESSAGE TIMED OUT");

                        awaitingMessage = null;
                        messageQueue.Dequeue();
                        CFG.ClearMessage();

                        Console.WriteLine(messageQueue.Count);
                        continue;
                    }
                }
            }
        }

        public static void CheckIfMessageSent(string recievedMessage)
        {
            if(recievedMessage == awaitingMessage)
            {
                awaitingMessage = null;
                CFG.ClearMessage();
            }
        }
    }

    public struct EnqueuedMessage
    {
        public string message;
        public long queuedTime;

        public EnqueuedMessage(string message, long queuedTime)
        {
            this.message = message;
            this.queuedTime = queuedTime;
        }
    }
}
