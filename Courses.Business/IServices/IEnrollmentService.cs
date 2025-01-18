﻿using Courses.Business.Contract.Course;
using Courses.Business.Contract.User;

namespace Courses.Business.IServices;
public interface IEnrollmentService 
{
    Task<Result> EnrollToCourseAsync(Guid courseId, string userId, CancellationToken cancellationToken = default);
    Task<Result<UserCourseResponse>> GetAsync(Guid courseId, string userId, CancellationToken token = default);
    Task<IEnumerable<UserCourseResponse>> GetMyCoursesAsync(string userId, CancellationToken cancellationToken = default);
    Task<(int NoCompleted, int NoEnrollment)> GetCourseInfoAsync(Guid courseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CourseResponse>> GetAllCoursesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<UserResponse>> GetUsersInCourseAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<UserCourse>> GetByCourseIdAsync(Guid id, string userId, CancellationToken cancellationToken = default);
    // get user courses 
    // get user report of any course with courseid
}