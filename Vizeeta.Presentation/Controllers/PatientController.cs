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
using Vizeeta.Domain.Enums;
using Vizeeta.Presentation.Dto;
using Vizeeta.Presentation.Errors;
using Vizeeta.Presentation.Helper;
using Vizeeta.Repository.IRepository;
using Vizeeta.Repository.Specification;
using Vizeeta.Service.IServices;

namespace Vizeeta.Presentation.Controllers
{

    public class PatientController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _user;
        private readonly IEmailService _mail;
        private readonly ITokenService _token;
        public PatientController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> user,
            IEmailService mail, ITokenService token)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _user = user;
            _mail = mail;
            _token = token;

        }

        [HttpGet("all-doctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            IEnumerable<Doctor> doctors = await _unitOfWork.Repository<Doctor>().GetAllAsync();

            if (doctors.Count() == 0)
                return NotFound(new ApiResponse(404, "There is no doctors"));

           return Ok(doctors);
        }


        [HttpPost("booking")]
        public async Task<IActionResult> AddBook(BookingDto bookingDto)
        {
            Booking book = _mapper.Map<BookingDto, Booking>(bookingDto);
            if (book == null) return BadRequest();

            BookingSpecification spec = new BookingSpecification(book.PatientId, "Complete");
            IEnumerable<Booking> completedBooking = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);

            if(completedBooking.Count()< 5)
            {
                book.CuponeId = null;
            }
            _unitOfWork.Repository<Booking>().Add(book);
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpPost("cancel-booking")]
        public async Task<IActionResult> CancelBook([FromQuery]string bookingId)
        {
            Booking book = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
            if (book == null) return BadRequest(new ApiResponse(404,"NotFount"));

            book.BookingStatus = BookingStatus.Cancelled;

            _unitOfWork.Repository<Booking>().Update(book);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}