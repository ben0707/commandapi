
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using backendapi.Data.Dto.features.Command;
using backendapi.Data.Interfaces;
using backendapi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace backendapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController : ControllerBase
    {
        //Dependency Injection
        private readonly ICommander _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommander repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Get api/commands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllCommands()
        {
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(await _repository.GetAllCommands()));
        }

        //Get api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
        {

            var command = await _repository.GetCommandById(id);
            // check if command is not null
            if (command != null) return Ok(_mapper.Map<CommandReadDto>(command));

            // return NOTFOUND if not found
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandmodel = _mapper.Map<Command>(commandCreateDto);
            await _repository.CreateCommand(commandmodel);
            await _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandmodel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        //PUT api/commands/{id} -> to update a command resorce
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandCreateDto commandUpdateDto)
        {
            var commandmodelfromRepo = await _repository.GetCommandById(id);
            if (commandmodelfromRepo == null) return NotFound();
            _mapper.Map(commandUpdateDto, commandmodelfromRepo);
            await _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/commands/{id} -> to partially update a command resorce
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandmodelfromRepo = _repository.GetCommandById(id);
            if (commandmodelfromRepo == null) return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandmodelfromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandmodelfromRepo);
            _repository.SaveChanges();
            return NoContent();
        }


        //DELETE api/commands/{id} -> to delete a command resorce
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var commandmodelfromRepo = await _repository.GetCommandById(id);
            if (commandmodelfromRepo == null) return NotFound();

            await _repository.DeleteCommand(id);
            await _repository.SaveChanges();

            return NoContent();
        }

    }
}