using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Medium.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
