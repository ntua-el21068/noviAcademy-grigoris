using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldRank.Api.DTOs;
using WorldRank.Application.Interfaces;
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

        public WalletsController(WalletService walletService)
        {
            _walletService = walletService;
        }

         [HttpGet("{playerId:int}")]
        public async Task<IActionResult> GetPlayerWallets(int playerId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _walletService.GetWalletsOfPlayer(playerId, cancellationToken);
                if(result==null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet( CreateWalletRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletService.AddWalletToPlayer(request.playerId, request.currency, request.balance, cancellationToken);
            return CreatedAtAction(nameof(GetPlayerWallets), new {playerId = wallet.PlayerId}, wallet);
        }

        [HttpPost("{playerId:int}/deposit")]
        public async Task<IActionResult> Deposit(int playerId, CreateDepositRequest request, CancellationToken cancellationToken)
        {
            try{
            await _walletService.DepositToWalletAsync(playerId, request.amount, request.currency, cancellationToken);
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
            return Ok();
        }

    }
}