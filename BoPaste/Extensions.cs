using System;

namespace BoPaste
{
    public static class Extensions
    {
        public static int ToInt(this string txt, bool throwError = false)
        {
            var i = -1;
            var done = int.TryParse(txt, out i);

            if (!done && throwError)
                throw new Exception("Not an Int");

            return i;
        }
    }
}
