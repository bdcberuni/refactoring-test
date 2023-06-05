namespace LegacyApp;

public interface IUserCreditServiceClient
{
    int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
    ValueTask<int> GetCreditLimitAsync(string firstname, string surname, DateTime dateOfBirth);
}