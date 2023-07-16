using AnimatLabs.Api.Models;
using AnimatLabs.Api.Sdk;
using Moq;
using Refit;

namespace AnimatLabs.Api.Consumer.Tests;

public class StudentsServiceConsumerTests
{
    private readonly Mock<IStudentsService> _mockStudentsService;
    private readonly StudentsServiceConsumer _studentsServiceConsumer;
    private readonly RefitSettings _refitSettings;

    public StudentsServiceConsumerTests()
    {
        _refitSettings = new RefitSettings();
        _mockStudentsService = new Mock<IStudentsService>();
        _studentsServiceConsumer = new StudentsServiceConsumer(_mockStudentsService.Object);
    }

    [Fact]
    public async Task ReadAllStudents_ShouldReturnAllStudents()
    {
        // Arrange
        var expectedStudents = new List<Student> { new Student(), new Student() };
        var expectedResponse = new ApiResponse<IEnumerable<Student>>(
                new HttpResponseMessage()
                {
                    Content = new StringContent(string.Empty),
                    StatusCode = System.Net.HttpStatusCode.OK
                },
                expectedStudents,
                _refitSettings);
        _mockStudentsService.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedResponse);

        // Act
        var students = await _studentsServiceConsumer.ReadAllStudents();

        // Assert
        Assert.Equal(expectedStudents, students);
    }

    [Fact]
    public async Task ReadStudent_ShouldReturnStudentById()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedStudent = new Student { Id = id, FirstName = "John", LastName = "Doe" };
        var expectedResponse = new ApiResponse<Student>(
            new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(string.Empty)
            },
            expectedStudent,
            _refitSettings);
        _mockStudentsService.Setup(s => s.GetAsync(id)).ReturnsAsync(expectedResponse);

        // Act
        var student = await _studentsServiceConsumer.ReadStudent(id);

        // Assert
        Assert.Equal(expectedStudent, student);
    }

    [Fact]
    public async Task UpdateStudent_ShouldUpdateExistingStudent()
    {
        // Arrange
        var id = Guid.NewGuid();
        var student = new Student { Id = id, FirstName = "John", LastName = "Doe" };
        var expectedResponse = new ApiResponse<bool>(
        new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(string.Empty)
        },
        true,
        _refitSettings);
        _mockStudentsService.Setup(s => s.UpdateAsync(id, student)).ReturnsAsync(expectedResponse);

        // Act
        var isUpdated = await _studentsServiceConsumer.UpdateStudent(id, student);

        // Assert
        Assert.True(isUpdated);
    }

    [Fact]
    public async Task DeleteStudent_ShouldDeleteExistingStudent()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedResponse = new ApiResponse<bool>(
            new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(string.Empty)
            },
            true,
            _refitSettings);
        _mockStudentsService.Setup(s => s.DeleteAsync(id)).ReturnsAsync(expectedResponse);

        // Act
        var isDeleted = await _studentsServiceConsumer.DeleteStudent(id);

        // Assert
        Assert.True(isDeleted);
    }
}