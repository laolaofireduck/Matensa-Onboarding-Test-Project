﻿namespace Ewallet.SharedKernel;

public abstract class AggregateRootId<TId> : ValueObject
{
    public abstract TId Value { get; protected set; }
}