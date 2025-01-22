using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Query;
using Xunit;

namespace Project.Manager.Application.Tests.Handlers.Query
{
    public class TaskQueryTest
    {
        private readonly Mock<DataDbContext> _mockContext;
        private readonly Mock<ILogger<TaskQuery>> _mockLogger;
        private readonly TaskQuery _taskQuery;

        public TaskQueryTest()
        {
            _mockContext = new Mock<DataDbContext>();
            _mockLogger = new Mock<ILogger<TaskQuery>>();
            _taskQuery = new TaskQuery(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetTasksByProject_ShouldReturnProjectWithTasks()
        {
            // Arrange
            var projectId = 1;
            var tasks = new List<TaskItem> { new TaskItem { Id = 1, Title = "Task 1" } };
            var project = new List<Api.Models.Project> {new Api.Models.Project{ Id = projectId, Name = "Test Project", Tasks = tasks }}.AsQueryable();

            var mockSet = new Mock<DbSet<Api.Models.Project>>();

            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Provider).Returns(project.Provider);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Expression).Returns(project.Expression);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.ElementType).Returns(project.ElementType);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.GetEnumerator()).Returns(project.GetEnumerator());

            _mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            // Act
            var result = _taskQuery.GetTasksByProject(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(projectId, result.Id);
            Assert.Equal(tasks.Count, result.Tasks.Count);
        }
    }
}