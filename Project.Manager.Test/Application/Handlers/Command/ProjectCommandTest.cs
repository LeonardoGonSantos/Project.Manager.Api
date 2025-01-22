using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Command;
using Xunit;

namespace Project.Manager.Application.Tests.Handlers.Command
{
    public class ProjectCommandTest
    {
        private readonly Mock<DataDbContext> _mockContext;
        private readonly Mock<ILogger<ProjectCommand>> _mockLogger;
        private readonly ProjectCommand _projectCommand;

        public ProjectCommandTest()
        {
            _mockContext = new Mock<DataDbContext>();
            _mockLogger = new Mock<ILogger<ProjectCommand>>();
            _projectCommand = new ProjectCommand(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void AddProject_ShouldAddProjectAndReturnId()
        {
            // Arrange
            var project = new Api.Models.Project { Id = 1, Name = "Test Project" };
            var mockSet = new Mock<DbSet<Api.Models.Project>>();
            _mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            // Act
            var result = _projectCommand.AddProject(project);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Api.Models.Project>()), Times.Once());
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.Equal(1, result);
        }
    }
}