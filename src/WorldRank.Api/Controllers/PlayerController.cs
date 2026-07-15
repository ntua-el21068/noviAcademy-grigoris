using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorldRank.Api.DTOs;
using WorldRank.Application.Commands.Players;
using WorldRank.Application.Caching;
using WorldRank.Application.Queries.Players;
using WorldRank.Application.Services;
using WorldRank.Domain.Entities;

namespace WorldRank.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {
        //private readonly PlayerService _playerService;
        private readonly IMediator _mediator;

        public PlayersController( IMediator mediator)
        {
            //_playerService = playerService;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task< IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try{
                var players = await _mediator.Send(new GetAllPlayersQuery(), cancellationToken);
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
                var result = await _mediator.Send(new GetPlayerByIdQuery(playerId), cancellationToken);
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
            var id = await _mediator.Send(new CreatePlayerCommand(request.Name, request.Score), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { playerId = id }, new {id});
        }


        
    }
}