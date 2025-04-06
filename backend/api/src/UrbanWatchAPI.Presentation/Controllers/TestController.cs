using Microsoft.AspNetCore.Mvc;
using UrbanWatchAPI.Infrastructure.Mongo.Repositories;

namespace UrbanWatchAPI.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly VehicleSnapshotRepository _snapshotRepository;

    public TestController(
        VehicleSnapshotRepository snapshotRepository
        )
    {
        _snapshotRepository = snapshotRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int seconds)
    {
        var result = await _snapshotRepository.GetRecorsForLastAsync(seconds);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _snapshotRepository.GetLastAsync();
        return Ok(result);
    }
}