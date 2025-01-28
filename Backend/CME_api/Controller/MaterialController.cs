using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MaterialController : ControllerBase
{
    private readonly IMaterialService _materialService;

    public MaterialController(IMaterialService materialService)
    {
        _materialService = materialService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<MaterialDto>> CreateMaterial(MaterialDto materialDto)
    {
        var material = await _materialService.CreateMaterial(materialDto);
        return CreatedAtAction(nameof(GetMaterialById), new { id = material.IdUser }, material);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaterialDto>>> GetAllMaterials()
    {
        var materials = await _materialService.GetAllMaterials();
        return Ok(materials);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialDto>> GetMaterialById(int id)
    {
        var material = await _materialService.GetMaterialById(id);
        if (material == null)
            return NotFound(new { message = "Material não encontrado." });

        return Ok(material);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaterial(int id)
    {
        var result = await _materialService.DeleteMaterial(id);
        if (result == null)
            return NotFound(new { message = "Material não encontrado." });

        if (!result.Value)
            return BadRequest(new { message = "Material não pode ser excluído porque está em uso no processo." });

        return Ok(new { message = "Material excluído com sucesso!" });
    }
}
