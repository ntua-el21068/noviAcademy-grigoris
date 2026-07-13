using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldRank.Api.DTOs;
using WorldRank.Application.Interfaces;
using WorldRank.Application.Services;
using WorldRank.Domain.Entities;

namespace WorldRank.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayersController(PlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpGet]
        public async Task< IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try{
            var players = await _playerService.GetAllPlayers(cancellationToken);
            return Ok(players);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{playerId:int}")]
        public async Task<IActionResult> GetById(int playerId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _playerService.GetPlayerById(playerId, cancellationToken);
                if(result==null) return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(CreatePlayerRequest request, CancellationToken cancellationToken)
        {
            var player = await _playerService.AddPlayer(request.Name, request.Score, cancellationToken);
            return CreatedAtAction(nameof(GetById), new {playerId = player.Id}, player);
        }


        
    }
}