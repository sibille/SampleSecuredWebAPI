﻿namespace SampleSecuredWebAPI.Core.Helpers
{
    public enum LoginResultType
    {
        Success,
        Unauthorized,
        CancelledByUser,
        NoNetworkAvailable,
        UnknownError
    }
}
