﻿namespace Courses.Presentation.Controllers;
[Route("api/{courseId:guid}/modules/{moduleId:guid}/[controller]")]
[ApiController]
[Authorize]
public class LessonsController(
    ILessonService lessonService,
    IModuleItemService moduleItemService) : ControllerBase
{
    private readonly ILessonService _lessonService = lessonService;
    private readonly IModuleItemService _moduleItemService = moduleItemService;

    [HttpPost("")]
    [HasPermission(Permissions.AddLesson)]
    public async Task<IActionResult> Add([FromRoute] Guid courseId, [FromRoute] Guid moduleId, [FromForm] LessonRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.AddAsync(moduleId, userId, request, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { courseId, moduleId, id = result.Value }, null) : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.UpdateLesson)]
    public async Task<IActionResult> UpdateTitle([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromBody] UpdateLessonRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;
        // TODO: find a way to check if the course Id is valid
        var result = await _lessonService.UpdateTitleAsync(id, moduleId, request, userId, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{id:guid}/update-index")]
    [HasPermission(Permissions.UpdateLesson)]
    public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromBody] UpdateIndexRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _moduleItemService.UpdateModuleItemIndexAsync(moduleId, id, userId, request.Index, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{id:guid}/update-video")]
    [HasPermission(Permissions.UpdateLesson)]
    public async Task<IActionResult> UpdateVideo([FromRoute] Guid id, [FromForm] UpdateLessonVideoRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.UpdateVideoAsync(id, userId, request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{id:guid}/toggle-preview")]
    [HasPermission(Permissions.UpdateLesson)]
    public async Task<IActionResult> ToggleIsPreview([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.ToggleIsPreviewAsync(id, userId, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("{id:guid}/add-recourse")]
    [HasPermission(Permissions.UpdateLesson)]
    public async Task<IActionResult> AddRecourse([FromRoute] Guid id, [FromForm] RecourseRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.AddResourceAsync(id, request, userId, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpGet("{id:guid}")]
    [HasPermission(Permissions.GetLesson)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.GetAsync(id, userId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("")]
    [HasPermission(Permissions.GetLesson)]
    public async Task<IActionResult> GetAll([FromRoute] Guid moduleId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.GetAllAsync(moduleId, userId, cancellationToken);

        return Ok(result);
    }

}
