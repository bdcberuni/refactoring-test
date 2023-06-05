namespace LegacyApp;

internal class UserRepository : IUserRepository
{
    public bool SaveUser(User user)
    {
        try
        {
            UserDataAccess.AddUser(user);
            return true;
        }
        catch
        {
            return false;
        }  
    }
}
