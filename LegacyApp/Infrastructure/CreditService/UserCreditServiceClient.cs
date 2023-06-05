using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace LegacyApp
{
    [DataContract]
    public class GetCreditLimitRequest
    {
        [DataMember(Order = 1)]
        public string Firstname { get; set; }

        [DataMember(Order = 2)]
        public string Surname { get; set; }

        [DataMember(Order = 3)]
        public DateTime DateOfBirth { get; set; }
    }

    [DataContract]
    public class GetCreditLimitResponse
    {
        [DataMember(Order = 1)]
        public int Result { get; set; }
    }

    [ServiceContract]
    public interface IUserCreditService
    {
        [OperationContract]
        ValueTask<GetCreditLimitResponse> GetCreditLimit(GetCreditLimitRequest request);
    }

    internal class UserCreditServiceClient : IUserCreditServiceClient
    {
        public async ValueTask<int> GetCreditLimit(string firstname, string surname, DateTime dateOfBirth)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;
            using var channel = GrpcChannel.ForAddress("http://totally-real-service.com");
            var userCreditService = channel.CreateGrpcService<IUserCreditService>();
            var response = await userCreditService.GetCreditLimit(new GetCreditLimitRequest
            {
                Firstname = firstname,
                Surname = surname,
                DateOfBirth = dateOfBirth
            });

            return response.Result;
        }
    }
}