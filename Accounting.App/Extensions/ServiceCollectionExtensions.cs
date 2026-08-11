using Accounting.Dao.Implementations;
using Accounting.Dao.Interfaces;
using Accounting.Service.Implementations;
using Accounting.Service.Interfaces;
using Accounting.Service.Validators.JournalEntry;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IJournalEntryService, JournalEntryService>();
            services.AddScoped<IJournalEntryDao, JournalEntryDao>();
            services.AddScoped<IAccountDao, AccountDao>();

            services.AddValidatorsFromAssembly(typeof(JournalEntryRequestValidator).Assembly);

            return services;
        }
    }
}