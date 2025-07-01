using Microsoft.AspNetCore.Mvc;
using UserApi.Models.DTO;
using UserApi.Services;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] PersonCreateRequest personCreateRequest)
    {
        if (personCreateRequest == null)
            return BadRequest("Person data is null.");

        try
        {
            var createdId = await _personService.CreatePersonAsync(personCreateRequest);
            return Created(string.Empty, new { Id = createdId });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(422, ex.Message);
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
            var person = await _personService.GetPersonByIdAsync(id);

            return person is null
                ? NotFound(new { Message = $"Person not found with id {id}" })
                : Ok(person);
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
            var existingPerson = await _personService.UpdatePersonAsync(id, personUpdateRequest);

            if (!existingPerson)
                return NotFound(new { Message = $"Person not found with id {id}" });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(422, ex.Message);
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
            var existingPerson = await _personService.DeletePersonAsync(id);

            if (!existingPerson)
                return NotFound(new { Message = $"Person not found with id {id}" });

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
            var people = await _personService.GetAllPeopleAsync();

            return Ok(people);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}