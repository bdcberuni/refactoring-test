using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LegacyApp
{
    public record Client
    {
        protected Client(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public ClientStatus ClientStatus { get; set; }

        public virtual CreditLimit CalculateLimit(int limit)
        {
            return new(limit);
        }

        public static Client Create(int id, string name)
        {
            var type = Assembly
                .GetAssembly(typeof(Client))?.GetTypes()
                .FirstOrDefault(type => typeof(Client).IsAssignableFrom(type) && type.Name == name);

            return type is not null
                ? (Client)type.GetConstructor(
                    BindingFlags.Public | BindingFlags.CreateInstance | BindingFlags.Instance, 
                    null,
                    new[] { typeof(int) },
                    null)?.Invoke(new object[] { id })!
                : new Client(id, name);
        }
    }

    public record ImportantClient(int Id) : Client(Id, nameof(ImportantClient))
    {
        public override CreditLimit CalculateLimit(int limit) => new(limit * 2);
    }

    public record VeryImportantClient(int Id) : Client(Id, nameof(VeryImportantClient))
    {
        public override CreditLimit CalculateLimit(int limit) => new(null);
    }
}