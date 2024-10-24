using FluentAssertions;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Repository;
public class ReactOnRepositoryTests
{
    [Fact]
    public async Task ShouldExtractEntity()
    {
        Repo _sut = new();

        Either<ProblemDetails, Unit> actual =
            (await _sut.Get())
            .Match(o => o, ex => throw ex)
            .Match(
                entity => GoAhead(entity),
                new ProblemDetails { Title = "Entity not found" }
                );

        actual.Match(
            _ => { }
            ,
            _ => false.Should().BeTrue()
            );

    }

    [Fact]
    public async Task ShouldThrow()
    {
        Repo _sut = new();


        Func<Task<Either<ProblemDetails, Unit>>> f = async () =>
            (await _sut.Except())
            .Match(o => o, ex => throw ex)
            .Match(
                entity => GoAhead(entity),
                new ProblemDetails { Title = "Entity not found" }
                );

        await f.Should().ThrowAsync<Exception>();
    }

    private Either<ProblemDetails, Unit> GoAhead(Entity entity) => Unit.Default;

}


public class Repo
{
    public async Task<Either<Exception, Option<Entity>>> Get()
    {
        Either<Exception, Option<Entity>> retval = Either<Exception, Option<Entity>>.Right(Option<Entity>.Some(new Entity("An entity")));
        return retval;
    }
    public async Task<Either<Exception, Option<Entity>>> Except()
    {
        Either<Exception, Option<Entity>> retval = new Exception("Exceptional");

        return retval;
    }
}

public record Entity(string Name);