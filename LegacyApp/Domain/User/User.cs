using System;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class User
    {
        private CreditLimit creditLimit = default;

        // todo: Primitive Obsession (names, email maybe dob) or it is not KISS enough
        // validation could be done so but..
        // as well should we use constructor for Firstname, Surname and DateOfBirth ?
        // as well Annemic classes... is it about simplicity
        // so imho it might depends on a matter of taste or objective requirements to select right proportion flex/strict for dom
        // other said it should be as strict as possible
        // I wonder what do you think

        public string Firstname { get; init; }
        public string Surname { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string EmailAddress { get; set; }
        public Client Client { get; private set; }

        public int Age
        {
            get
            {
                var now = DateTime.Now;
                return (now.Month < DateOfBirth.Month || (now.Month == DateOfBirth.Month && now.Day < DateOfBirth.Day))
                    ? now.Year - DateOfBirth.Year -1
                    : now.Year - DateOfBirth.Year;
            }            
        }

        public bool HasCreditLimit => creditLimit.HasLimit;
        public int CreditLimit => creditLimit;

        public void SetCreditLimit(CreditLimit initialLimit, Client client)
        {
            Client = client;
            creditLimit = Client.CalculateLimit(initialLimit);
        }
    }
}