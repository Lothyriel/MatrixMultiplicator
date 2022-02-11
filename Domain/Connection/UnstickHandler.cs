using Domain.ExtensionMethods;
using System.Collections.Concurrent;

namespace Domain.Connection
{
    public class UnstickHandler<T>
    {
        public ConcurrentQueue<T> RequestQueue { get; }
        public string Buffer { get; set; }
        private static object Lock = new();

        public UnstickHandler()
        {
            Buffer = string.Empty;
            RequestQueue = new();
        }

        public async Task<T> Next()
        {
            T? request;
            while (!RequestQueue.TryDequeue(out request))
            {
                await Task.Delay(100);
            }
            return request;
        }

        public void Add(string received)
        {
            Task.Run(() => Desserialize(received));
        }

        private void Desserialize(string received)
        {
            lock (Lock)
            {
                Buffer += received;
                var requests = Buffer.Split('}').Where(r => r != string.Empty);
                foreach (var request in requests)
                {
                    var r = request + '}';
                    (bool success, T? value) = UnstickHandler<T>.TryDesserialize(r);
                    if (success)
                    {
                        RequestQueue.Enqueue(value!);
                    }
                    else
                    {
                        Buffer = request;
                        break;
                    }
                }
            }
        }

        private static (bool, T?) TryDesserialize(string possibleRequest)
        {
            try
            {
                T request = possibleRequest.Desserialize<T>();
                return (true, request);
            }
            catch (Exception)
            {
                return (false, default);
            }
        }
    }
}