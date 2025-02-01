
using System;
using System.Linq;

namespace Extensions
{
    public static class DataExtensions
    {
        private static readonly Random _random = new();

        // Возвращает случайное значение enum, исключая None
        public static T GetRandom<T>(this T enumValue) where T : Enum
        {
            var values = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(v => !v.ToString().Equals("None", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (values.Length == 0)
            {
                throw new InvalidOperationException($"Enum {typeof(T).Name} has no valid values after excluding None.");
            }

            return values[_random.Next(values.Length)];
        }

        // Возвращает случайный индекс enum, исключая None
        public static int GetRandomIndex<T>(this T enumValue) where T : Enum
        {
            var values = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(v => !v.ToString().Equals("None", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (values.Length == 0)
            {
                throw new InvalidOperationException($"Enum {typeof(T).Name} has no valid values after excluding None.");
            }

            return _random.Next(values.Length);
        }
    }
}