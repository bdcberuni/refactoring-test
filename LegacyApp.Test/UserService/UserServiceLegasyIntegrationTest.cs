namespace LegacyApp.Test;

public class UserServiceLegasyIntegrationTest
{
    const int DEF_LIMIT = 500;


    [Theory(Skip = "todo:")]
    [InlineData("Any", true, DEF_LIMIT)]
    [InlineData(nameof(ImportantClient), true, DEF_LIMIT *2)]
    [InlineData(nameof(VeryImportantClient), false, 0)]
    public async void userService_should_addUser(string client, bool hasCreditLimit, int limit)
    {
        
    }
}