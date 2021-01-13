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
        private readonly IRequestClient<UtilizeCreditRequested> requestClient;
        private readonly IMapper mapper;

        public CreditController(ILogger<CreditController> logger, IClientFactory clientFactory, IRequestClient<UtilizeCreditRequested> requestClient, IMapper mapper)
        {
            _logger = logger;
            this.clientFactory = clientFactory;
            this.requestClient = requestClient;
            this.mapper = mapper;
        }

        [HttpPost("UtilizeCredit")]
        public async Task<IActionResult> UtilizeCredit([FromBody]UtilizeCreditModel credit)
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


        [HttpPost("UtilizeCredit2")]
        public async Task<IActionResult> UtilizeCredit2([FromBody]UtilizeCreditModel credit)
        {
            try
            {
                var createCredit = mapper.Map<CreateCreditDTO>(credit.CreateCredit);
                var bonusPoints = mapper.Map<BonusPointsDTO>(credit.BonusPoints);
                bonusPoints.CreditId = createCredit.CreditId;
                var refinanceCredit = mapper.Map<RefinanceCreditDTO>(credit.RefinanceCredit);
                refinanceCredit.ParentCreditId = createCredit.CreditId;

                var response = await requestClient.GetResponse<UtilizeCreditCompleted, UtilizeCreditFaulted>(new
                {
                    CreateCredit = createCredit,
                    BonusPoints = bonusPoints,
                    RefinanceCredit = refinanceCredit
                });

                return response switch
                {
                    (_, UtilizeCreditCompleted completed) => Ok(new
                    {
                       completed.CreditId
                    }),
                    (_, UtilizeCreditFaulted faulted) => BadRequest(new
                    {
                       faulted.CreditId
                    }),
                    _ => BadRequest()
                };
            }
            catch (RequestTimeoutException)
            {
                return Accepted(new
                {
                });
            }
        }
    }
}
