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
            var project = new Api.Models.Project { Id = projectId, Name = "Test Project", Tasks = tasks };
            var mockSet = new Mock<DbSet<Api.Models.Project>>();
            mockSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockSet.Object);
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