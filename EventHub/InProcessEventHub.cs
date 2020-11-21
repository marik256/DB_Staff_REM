using System;
using System.Collections.Generic;
using System.Linq;

namespace DB_Staff_REM.EventHub
{
    internal class InProcessEventHub : IEventHub
    {
        private readonly IDictionary<Type, List<object>> _registeredHandlers = new Dictionary<Type, List<object>>();

        public void Publish<T>(T message)
        {
            if (_registeredHandlers.TryGetValue(typeof(T), out var handlerObjects))
            {
                foreach (var handler in handlerObjects.Cast<IEventHandler<T>>())
                {
                    handler.Handle(message);
                }
            }
        }

        public void Subscribe<TMessage, THandler>(THandler handler) where THandler : IEventHandler<TMessage>
        {
            var messageType = typeof(TMessage);
            if (!_registeredHandlers.TryGetValue(messageType, out var handlers))
            {
                handlers = new List<object>();
                _registeredHandlers.Add(messageType, handlers);
            }
            handlers.Add(handler);
        }
    }
}
