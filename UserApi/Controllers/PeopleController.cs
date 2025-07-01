using Microsoft.AspNetCore.Mvc;
using UserApi.Mappers;
using UserApi.Models.DTO;
using UserApi.Repository;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPersonRepository _repository;

    public PeopleController(IPersonRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonCreateRequest personCreateRequest)
    {
        if (personCreateRequest == null)
            return BadRequest("Person data is null.");

        var person = personCreateRequest.ToPerson();

        try
        {
            var personCreated = await _repository.CreatePersonAsync(person);
            return CreatedAtAction(nameof(GetPersonById), new { id = personCreated.Id }, personCreated);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id:int}", Name = "GetPersonById")]
    public async Task<IActionResult> GetPersonById(int id)
    {
        try
        {
            var person = await _repository.GetPersonByIdAsync(id);

            if (person == null)
                return NotFound(new { Message = $"Person not found with id {id}" });

            return Ok(person.ToPersonResponse());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonUpdateRequest personUpdateRequest)
    {
        try
        {
            var existingPerson = await _repository.GetPersonByIdAsync(id);

            if (existingPerson == null)
                return NotFound(new { Message = $"Person not found with id {id}" });

            var personToUpdate = personUpdateRequest.ToPerson(id);

            await _repository.UpdatePersonAsync(personToUpdate);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        try
        {
            var existingPerson = await _repository.GetPersonByIdAsync(id);

            if (existingPerson == null)
                return NotFound(new { Message = $"Person not found with id {id}" });

            await _repository.DeletePersonAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        try
        {
            var people = await _repository.GetAllPersonsAsync();

            var peopleResponse = people.Select(p => p.ToPersonResponse());
            
            return Ok(peopleResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}