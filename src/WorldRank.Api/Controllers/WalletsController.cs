using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorldRank.Api.DTOs;
using WorldRank.Application.Commands.Wallets;
using WorldRank.Application.Interfaces;
using WorldRank.Application.Queries.Wallets;
using WorldRank.Application.Services;
using WorldRank.Domain.Entities;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly WalletService _walletService;
        private readonly IMediator _mediator;

        public WalletsController(WalletService walletService, IMediator mediator)
        {
            _walletService = walletService;
            _mediator = mediator;
        }

         [HttpGet("{playerId:int}")]
        public async Task<IActionResult> GetPlayerWallets(int playerId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new GetWalletsByPlayerIdQuery(playerId), cancellationToken);

                return Ok(result);
            }
            catch (PlayerNotFoundException ex) { return StatusCode(404, ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
            

        }

        [HttpPost]
        public async Task<IActionResult> AddWallet( CreateWalletRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var id = await _mediator.Send(new CreateWalletCommand(request.playerId, request.currency, request.balance), cancellationToken);
                return Created($"/wallets/{id}", new { id });
            }
            catch (PlayerNotFoundException ex) { return StatusCode(404, ex.Message); }
            catch (DuplicateWalletException ex) { return StatusCode(409, ex.Message); }
            catch (WalletException ex) { return StatusCode(400, ex.Message); }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpPost("{playerId:int}/deposit")]
        public async Task<IActionResult> Deposit(int playerId, CreateDepositRequest request, CancellationToken cancellationToken)
        {
            decimal newBalance;
            try{
               newBalance = await _mediator.Send(new DepositToWalletCommand(playerId, request.amount, request.currency), cancellationToken);
            }
            catch (WalletNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (PlayerNotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (WalletException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(new { balance = newBalance }); ;
        }

    }
}