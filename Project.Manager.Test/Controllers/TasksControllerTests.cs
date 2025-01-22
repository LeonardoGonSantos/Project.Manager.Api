using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Project.Manager.Api.Controllers;
using Project.Manager.Api.Models;
using Project.Manager.Application.Handlers.Command;
using Xunit;

namespace Project.Manager.Test.Controllers
{
    public class TasksControllerTests
    {
        private readonly Mock<ITaskCommand> _mockTaskCommand;
        private readonly Mock<ITaskQuery> _mockTaskService;
        private readonly TasksController _tasksController;

        public TasksControllerTests()
        {
            _mockTaskCommand = new Mock<ITaskCommand>();
            _mockTaskService = new Mock<ITaskQuery>();
            _tasksController = new TasksController(_mockTaskCommand.Object, _mockTaskService.Object);
        }

        [Fact]
        public void GetTasks_ShouldReturnNotFoundResult_WhenProjectDoesNotExist()
        {
            _mockTaskService.Setup(p => p.GetTasksByProject(1)).Returns((Api.Models.Project)null);

            var result = _tasksController.GetTasks(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CreateProject_ShouldReturnCreatedAtActionResult_WithCreatedProject()
        {
            var task = new Api.Models.TaskItem() { Id = 1, Title = "Test Project" };

            var result = _tasksController.CreateTask(1 ,task);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Api.Models.Project>(createdAtActionResult.Value);
            Assert.Equal(new Api.Models.Project(), returnValue);
        }

        [Fact]
        public void CreateTask_ShouldReturnCreatedAtActionResult_WithCreatedTask()
        {
            var task = new TaskItem { Id = 1, Title = "Test Task" };

            var result = _tasksController.CreateTask(1, task);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<TaskItem>(createdAtActionResult.Value);
            Assert.Equal(task, returnValue);
        }

        [Fact]
        public void UpdateTask_ShouldReturnNoContentResult()
        {
            var task = new TaskItem { Id = 1, Title = "Updated Task" };

            var result = _tasksController.UpdateTask(1, 1, task);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteTask_ShouldReturnNoContentResult()
        {
            var result = _tasksController.DeleteTask(1, 1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}