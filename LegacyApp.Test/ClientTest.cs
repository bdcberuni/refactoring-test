using FluentAssertions;
using Moq;

namespace LegacyApp.Test;

public class ClientTest
{
    [Theory]
    [InlineData("Any", typeof(Client))]
    [InlineData(nameof(ImportantClient), typeof(ImportantClient))]
    [InlineData(nameof(VeryImportantClient), typeof(VeryImportantClient))]
    public void client_create_should_redurn_apropriate_client_type_by_its_name(string name, Type type)
    {
        //Arrange      


        //Act
        var expected = Client.Create(1, name);

        //Assert
        expected.Should().BeOfType(type);
    }

    [Fact]
    public void default_credit_limit_should_be_eq_initial()
    {
        //Arrange      
        int expected = 100;

        //Act
        var client = Client.Create(1, "Any");
        var actual = client.CalculateLimit(expected);

        //Assert
        actual.HasLimit.Should().Be(true);
        actual.Limit.Should().Be((uint)expected);
        expected.Should().Be(actual);
    }

    [Fact]
    public void credit_limit_for_ImportantClient_should_be_doubled()
    {
        //Arrange      
        int initialLimit = 100;
        int expected = initialLimit * 2;

        //Act
        var client = Client.Create(1, nameof(ImportantClient));
        var actual = client.CalculateLimit(initialLimit);

        //Assert
        actual.HasLimit.Should().Be(true);
        actual.Limit.Should().Be((uint)expected);
        expected.Should().Be(actual);
    }

    [Fact]
    public void there_should_not_be_credit_limit_for_VeryImportantClient()
    {
        //Arrange      
        int initialLimit = 100;

        //Act
        var client = Client.Create(1, nameof(VeryImportantClient));
        var actual = client.CalculateLimit(initialLimit);

        //Assert
        actual.Limit.Should().Be(0);
        actual.HasLimit.Should().Be(false);
    }
}