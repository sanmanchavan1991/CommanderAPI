using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    //GET api/commands
    [Route("api/[Controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {

            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/command/5
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int Id)
        {
            var commandItem = _repository.GetCommandById(Id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            else
            {
                return NotFound();
            }
        }


        //POSt api /commands
        [HttpPost]
         public ActionResult<CommandCreateDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandItem = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandItem);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandItem);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);      
        }

        //POSt api /commands
        [HttpPut("{id}")]
         public ActionResult UpdateCommand(int id,CommandUpdateDto commandUpdateDto)
        {
            var commandItem = _repository.GetCommandById(id);
             if (commandItem == null)
            {
                 return NotFound();
              }
             _mapper.Map(commandUpdateDto,commandItem);
             _repository.UpdateCommand(commandItem);
             _repository.SaveChanges();
             return NoContent();
        }

        //POSt api /commands
        [HttpPatch("{id}")]
         public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandItem = _repository.GetCommandById(id);
             if (commandItem == null)
            {
                 return NotFound();
              }
             var commandToPatch = _mapper.Map<CommandUpdateDto>(commandItem);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandItem);

            _repository.UpdateCommand(commandItem);

            _repository.SaveChanges();

            return NoContent();
        }


         //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}