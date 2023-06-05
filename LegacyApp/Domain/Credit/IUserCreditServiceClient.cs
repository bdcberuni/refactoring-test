namespace LegacyApp;

public interface IUserCreditServiceClient
{
    ValueTask<int> GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
}