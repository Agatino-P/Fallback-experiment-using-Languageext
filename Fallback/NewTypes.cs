using System;
using System.Collections.Generic;
using System.Linq;
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
}

public record Age(int Value)
;
