using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace CoreDBIteratorTests.Infrastructure
{
    /// <summary>Собирает все сообщения логов, чтобы проверять, что именно пишут воркеры.</summary>
    public sealed class ListLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentQueue<string> _messages = new();

        public IReadOnlyList<string> Messages => _messages.ToArray();

        public ILogger CreateLogger(string categoryName) => new ListLogger(_messages);

        public void Dispose() { }

        private sealed class ListLogger(ConcurrentQueue<string> messages) : ILogger
        {
            public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

            public bool IsEnabled(LogLevel logLevel) => true;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) =>
                messages.Enqueue(formatter(state, exception));
        }
    }
}
