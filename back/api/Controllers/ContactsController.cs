using api.Dtos;
using api.Repositories.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService contactService;

    public ContactsController(IContactService contactService)
    {
        this.contactService = contactService;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post(CreateContactDto model)
    {
        await contactService.Add(model);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, UpdateContactDto model)
    {
        await contactService.Update(id, model);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await contactService.Remove(id);
        return Ok();
    }
}
