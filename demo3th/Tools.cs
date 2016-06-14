using System.Collections.Generic;

namespace demo3th
{
    using ColorKeyValuePair = KeyValuePair<string, string>;

    static class Tools
    {
        static public bool EqualTo(this ColorKeyValuePair l, ColorKeyValuePair r)
        {
            if (r.Key == l.Key && r.Value == l.Value)
            {
                return true;
            }
            else
                return false;
        }
        static public readonly double MAXTIME = 5000;
        static public readonly double REFERSHINTERVAL = 10;

        internal static double Decrease()
        {
            return 50;
        }
    }
}
