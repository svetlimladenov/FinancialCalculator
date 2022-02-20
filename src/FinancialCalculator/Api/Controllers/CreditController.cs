using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using Contracts;
using Infrastucture;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditController : ControllerBase
    {
        private readonly ILogger<CreditController> _logger;
        private readonly IClientFactory clientFactory;
        private readonly IRequestClient<UtilizeCreditRequested> utilizeRequstClient;
        private readonly IMapper mapper;

        public CreditController(ILogger<CreditController> logger, IClientFactory clientFactory, IRequestClient<UtilizeCreditRequested> requestClient, IMapper mapper)
        {
            _logger = logger;
            this.clientFactory = clientFactory;
            this.utilizeRequstClient = requestClient;
            this.mapper = mapper;
        }

        [HttpPost("UtilizeCredit")]
        public async Task<IActionResult> UtilizeCredit([FromBody] UtilizeCreditModel credit)
        {
            var createCredit = mapper.Map<CreateCreditDTO>(credit.CreateCredit);
            var bonusPoints = mapper.Map<BonusPointsDTO>(credit.BonusPoints);
            bonusPoints.CreditId = createCredit.CreditId;
            var refinanceCredit = mapper.Map<RefinanceCreditDTO>(credit.RefinanceCredit);
            refinanceCredit.ParentCreditId = createCredit.CreditId;

            var requestMessage = new
            {
                CreateCredit = createCredit,
                BonusPoints = bonusPoints,
                RefinanceCredit = refinanceCredit
            };

            using (var request = clientFactory.CreateRequestClient<UtilizeCreditRequested>().Create(requestMessage))
            {
                var (statusResponse, errorResponse) = await request.GetResponses<UtilizeCreditCompleted, UtilizeCreditFaulted>();

                if (statusResponse.IsCompletedSuccessfully)
                {
                    var data = await statusResponse;
                    return Ok(data);
                }
                else
                {
                    var data = await errorResponse;
                    return Ok(data);
                }
            }
        }

        [HttpPost("CalculateCredit")]
        public async Task<IActionResult> CalculateCredit(CalculateCreditModel model)
        {
            var request = clientFactory.CreateRequest<CalculateCreditRequested>(new { model.Amount, model.Interest, model.Period });

            var response = await request.GetResponse<CalculateCreditResponse>();

            return Ok(response);
        }

        [HttpPost("CalculateRefinanceCredit")]
        public async Task<IActionResult> CalculateRefinanceCredit(CalculateRefinanceModel model)
        {
            var request = clientFactory.CreateRequest<CalculateRefinanceRequested>(new { model.CreditSum, model.Interest, model.Months, model.PaymentsMade });

            var response = await request.GetResponse<RefinanceCreditResponse>();

            return Ok(response);
        }
    }
}
