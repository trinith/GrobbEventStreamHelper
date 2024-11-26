using System;

namespace GrobbEventStreamHelper
{
    public static partial class GlobalConstants
    {
        public static class Event
        {
            public static string Name { get; private set; } = "Blood and Beard";
            public static DateTime StartTime { get; private set; } = new DateTime(new DateOnly(2024, 11, 26), new TimeOnly(17, 00));
        }
    }
}
