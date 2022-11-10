using FrontEnd.Data;
namespace FrontEnd.Services;

public interface IAdminService
{
    Task<bool> AllowAdminUserCreationAsync();
}
