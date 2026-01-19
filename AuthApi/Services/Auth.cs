using AuthApi.Datas;
using AuthApi.Models;
using AuthApi.Services.Dtos;
using AuthApi.Services.IAuthService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AuthApi.Services
{
    public class Auth : IAuth
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly ITokenGenerator tokenGenerator;

        public Auth(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator)
        {
            _dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<object> AssignRole(string UserName, string roleName)
        {
            var user = await _dbContext.applicationUsers.FirstOrDefaultAsync(user => user.NormalizedUserName == UserName.ToUpper());

            if (user != null)
            {
                if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await userManager.AddToRoleAsync(user, roleName);

                return new ResponseDto(){ Value = user, Message = "Sikeres hozzárendelés." };
            }

            return "Sikertelen hozzárendelés";
        }

        public Task<object> DeleteUserData()
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetOneUserData(string userName)
        {
            var oneuser = await _dbContext.applicationUsers.FirstOrDefaultAsync(u => u.NormalizedUserName == userName.ToUpper());

            if (oneuser != null)
            {
                return new ResponseDto(){ Value = oneuser, Message = "Sikeres lekérés" };
            }

            return "Sikertelen lekérés";
        }

        public async Task<object> GetOneUserDataById(string id)
        {
            var oneUser = await _dbContext.applicationUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (oneUser != null)
            {
                return new ResponseDto(){ Value = oneUser, Message = "Sikeres lekérés"};
            }

            return "Sikertelen lekérés";


        }

        public async Task<object> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _dbContext.applicationUsers.FirstOrDefaultAsync(user => user.NormalizedUserName == loginRequestDto.UserName.ToUpper());

            bool isValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (isValid)
            {
                var roles = await userManager.GetRolesAsync(user);
                var jwtToken = tokenGenerator.GenerateToken(user, roles);

                return new LoginResponseDto(){ Value = user.UserName, Message = "Sikeres beléptetés.", Token = jwtToken };
            }

            return "Nem regisztrált.";
        }

        public async Task<object> Register(RegisterRequestDto registerRequestDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
                Birthdate = DateTime.Now,
            };

            var result = await userManager.CreateAsync(user, registerRequestDto.Password);

            if (result.Succeeded)
            {
                var userReturn = await _dbContext.applicationUsers.FirstOrDefaultAsync(user => user.UserName == registerRequestDto.UserName);

                return new ResponseDto(){ Value = userReturn.UserName, Message = "Sikeres regisztráció." };
            }

            return $"Sikertelen regisztráció \n{result.Errors.FirstOrDefault().Description}";
        }

        public async Task<object> UpdateUserData(RegisterRequestDto updateUserDto)
        {

            var user = await _dbContext.applicationUsers.FirstOrDefaultAsync(u => u.NormalizedUserName == updateUserDto.UserName.ToUpper());
            bool changedEmail = false;

            if (user != null) 
            {
                if (updateUserDto.Email != null)
                {
                    user.Email = updateUserDto.Email;
                    var update = await userManager.UpdateAsync(user);
                    if (update.Succeeded)
                    {
                        user.ModDate = DateTime.Now;
                        changedEmail = true;
                    }
                }

                if (updateUserDto.Password != null)
                {

                    var isRemovePassword = await userManager.RemovePasswordAsync(user);

                    if (isRemovePassword.Succeeded)
                    {
                        var isPasswordAdded = await userManager.AddPasswordAsync(user, updateUserDto.Password);

                        if (isPasswordAdded.Succeeded)
                        {


                            var updated = await userManager.UpdateAsync(user);

                            if (updated.Succeeded)
                            {
                                if (!changedEmail)
                                {
                                    user.ModDate = DateTime.Now;
                                }

                                return new ResponseDto() { Value = user, Message = "Sikeres módosítás" };
                            }
                        }

                    }

                }
            }

            if (changedEmail)
            {
                return new ResponseDto() { Value = user, Message = "Sikeres módosítás" };
            }


            return "Sikertelen módosítás";
        }
    }
}
