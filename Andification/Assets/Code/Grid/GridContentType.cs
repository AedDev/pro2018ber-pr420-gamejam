using System;

namespace Andification.Runtime.GridSystem
{
    [Flags]
    public enum GridContentType
    {
        Nothing = 0,    // 0000 0000 0000 0000
        Tower   = 1     // 0000 0000 0000 0001
    }
}