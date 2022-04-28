
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TakDotNet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO:
        //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseInMemoryDatabase("CleanArchitectureDb"));
        //}
        //else
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(
        //            configuration.GetConnectionString("DefaultConnection"),
        //            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        //}

        //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //services.AddScoped<IDomainEventService, DomainEventService>();

        //services
        //    .AddDefaultIdentity<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();

        //services.AddIdentityServer()
        //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();


        //services.AddAuthentication()
        //    .AddIdentityServerJwt();

        //services.AddAuthorization(options => 
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
