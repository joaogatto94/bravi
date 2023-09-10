using api.Dtos;
using api.Repositories.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService personService;
        private readonly IContactService contactService;

    public PersonsController(IPersonService personService, IContactService contactService)
    {
        this.contactService = contactService;
        this.personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get()
    {
        return Ok(await personService.GetAll());
    }
    
    [HttpGet("{id}/contacts")]
    public async Task<ActionResult<List<Contact>>> GetContactsByPerson(int id)
    {
        return Ok(await contactService.GetByPerson(id));
    }
    
    [HttpPost]
    public async Task<ActionResult> Post(CreatePersonDto model)
    {
        await personService.Add(model);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, UpdatePersonDto model)
    {
        await personService.Update(id, model);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await personService.Remove(id);
        return Ok();
    }
}
