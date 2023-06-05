namespace LegacyApp.Test;

public class UserServiceTest
{
    const int SUFFICIENT_LIMIT = 500;
    const int INSUFFICIENT_LIMIT = 100;


    [Theory]
    [InlineData(true, "Any", true, SUFFICIENT_LIMIT, SUFFICIENT_LIMIT)]
    [InlineData(true, nameof(ImportantClient), true, SUFFICIENT_LIMIT, SUFFICIENT_LIMIT * 2)]
    [InlineData(true, nameof(VeryImportantClient), false, SUFFICIENT_LIMIT, 0)]

    [InlineData(false, "Any", true, INSUFFICIENT_LIMIT, INSUFFICIENT_LIMIT)]
    [InlineData(false, nameof(ImportantClient), true, INSUFFICIENT_LIMIT, INSUFFICIENT_LIMIT * 2)]
    public async void userService_should_addUser(bool expected, string client, bool hasCreditLimit, int initialLimit, int appliedLimit)
    {
        //Arrange
        var clientId = 4;
        var user = new User 
        { 
            Firstname = "John",
            Surname = "Doe",
            DateOfBirth = new DateTime(1993, 1, 1),
            EmailAddress = "John.doe@gmail.com"
        };

        var userCreditServiceClient = new Mock<IUserCreditServiceClient>();
        userCreditServiceClient
            .Setup(c => c.GetCreditLimitAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
            .Returns(ValueTask.FromResult(initialLimit));

        var userDal = new Mock<IUserRepository>();
        userDal.Setup(r => r.SaveUser(It.IsAny<User>())).Returns(true);

        var clientDal = new Mock<IClientRepository>();
        clientDal.Setup(r => r.GetById(It.IsAny<int>())).Returns(Client.Create(clientId, client));

        //Act
        var service = new UserService(userCreditServiceClient.Object, userDal.Object, clientDal.Object);
        var actual = await service.AddUser(user, clientId);

        //Assert
        actual.Should().Be(expected);
        user.HasCreditLimit.Should().Be(hasCreditLimit);
        user.CreditLimit.Should().Be(appliedLimit);
    }
}