using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSystem
{
    /// <summary>Variable of the Console.</summary>
    public sealed class CVar
    {
        public CVar(string name)
        {
            Name = name;
            Maxed = false;
            Mined = false;
            Max = 0;
            Min = 0;
            accessLevel = AccessLevel.Public;
            Default = "0";
            Value = "0";
        }

        public enum AccessLevel
        {
            /// <summary>The Console is the only that can acces the variable.</summary>
            Internal,
            /// <summary> //The user can only read the variable, but not modify.</summary>
            ReadOnly,
            /// <summary>The user can read and modify the variable.</summary>
            Public,
        }

        public readonly string Name;
        public string Value;
        public string Default;
        public bool Maxed;
        public bool Mined;
        public float Max;
        public float Min;
        public AccessLevel accessLevel;
    }
}
