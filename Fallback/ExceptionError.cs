using LanguageExt.Common;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fallback;
public record ExceptionError : Error
{
    public ExceptionError(Exception exception) : base(exception) 
    {
        InnerException = exception;
    }

    public Exception InnerException { get; init; }

    [DataMember]
    public override string Message => InnerException.Message;

    public override string ToString() => Message;

    public override ErrorException ToErrorException() => new ExceptionalException(InnerException);

    [Pure]
    public override bool Is<E>() => InnerException is E;

    public override bool IsExceptional => true;

    public override bool IsExpected => false;
}
