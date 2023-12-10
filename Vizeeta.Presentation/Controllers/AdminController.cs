using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Presentation.Dto;
using Vizeeta.Presentation.Errors;
using Vizeeta.Presentation.Helper;
using Vizeeta.Repository.IRepository;
using Vizeeta.Repository.Specification;
using Vizeeta.Service.IServices;

namespace Vizeeta.Presentation.Controllers
{

    public class AdminController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _user;
        private readonly IEmailService _mail;
        private readonly ITokenService _token;
        public AdminController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> user,
            IEmailService mail, ITokenService token)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _user = user;
            _mail = mail;
            _token = token;
        }


        [HttpPost]
        public async Task<IActionResult> PostDoctor(DoctorDot doctorDto /*,IFormFile Imge*/)
        {
            if (!await IsExisting(doctorDto.Email))
            {


                Doctor doctor = _mapper.Map<DoctorDot, Doctor>(doctorDto);
                doctor.ImageUrl = "https://ptetutorials.com/images/user-profile.png"; //DocumentSettings.UploadFile(Image, "UploadFiles");
                _unitOfWork.Repository<Doctor>().Add(doctor);

                AppUser user = _mapper.Map<DoctorDot, AppUser>(doctorDto);
                user.DoctorId = doctor.Id;
                user.UserName = doctorDto.Email.Split("@")[0];

                var result = await _user.CreateAsync(user, doctorDto.Password);
                if (!result.Succeeded)
                    return BadRequest(new ApiResponse(400, "User do not save, Try again."));

                await _unitOfWork.Complete();

                await _mail.SendEmailAsync("ayamohamedabdelrahman868@gmail.com", doctorDto.Email, "Welcome to Vezzeta",
                    $"Your Password :{doctorDto.Password}");
            }


            return Ok();
        }

       [HttpPost("edit-user")]
       public async Task<IActionResult> EditDoctor([FromBody]DoctorDot doctorDto , [FromQuery]string Id)
        {
            Doctor doctor = _mapper.Map<DoctorDot, Doctor>(doctorDto);
            doctor.Id = Id;
            _unitOfWork.Repository<Doctor>().Update(doctor);
          

            AppUser user = _mapper.Map<DoctorDot, AppUser>(doctorDto);
            AppUserSpecification spec = new AppUserSpecification(doctor.Id);
            var oldUser = await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(spec);

            user.Id = oldUser.Id;
            var result = await _user.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse( 400 , "An error was occured"));


            await _unitOfWork.Complete();

            await _mail.SendEmailAsync("ayamohamedabdelrahman868@gmail.com", user.Email, "Vezeeta",
                "Your account has been Etited");
            return Ok();
    }

        [HttpPost("delete-doctor")]
        public async Task<IActionResult> DeleteDoctor(string Id)
        {
            BookingSpecification bSpec = new BookingSpecification(Id, 0);
            IEnumerable<Booking> bookings = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(bSpec);
            if (bookings.Count() > 0)
                return BadRequest(new ApiResponse(400, "You can not delete that doctor, He has booking list."));

            Doctor doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(Id);
      
           /* if (doctor.Booking.Count() != 0)
                return BadRequest(new ApiResponse(400, $"Dr {doctor.AppUser.FName} has booking, You can not delete him."));*/

            _unitOfWork.Repository<Doctor>().Delete(doctor);

            AppUserSpecification spec = new AppUserSpecification(doctor.Id);
            var user =  await _unitOfWork.Repository<AppUser>().GetEntityWithSpec(spec);

            var result = await _user.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "User do not Deleted, Try again."));

            await _unitOfWork.Complete();

            await _mail.SendEmailAsync("ayamohamedabdelrahman868@gmail.com", user.Email, "Vezeeta",
                "Your account has deleted");

            return Ok();
        }

        [HttpGet("get-doctor")]
        public async Task<IActionResult> GetDoctor([FromQuery] string Id)
        {
            Doctor doctor = await _unitOfWork.Repository<Doctor>().GetByIdAsync(Id);

            if (doctor == null)
                return NotFound(new ApiResponse(404, "Doctor not found"));


     

            return Ok(doctor);
        }

        [HttpGet("all-doctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            IEnumerable<Doctor> doctors = await _unitOfWork.Repository<Doctor>().GetAllAsync();

            if (doctors.Count()  ==0)
                return NotFound(new ApiResponse(404, "There is no doctors"));




            return Ok(doctors);
        }





        /// <summary>
        /// //Patient Actions
        /// </summary>
        /// <param name="Patient"></param>
        /// <returns></returns>
        /// 


        [HttpGet("all-patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            IEnumerable<Patient> patients = await _unitOfWork.Repository<Patient>().GetAllAsync();

            if (patients.Count() == 0)
                return NotFound(new ApiResponse(404, "There is no patients"));




            return Ok(patients);
        }

        private async Task<bool> IsExisting(string email)
        {
            return await _user.FindByIdAsync(email) != null;
        }

        [HttpGet("get-patient")]
        public async Task<IActionResult> GetPatient([FromQuery] string Id)
        {
            Patient patient = await _unitOfWork.Repository<Patient>().GetByIdAsync(Id);

            if (patient == null)
                return NotFound(new ApiResponse(404, "Patient not found"));

          
            return Ok(patient);
        }
    }
}
