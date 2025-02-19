using Microsoft.AspNetCore.Mvc;

namespace FamilyLibraryBackend.Controllers;

[ApiController]
[Route("api/families")]
public class FamiliesController : ControllerBase
{
    private readonly FamilyService _familyService;

    public FamiliesController(FamilyService familyService)
    {
        _familyService = familyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetFamilies()
    {
        var families = await _familyService.GetAllFamiliesAsync();
        return Ok(families);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFamily(string id)
    {
        var families = await _familyService.GetFamilyAsync(id);
        return Ok(families);
    }

    [HttpPost]
    public async Task<IActionResult> AddFamily([FromBody] FamilyMetadata family)
    {
        await _familyService.AddFamilyAsync(family);
        return CreatedAtAction(nameof(GetFamilies), new { id = family.Id }, family);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFamily(string id)
    {
        try
        {
            var result = await _familyService.DeleteFamilyAsync(id);
            if (!result) return NotFound(new { message = "Семейство не найдено." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFamily(string id, [FromBody] FamilyMetadata updatedFamily)
    {
        try
        {
            var result = await _familyService.UpdateFamilyAsync(id, updatedFamily);
            if (!result) return NotFound(new { message = "Семейство не найдено." });

            return Ok(new { message = "Семейство обновлено" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


}
