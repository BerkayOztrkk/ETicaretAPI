
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Services.Configurations;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Infrastructure.Enums;
using ETicaretAPI.Infrastructure.Services;
using ETicaretAPI.Infrastructure.Services.Configurations;
using ETicaretAPI.Infrastructure.Services.Storage;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
           serviceCollection.AddScoped<IStorageService,StorageService>();
           serviceCollection.AddScoped<ITokenHandler,TokenHandler>();
           serviceCollection.AddScoped<IMailService,MailService>();
           serviceCollection.AddScoped<IService,Service>();
           serviceCollection.AddScoped<IQRCodeService,QRCodeService>();

        }
        public static void AddStorage<T>(this IServiceCollection servicecollection) where T :class, IStorage 
        {
            servicecollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection servicecollection,StorageType storageType) 
        {
            switch (storageType)
            {
                case StorageType.Local:
                    servicecollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    break;
                case StorageType.AWS:
                    break;
                default:
                    servicecollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
