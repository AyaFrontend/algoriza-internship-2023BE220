using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Domain.Enums;
using Vizeeta.Presentation.Dto;
using Vizeeta.Presentation.Errors;
using Vizeeta.Repository.IRepository;
using Vizeeta.Repository.Specification;
using Vizeeta.Service.IServices;

namespace Vizeeta.Presentation.Controllers
{
    
    public class DoctorController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _user;
        private readonly SignInManager<AppUser> _signIn;
        private readonly ITokenService _token;
        public DoctorController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> user,
            ITokenService token)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _user = user;
           
            _token = token;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            AppUser user = new AppUser();
            try
            {
                user = await _user.FindByEmailAsync(loginDto.Email);
            }
            catch (Exception ex)
            { }



            if (user == null) return BadRequest("Email or password incorrect");

            var result = await _signIn.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result == null) return BadRequest("Email or password incorrect");

            var userDto = new UserDto()
            {
                Id = user.Id,
                DisplayName = $"{user.FName} {user.LName}",
                Email = user.Email,
                ProfilePicture = user.Doctor.ImageUrl,
                Token = await _token.CreateToken(user, _user)
            };



            
            return Ok(userDto);
        }

        [HttpPost("add-appoinment")]
        public async Task<IActionResult>  AddApoinment(AppoinmentDto appoinmentDto)
        {
            Appoinment appoinment = _mapper.Map<AppoinmentDto, Appoinment>(appoinmentDto);
            _unitOfWork.Repository<Appoinment>().Add(appoinment);
           await _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost("update-appoinment")]
        public async Task<IActionResult> UpdateApoinment(AppoinmentDto appoinmentDto)
        {

            Appoinment appoinment = _mapper.Map<AppoinmentDto, Appoinment>(appoinmentDto);
            _unitOfWork.Repository<Appoinment>().Update(appoinment);
            await _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost("delete-Time")]
        public async Task<IActionResult> DeleteTime([FromQuery]string  Id)
        {
            BookingSpecification spec = new BookingSpecification(Id);
            var bookingTime = await _unitOfWork.Repository<Booking>().GetEntityWithSpec(spec);
            if (bookingTime != null)
                return BadRequest(new ApiResponse(400, "This time already book, you can not delete it"));

            Times time = await _unitOfWork.Repository<Times>().GetByIdAsync(Id);
            _unitOfWork.Repository<Times>().Delete(time);
            await _unitOfWork.Complete(); ;

            return Ok();
        }

        [HttpPost("confirm-checkup")]
        public async Task<IActionResult> ConfirmCheckup([FromQuery] string bookingId)
        {
            Booking book = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
            if (book == null) return BadRequest(new ApiResponse(404, "NotFount"));

            book.BookingStatus = BookingStatus.Complete;

            _unitOfWork.Repository<Booking>().Update(book);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}
