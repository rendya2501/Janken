using System.Collections.Generic;

namespace Janken
{
    public static class HandDictionary
    {
        public static readonly Dictionary<HandEnum, string> HandDict = new Dictionary<HandEnum, string>()
        {
            { HandEnum.STONE,  "グー" },
            { HandEnum.SCISSORS, "チョキ" },
            { HandEnum.PAPER,  "パー" }
        };
    }
}
