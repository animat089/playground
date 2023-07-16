using AnimatLabs.Api.Models;
using AnimatLabs.Api.Sdk;

namespace AnimatLabs.Api.Consumer;

public class StudentsServiceConsumer
{
    private readonly IStudentsService _studentsService;

    public StudentsServiceConsumer(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    public async Task<IEnumerable<Student>?> ReadAllStudents()
    {
        var response = await _studentsService.GetAllAsync().ConfigureAwait(false);
        return response.Content?.ToList();
    }

    public async Task<Guid?> CreateStudent(Student student)
    {
        var response = await _studentsService.CreateAsync(student).ConfigureAwait(false);
        return response.Content;
    }

    public async Task<Student?> ReadStudent(Guid id)
    {
        var response = await _studentsService.GetAsync(id).ConfigureAwait(false);
        return response.Content;
    }

    public async Task<bool> UpdateStudent(Guid id, Student student)
    {
        var response = await _studentsService.UpdateAsync(id, student).ConfigureAwait(false);
        return response.Content;
    }

    public async Task<bool> DeleteStudent(Guid id)
    {
        var response = await _studentsService.DeleteAsync(id).ConfigureAwait(false);
        return response.Content;
    }
}