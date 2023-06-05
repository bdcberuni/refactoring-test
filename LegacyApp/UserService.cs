using System;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IUserCreditServiceClient userCreditService;
        private readonly IUserRepository userDal;
        private readonly IClientRepository clientDal;

        // todo: legacy cleanup
        public UserService() : this(
            new UserCreditServiceClient(), 
            new UserRepository(), 
            new ClientRepository())
        {
        }

        public UserService(
                IUserCreditServiceClient userCreditServiceClient,
                IUserRepository userRepository,
                IClientRepository clientRepository
            )
        {
            this.userCreditService = userCreditServiceClient;
            this.userDal = userRepository;
            this.clientDal = clientRepository;
        }

        // todo: legacy cleanup
        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            return Task.Run(async () => await AddUser(
                new User
                {
                    DateOfBirth = dateOfBirth,
                    EmailAddress = email,
                    Firstname = firname,
                    Surname = surname
                }, clientId))
                .GetAwaiter()
                .GetResult();
        }

        public async Task<bool> AddUser(User user, int clientId)
        {
            // todo: consider FluentValidation..?
            return ValidateUser(user)
                && await SetCreditLimit(user, clientId)
                && userDal.SaveUser(user);
        }

        protected async Task<bool> SetCreditLimit(User user, int clientId)
        {
            if (clientDal.GetById(clientId) is not Client client)
            {
                return false;
            }

            var creditLimit = await userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
            user.SetCreditLimit(creditLimit, client);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }

        protected static bool ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Firstname) || string.IsNullOrEmpty(user.Surname))
            {
                return false;
            }

            // todo: is it possible to have null in EmailAddress
            if (user.EmailAddress.Contains('@') && !user.EmailAddress.Contains('.'))
            {
                return false;
            }            

            if (user.Age < 21)
            {
                return false;
            }

            return true;
        }
    }
}