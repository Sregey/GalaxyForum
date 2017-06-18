using System;
using System.Collections.Generic;

namespace ForumPlConsole
{
    class ActionNameComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return String.Compare(x, y, true) == 0;
        }

        public int GetHashCode(string obj)
        {
            return obj.ToLower().GetHashCode();
        }
    }
}
