using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Presentation.Dto;
using Vizeeta.Presentation.Errors;
using Vizeeta.Presentation.Helper;
using Vizeeta.Repository.IRepository;
using Vizeeta.Repository.Specification;
using Vizeeta.Service.IService;
using Vizeeta.Service.IServices;

namespace Vizeeta.Presentation.Controllers
{

    public class PatientSettingsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _user;
        private readonly IEmailService _mail;
        private readonly ITokenService _token;
        private readonly IResponseCacheServices _cache;
        private readonly SignInManager<AppUser> _signIn;
        public PatientSettingsController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> user,
            IEmailService mail, ITokenService token , IResponseCacheServices cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _user = user;
            _mail = mail;
            _token = token;
            _cache = cache;
        }


        [HttpPost("register-Patient")]
        public async Task<IActionResult> Register(PatientDto patientData)
        {
            if (ModelState.IsValid)
            {
                if (await IsExisting(patientData.Email)) return BadRequest("That email aready exist");

                await _cache.CacheResponseAsync("registeredData", patientData, TimeSpan.FromMinutes(15));
                var vaidationCode = _mail.GenerateRandom4DigitsCode();
                await _cache.CacheResponseAsync(vaidationCode.ToString(), vaidationCode, TimeSpan.FromMinutes(15));


                await _mail.SendEmailAsync("ayamohamedabdelrahman868@gmail.com", patientData.Email, "Validation Code From Vezeeta", $"Your validation code is ( {vaidationCode} )");


                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("completeRegister")]
        public async Task<IActionResult> CompleteRegister(ValidationCodeDto code)
        {

            var cachedCode = await _cache.GetCachedResponse(code.ValidationCode);
            if (cachedCode == null) return NotFound(new ApiResponse(404, "Expired Date"));
            else if (cachedCode != code.ValidationCode) return BadRequest(new ApiResponse(400, "incorrect code"));

            var stringRegistredData = await _cache.GetCachedResponse("registeredData");

            if (string.IsNullOrEmpty(stringRegistredData)) return NotFound(new ApiResponse(400, "expired date"));
            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = false };
            var registredData = JsonSerializer.Deserialize<PatientDto>(stringRegistredData, option);
            Patient patient = _mapper.Map<PatientDto, Patient>(registredData);
            AppUser user = _mapper.Map<PatientDto,AppUser>(registredData);

            patient.ImageUrl= "https://ptetutorials.com/images/user-profile.png";
            user.UserName = registredData.Email.Split("@")[0];

            _unitOfWork.Repository<Patient>().Add(patient);

            var result = await _user.CreateAsync(user, registredData.Password);
            if (!result.Succeeded) return BadRequest(result);

           await _unitOfWork.Complete();
           
            return Ok();


        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _user.FindByEmailAsync(loginDto.Email);
            
            if (user == null) return BadRequest("Email or password incorrect");

            var result = await _signIn.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result == null) return BadRequest("Email or password incorrect");

            var userDto = new UserDto()
            {
                ProfilePicture = user.Patient.ImageUrl,
                Id = user.Patient.Id,
                Email = user.Email,
                DisplayName = $"{user.UserName}",
                Token = await _token.CreateToken(user, _user)
            };



           
            return Ok(userDto);
        }

        [HttpGet("currentUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _user.FindByEmailAsync(email);


            var userDto = new UserDto()
            {
                Email = user.Email,
                DisplayName = $"{user.FName} {user.LName}",
                Token = await _token.CreateToken(user, _user)
            };

            return Ok(userDto);
        }


        [HttpPost("forgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordDto data)
        {
            if (await IsExisting(data.Email))
            {
                var user = await _user.FindByEmailAsync(data.Email);
                var token = await _token.CreateToken(user, _user);
                string link = $"http://localhost:4200/change-password/{data.Email}/{token}";
                await _cache.CacheResponseAsync("changePasswordLink", link, TimeSpan.FromMinutes(5));
                await _mail.SendEmailAsync("ayamohamedabdelrahman868@gmail.com", data.Email, "Reset Your Password", "To change your password please click here " + link);
                return Ok(token);
            }
            return NotFound(new ApiResponse(404, "That email dose not exist"));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("changePassword")]

        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (await _cache.GetCachedResponse("changePasswordLink") == null) return BadRequest(new ApiResponse(404, "Link is expired, please try again"));

            var user = await _user.FindByEmailAsync(changePasswordDto.Email);
            if (user == null) return BadRequest(new ApiResponse(404, "This email not found"));
            user.Password = changePasswordDto.Password;
             _unitOfWork.Repository<AppUser>().Update(user);
            await _unitOfWork.Complete();
            // var result = await _user.ResetPasswordAsync(user, changePasswordDto.Token, changePasswordDto.Password);
            //if (!result.Succeeded) return BadRequest(new ApiResponse(400 , "Password faild to change"));

            return Ok(new ApiResponse(200, "password is changed correctly"));


        }




        private async Task<bool> IsExisting(string email)
        {
            return await _user.FindByIdAsync(email) != null;
        }
    }
}