using System;

namespace Bekk.Pact.Common.Contracts
{
    [Flags]
    public enum ValidationTypes
    {
        None = 0,
        StatusCode = 1 << 0,
        Headers = 1 << 1,
        Body = 1 << 2
    }
}