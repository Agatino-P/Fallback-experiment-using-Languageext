using LanguageExt;
using LanguageExt.Common;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Fallback;
internal class NewTypes
{
    public void CheckConversions()
    {
        Age age = new(10);
        int twice = age.Value * 2;

        var v1 = AgeParam(new Age(2));

    }

    public int AgeParam(Age age)
    {
        return age.Value;
    }

    private CustomError ReturnCustomError(string message)
    {
        var ce = CustomError.New("a message");
        return new CustomError(message);
    }


    private Either<Error, Unit> ReturnDifferentErrors(bool flag)
    {
        CustomExpected c1 = new CustomExpected("Custom");

        Error c2 = CustomExpected.New(new Exception("An exception");

        CustomExceptional c3 = CustomExceptional.From(new Exception("an exception"));

        return flag ? c1 : c3;
    }
}

public record Age(int Value)
;


public record CustomExpected(string Message) : Expected(Message, 0, Prelude.None);

public record AnotherError(string Message) : Expected(Message, 0, Prelude.None);

public record CustomExceptional : Error
{
    public new Exception Exception { get; init; }

    public CustomExceptional(Exception exception) => Exception = exception;

    public static CustomExceptional From(Exception exception) => new(exception);

    public override string Message => Exception.Message;
    public override bool IsExceptional => true;
    public override bool IsExpected => false;

    public override bool Is<E>() => Exception is E;

    public override ErrorException ToErrorException() => Error.New(Exception).ToErrorException();

}



