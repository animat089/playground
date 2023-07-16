using AnimatLabs.Api.Models;
using Refit;

namespace AnimatLabs.Api.Sdk;

/// <summary>
/// Refit service interface
/// </summary>
public interface IStudentsService
{
    [Get("/students")]
    Task<ApiResponse<IEnumerable<Student>>> GetAllAsync();

    [Get("/students/{id}")]
    Task<ApiResponse<Student?>> GetAsync(Guid id);

    [Post("/students")]
    Task<ApiResponse<Guid?>> CreateAsync([Body] Student entity);

    [Post("/students/{id}")]
    Task<ApiResponse<bool>> UpdateAsync(Guid id, [Body] Student entity);

    [Delete("/students/{id}")]
    Task<ApiResponse<bool>> DeleteAsync(Guid id);
}