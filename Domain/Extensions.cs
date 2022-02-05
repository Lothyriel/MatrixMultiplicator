using Domain.Exceptions;
using Newtonsoft.Json;

namespace Domain
{
    public static class Extensions
    {
        public static T Desserialize<T>(this string received)
        {
            return JsonConvert.DeserializeObject<T>(received) ?? throw new DesserializeException(received);
        }
        public static HashSet<T> AddAndReturn<T>(this HashSet<T> hashSet, T item)
        {
            hashSet.Add(item);
            return hashSet;
        }
    }
}
