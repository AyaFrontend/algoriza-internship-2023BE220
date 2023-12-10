using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizeeta.Domain.Entities;
using Vizeeta.Repository.IRepository;

namespace Vizeeta.Presentation.Controllers
{
   
    public class DashboardController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("numOfDoctors")]
        public async Task<IActionResult> NumOfDoctors()
        {
            IEnumerable<Doctor> doctors = await _unitOfWork.Repository<Doctor>().GetAllAsync();
            int numOfDoctors = doctors.Count();

            return Ok(numOfDoctors);
        }

        [HttpGet("numOfPatients")]
        public async Task<IActionResult> NumOfPatients()
        {
            IEnumerable<Patient> patients = await _unitOfWork.Repository<Patient>().GetAllAsync();
            int numOfPatients = patients.Count();

            return Ok(numOfPatients);
        }


        [HttpGet("numOfRequetss")]
        public async Task<IActionResult> NumOfRequests()
        {
            IEnumerable<Booking> requests = await _unitOfWork.Repository<Booking>().GetAllAsync();
            int numOfRequests = requests.Count();

            return Ok(numOfRequests);
        }

    }
}
