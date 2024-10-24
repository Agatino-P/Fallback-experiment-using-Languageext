using LanguageExt;
using static LanguageExt.Prelude;
using FluentAssertions;

namespace Fallback;

public class FallbackTests
{
    [Fact]
    public void ShouldUseFallBackValue()
    {
        Func<string> FirstThrower = () => throw new Exception();
        Func<string> SecondThrower = () => throw new Exception();
        Func<string> Fallback = () => ("fallback");

        string s = FallBackStrategy([FirstThrower, SecondThrower], Fallback);
        s.Should().Be("fallback");
    }

    [Fact]
    public void ShouldShortcutEarly()
    {
        Func<string> FirstThrower= () => throw new Exception();
        Func<string> SecondThrower=() => "I didn't trow!";
        Func<string> Fallback= () => "fallback";

        string s = FallBackStrategy([FirstThrower, SecondThrower], Fallback);
        s.Should().Be("I didn't trow!");
    }

    [Fact]
    public void ShouldShortcutEarlyUntil()
    {
        Func<string> FirstThrower = () => throw new Exception();
        Func<string> SecondThrower = () => "I didn't trow!";
        Func<string> Fallback = () => "fallback";

        string s = FallBackStrategyUntil([FirstThrower, SecondThrower], Fallback);
        s.Should().Be("I didn't trow!");
    }

    private static T FallBackStrategy<T>(IEnumerable<Func<T>> throwers, Func<T> fallback)
    {
        T t=Seq(throwers)
            .FoldWhile(Option<T>.None, (state, fun) => Try(fun).ToOption(), state => state.IsNone)
            .IfNone(fallback);
        return t;
    }

    private static T FallBackStrategyUntil<T>(IEnumerable<Func<T>> throwers, Func<T> fallback)
    {
        T t = Seq(throwers)
            .FoldUntil(Option<T>.None, (state, fun) => Try(fun).ToOption(), state => state.IsSome)
            .IfNone(fallback);
        return t;
    }
}