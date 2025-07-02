using System.Runtime.InteropServices;
using UserApi.Models;
using UserApi.Models.DTO;

namespace UserApi.Mappers;

public static class PersonMapper
{
    public static void ToUpdate(this Person person, PersonUpdateRequest personUpdateRequest)
    {
        person.Name = personUpdateRequest.Name;
        person.Email = personUpdateRequest.Email;
    }

    public static Person ToPerson(this PersonCreateRequest personCreateRequest)
    {
        return new Person
        {
            Name = personCreateRequest.Name,
            Email = personCreateRequest.Email
        };
    }

    public static PersonResponse ToPersonResponse(this Person person)
    {
        var timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? "E. South America Standard Time"
            : "America/Sao_Paulo";

        var brazilTime = TimeZoneInfo.ConvertTimeFromUtc(
            person.CreatedAt,
            TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)
        );

        return new PersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Email = person.Email,
            CreatedAt = brazilTime.ToString("dd-MM-yyyy HH:mm:ss")
        };
    }
}