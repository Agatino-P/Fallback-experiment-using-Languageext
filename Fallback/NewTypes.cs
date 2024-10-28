using LanguageExt;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
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
        ErrorException a = CustomError.New("Custom").ToErrorException();
        var b = new CustomError("Custom").ToErrorException();

        return flag ? CustomError.New("Custom") : AnotherError.New("Another");
    }
}

public record Age(int Value)
;


public record CustomError(string Message): Expected(Message, 0, Prelude.None);

public record AnotherError(string Message) : Expected(Message, 0, Prelude.None);
