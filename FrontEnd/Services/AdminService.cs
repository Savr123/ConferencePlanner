using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FrontEnd.Services;

public class AdminService: IAdminService
{
    private readonly IServiceProvider _serviceProvider;

    private bool _adminExist;

    public AdminService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> AllowAdminUserCreationAsync()
    {
        if (_adminExist)
        {
            return false;
        }
        else
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

                if (await dbContext.Users.AnyAsync(user => user.isAdmin))
                {
                    // There are already admin users so disable admin creation
                    _adminExist = true;
                    return false;
                }

                // There are no admin users so enable admin creation
                return true;
            }
            
        }
    }
}
