using HMS.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Business.Services.Interfaces
{
    public interface IUserService
    {
        //Task<List<ApplicationUser>> GetAllAsync();
        //Task<Paginate<ApplicationUser>> GetAllPaginatedAsync(int page);
        Task Login(UserLoginVM userLoginVm);
        Task LogOut();

        Task CreateRollAsync();
        //Task<ApplicationUser> GetAsync(int id);
        Task CreateAsync(UserRegisterVM userRegisterVm);
        //Task Update(int id, UserPostVM userPostVM);
        //Task Remove(int id);
        //Task<int> getPageCount(int take);
    }
}
