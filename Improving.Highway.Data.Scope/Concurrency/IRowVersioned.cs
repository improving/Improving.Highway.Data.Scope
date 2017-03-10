﻿namespace Improving.Highway.Data.Scope.Concurrency
{
    using System;

    public interface IRowVersioned
    {
        byte[]   RowVersion { get; set; }

        DateTime Modified   { get; set; }
    }
}