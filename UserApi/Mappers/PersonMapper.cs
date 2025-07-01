using UserApi.Models;
using UserApi.Models.DTO;

namespace UserApi.Mappers;

public static class PersonMapper
{
    public static Person ToPerson(this PersonCreateRequest personCreateRequest)
    {
        return new Person
        {
            Name = personCreateRequest.Name,
            Email = personCreateRequest.Email,
            Active = true
        };
    }
    
    public static Person ToPerson(this PersonUpdateRequest personUpdateRequest, int id)
    {
        return new Person
        {
            Id = id,
            Name = personUpdateRequest.Name,
            Email = personUpdateRequest.Email
        };
    }
    
    public static PersonResponse ToPersonResponse(this Person person)
    {
        return new PersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Email = person.Email
        };
    }
}