using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using MockQueryable;
using Moq;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.User.Queries.GetUsersQuery;
using RO.DevTest.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Tests.Unit.Application.Features.User.Queries;

internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;
    internal TestAsyncQueryProvider(IQueryProvider inner) => _inner = inner;
    public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);
    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        => new TestAsyncEnumerable<TElement>(expression);
    public object Execute(Expression expression) => _inner.Execute(expression);
    public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);
    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        => Execute<TResult>(expression);
}

internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
    public TestAsyncEnumerable(Expression expression) : base(expression) { }
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        => new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
}

internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;
    public TestAsyncEnumerator(IEnumerator<T> inner) => _inner = inner;
    public T Current => _inner.Current;
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    public ValueTask<bool> MoveNextAsync() => ValueTask.FromResult(_inner.MoveNext());
}



public class GetUsersQueryHandlerTests
{
    private Mock<UserManager<Domain.Entities.User>> GetMockUserManager(List<Domain.Entities.User> users)
    {
        var store = new Mock<IUserStore<Domain.Entities.User>>();
        var userManager = new Mock<UserManager<Domain.Entities.User>>(store.Object, null, null, null, null, null, null, null, null);
        var usersQueryable = users.AsQueryable().BuildMock();
        userManager.Setup(r => r.Users).Returns(usersQueryable);

        return userManager;
    }


    [Fact]
    public async Task Handle_ValidQuery_ReturnsPaginatedUsers()
    {
        // Arrange
        var users = new List<Domain.Entities.User>
            {
                new() { Id = "1", Name = "Alice", Email = "alice@example.com" },
                new() { Id = "2", Name = "Bob",   Email = "bob@example.com" },
                new() { Id = "3", Name = "Carol", Email = "carol@example.com" }
            };
        var queryableUsers = new TestAsyncEnumerable<Domain.Entities.User>(users);

        var mockUserManager = GetMockUserManager(users);


        var handler = new GetUsersQueryHandler(mockUserManager.Object, true);
        var request = new GetUsersQuery
        {
            PageNumber = 1,
            PageSize = 2,
            SearchTerm = null,
            SortBy = "name",
            IsAscending = true
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.Users.Count());
        Assert.Equal(2, result.TotalPages);
        // Verifica ordenação
        Assert.Equal("Alice", result.Users[0].Name);
        Assert.Equal("Bob", result.Users[1].Name);
    }
}