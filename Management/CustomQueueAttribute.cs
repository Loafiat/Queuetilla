using System;

namespace Queuetilla
{
    /// <summary>
    /// Typically interfaced with <see cref="ICustomQueueManager"/> but doesn't have to be.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomQueueAttribute : Attribute
    {
        public CustomQueueAttribute(string queueName)
        {
            Queuetilla.queues.Add(queueName);
        }
    }
}