﻿namespace Courses.Business.Errors;

public class LessonErrors
{
    public static readonly Error NotFound
        = new("Lesson.NotFound", "Lesson not found", StatusCodes.Status404NotFound);

    public static readonly Error InvalidLessonOrder
        = new(nameof(InvalidLessonOrder), "invalid lesson order input a valid one", StatusCodes.Status400BadRequest);
    
    public static readonly Error DuplicatedTitle
        = new(nameof(DuplicatedTitle), "there is an lesson with same title select another title.", StatusCodes.Status409Conflict);
}