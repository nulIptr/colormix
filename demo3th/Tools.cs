using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace demo3th
{
    static class Tools
    {
        static public void copyTo(this List<Color> ListOfColors, List<string> dist)
        {
            foreach (var item in ListOfColors)
            {
                dist.Add(item.ToString());
            }
        }
        static public readonly double MAXTIME = 5000;
        static public readonly double INTERVALTIME = 10;
    }
}
