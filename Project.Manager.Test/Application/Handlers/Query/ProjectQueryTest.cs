using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Manager.Api.Models;
using Project.Manager.Api.Services;
using Xunit;

namespace Project.Manager.Application.Tests.Handlers.Query
{
    public class ProjectQueryTest
    {
        private readonly Mock<DataDbContext> _mockContext;
        private readonly Mock<ILogger<ProjectQuery>> _mockLogger;
        private readonly ProjectQuery _projectQuery;

        public ProjectQueryTest()
        {
            _mockContext = new Mock<DataDbContext>();
            _mockLogger = new Mock<ILogger<ProjectQuery>>();
            _projectQuery = new ProjectQuery(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetProjects_ShouldReturnAllProjects()
        {
            // Arrange
            var projects = new List<Api.Models.Project>
            {
                new Api.Models.Project { Id = 1, Name = "Project 1" },
                new Api.Models.Project { Id = 2, Name = "Project 2" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Api.Models.Project>>();
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Provider).Returns(projects.Provider);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Expression).Returns(projects.Expression);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.ElementType).Returns(projects.ElementType);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.GetEnumerator()).Returns(projects.GetEnumerator());

            _mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            // Act
            var result = _projectQuery.GetProjects();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Project 1", result[0].Name);
            Assert.Equal("Project 2", result[1].Name);
        }

        [Fact]
        public void GetProjectById_ShouldReturnProject_WhenProjectExists()
        {
            // Arrange
            var project = new Api.Models.Project { Id = 1, Name = "Project 1" };
            var projects = new List<Api.Models.Project> { project }.AsQueryable();

            var mockSet = new Mock<DbSet<Api.Models.Project>>();
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Provider).Returns(projects.Provider);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Expression).Returns(projects.Expression);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.ElementType).Returns(projects.ElementType);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.GetEnumerator()).Returns(projects.GetEnumerator());

            _mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            // Act
            var result = _projectQuery.GetProjectById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Project 1", result.Name);
        }

        [Fact]
        public void GetProjectById_ShouldReturnNull_WhenProjectDoesNotExist()
        {
            // Arrange
            var projects = new List<Api.Models.Project>().AsQueryable();

            var mockSet = new Mock<DbSet<Api.Models.Project>>();
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Provider).Returns(projects.Provider);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.Expression).Returns(projects.Expression);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.ElementType).Returns(projects.ElementType);
            mockSet.As<IQueryable<Api.Models.Project>>().Setup(m => m.GetEnumerator()).Returns(projects.GetEnumerator());

            _mockContext.Setup(m => m.Projects).Returns(mockSet.Object);

            // Act
            var result = _projectQuery.GetProjectById(1);

            // Assert
            Assert.Null(result);
        }
    }
}