using Domain.Exceptions;
using Newtonsoft.Json;

namespace Domain.ExtensionMethods
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
        public static Out[] Denullify<In, Out>(this IEnumerable<In> enumerable)
        {
            return enumerable.Where(x => x is not null).Cast<Out>().ToArray();
        }
    }
}
