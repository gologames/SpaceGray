using SpaceGray.Core.TreeMap;
using System;
using System.Collections.Generic;

namespace SpaceGray.Core.Error;

public abstract class ErrorNode : ITreeMapNode
{
    public abstract ErrorNode Parent { get; }
    public virtual IErrorReport Report  => throw new NotImplementedException();
    public abstract string Text { get; }
    public abstract ErrorSeverity Severity { get; }
    public abstract int Depth { get; }
    public abstract long Size { get; }
    public abstract bool HasContent { get; }

    public virtual (IEnumerable<ErrorNode>, long) GetSortedChildren() =>
        throw new NotImplementedException();
}
