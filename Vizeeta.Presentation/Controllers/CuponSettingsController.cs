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

    public class CuponSettingsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _user;
        private readonly IEmailService _mail;
        private readonly ITokenService _token;
        public CuponSettingsController(IUnitOfWork unitOfWork, IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        [HttpPost("add-cupon")]
        public async Task<IActionResult> AddCupon(CuponDto cuponDto)
        {
            Cupone cupon = _mapper.Map<CuponDto, Cupone>(cuponDto);
            _unitOfWork.Repository<Cupone>().Add(cupon);
            await _unitOfWork.Complete();

            return Ok();

        }

        [HttpPost("update-cupon")]
        public async Task<IActionResult> UpdateCupon(CuponDto cuponDto , string cuponId)
        {


            Cupone cupon = _mapper.Map<CuponDto, Cupone>(cuponDto);
            cupon.Id = cuponId;
            _unitOfWork.Repository<Cupone>().Update(cupon);
            await _unitOfWork.Complete();

            return Ok();

        }

        [HttpPost("delete-cupon")]
        public async Task<IActionResult> DeleteCupon( string cuponId)
        {


            Cupone cupon = await _unitOfWork.Repository<Cupone>().GetByIdAsync(cuponId);
            _unitOfWork.Repository<Cupone>().Delete(cupon);
            await _unitOfWork.Complete();

            return Ok();

        }

        [HttpPost("deactive-cupon")]
        public async Task<IActionResult> DeactiveCupon(string cuponId)
        {


            Cupone cupon = await _unitOfWork.Repository<Cupone>().GetByIdAsync(cuponId);
            cupon.DiscoundType = Domain.Enums.CuponType.Deactive;
            _unitOfWork.Repository<Cupone>().Update(cupon);
            await _unitOfWork.Complete();

            return Ok();

        }
    }
}