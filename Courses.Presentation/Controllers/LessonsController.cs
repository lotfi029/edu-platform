﻿using Courses.Business.Contract.Lesson;
using Courses.Business.Entities;
using System.Security.Claims;

namespace Courses.Presentation.Controllers;
[Route("api/{courseId:guid}/modules/{moduleId:guid}/[controller]")]
[ApiController]
[Authorize]
public class LessonsController(ILessonService lessonService) : ControllerBase
{
    private readonly ILessonService _lessonService = lessonService;

    [HttpPost("")]
    public async Task<IActionResult> Add([FromRoute] Guid courseId, [FromRoute] Guid moduleId, [FromForm] LessonRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.AddAsync(moduleId, userId, request, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { courseId, moduleId, id = result.Value }, null) : result.ToProblem();
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTitle([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromBody] UpdateLessonTitleRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.UpdateTitleAsync(id, moduleId, request, userId, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{id:guid}/update-order")]
    public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromRoute] Guid moduleId, [FromBody] UpdateLessonOrderRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.UpdateOrderAsync(moduleId, id, userId, request.Order, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{id:guid}/update-video")]
    public async Task<IActionResult> UpdateVideo([FromRoute] Guid id, [FromForm] UpdateLessonVideoRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.UpdateVideoAsync(id, userId, request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{id:guid}/toggle-preview")]
    public async Task<IActionResult> ToggleIsPreview([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.ToggleIsPreviewAsync(id, userId, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPost("{id:guid}/add-recourse")]
    public async Task<IActionResult> AddRecourse([FromRoute] Guid id, [FromForm] RecourseRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.AddResourceAsync(id, request, userId, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();

    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.GetAsync(id, userId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromRoute] Guid moduleId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;

        var result = await _lessonService.GetAllAsync(moduleId, userId, cancellationToken);

        return Ok(result);
    }

}
