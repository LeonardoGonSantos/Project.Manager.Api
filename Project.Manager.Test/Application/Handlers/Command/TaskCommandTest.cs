using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Command;
using Xunit;

namespace Project.Manager.Application.Tests.Handlers.Command
{
    public class TaskCommandTest
    {
        private readonly Mock<DataDbContext> _mockContext;
        private readonly Mock<IProjectQuery> _mockProjectQuery;
        private readonly Mock<ILogger<TaskCommand>> _mockLogger;
        private readonly TaskCommand _taskCommand;

        public TaskCommandTest()
        {
            _mockContext = new Mock<DataDbContext>();
            _mockProjectQuery = new Mock<IProjectQuery>();
            _mockLogger = new Mock<ILogger<TaskCommand>>();
            _taskCommand = new TaskCommand(_mockContext.Object, _mockProjectQuery.Object, _mockLogger.Object);
        }

        [Fact]
        public void AddTask_ShouldAddTaskToProject()
        {
            // Arrange
            var projectId = 1;
            var task = new TaskItem { Id = 1, Title = "Test Task" };
            var project = new Api.Models.Project { Id = projectId, Tasks = new List<TaskItem>() };
            _mockProjectQuery.Setup(m => m.GetProjectById(projectId)).Returns(project);

            // Act
            _taskCommand.AddTask(projectId, task);

            // Assert
            Assert.Contains(task, project.Tasks);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void AddTask_ShouldThrowExceptionWhenProjectNotFound()
        {
            // Arrange
            var projectId = 1;
            var task = new TaskItem { Id = 1, Title = "Test Task" };
            _mockProjectQuery.Setup(m => m.GetProjectById(projectId)).Returns((Api.Models.Project)null);

            // Act & Assert
            Assert.Throws<Exception>(() => _taskCommand.AddTask(projectId, task));
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void UpdateTask_ShouldUpdateTaskInProject()
        {
            // Arrange
            var projectId = 1;
            var task = new TaskItem { Id = 1, Title = "Updated Task" };
            var existingTask = new TaskItem { Id = 1, Title = "Old Task" };
            var project = new Api.Models.Project { Id = projectId, Tasks = new List<TaskItem> { existingTask } };
            _mockProjectQuery.Setup(m => m.GetProjectById(projectId)).Returns(project);

            // Act
            _taskCommand.UpdateTask(projectId, task);

            // Assert
            Assert.Equal("Updated Task", existingTask.Title);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void UpdateTask_ShouldNotUpdateTaskWhenNotFound()
        {
            // Arrange
            var projectId = 1;
            var task = new TaskItem { Id = 1, Title = "Updated Task" };
            var project = new Api.Models.Project { Id = projectId, Tasks = new List<TaskItem>() };
            _mockProjectQuery.Setup(m => m.GetProjectById(projectId)).Returns(project);

            // Act
            _taskCommand.UpdateTask(projectId, task);

            // Assert
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void RemoveTask_ShouldRemoveTaskFromProject()
        {
            // Arrange
            var projectId = 1;
            var taskId = 1;
            var task = new TaskItem { Id = taskId, Title = "Test Task" };
            var project = new Api.Models.Project { Id = projectId, Tasks = new List<TaskItem> { task } };
            _mockProjectQuery.Setup(m => m.GetProjectById(projectId)).Returns(project);

            // Act
            _taskCommand.RemoveTask(projectId, taskId);

            // Assert
            Assert.DoesNotContain(task, project.Tasks);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void RemoveTask_ShouldNotRemoveTaskWhenNotFound()
        {
            // Arrange
            var projectId = 1;
            var taskId = 1;
            var project = new Api.Models.Project { Id = projectId, Tasks = new List<TaskItem>() };
            _mockProjectQuery.Setup(m => m.GetProjectById(projectId)).Returns(project);

            // Act
            _taskCommand.RemoveTask(projectId, taskId);

            // Assert
            _mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}