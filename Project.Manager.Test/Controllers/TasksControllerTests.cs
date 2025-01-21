using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using TaskManagementAPI.Controllers;
using TaskManagementAPI.Interfaces;
using TaskManagementAPI.Models;
using Xunit;

namespace Project.Manager.Test.Controllers
{
    public class TasksControllerTests
    {
        private readonly Mock<IProjectService> _mockProjectService;
        private readonly Mock<ITaskService> _mockTaskService;
        private readonly TasksController _tasksController;

        public TasksControllerTests()
        {
            _mockProjectService = new Mock<IProjectService>();
            _mockTaskService = new Mock<ITaskService>();
            _tasksController = new TasksController(_mockProjectService.Object, _mockTaskService.Object);
        }

        [Fact]
        public void GetProjects_ShouldReturnOkResult_WithListOfProjects()
        {
            var projects = new List<Project> { new Project { Id = 1, Name = "Test Project" } };
            _mockProjectService.Setup(p => p.GetProjects()).Returns(projects);

            var result = _tasksController.GetProjects();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Project>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetTasks_ShouldReturnOkResult_WithListOfTasks_WhenProjectExists()
        {
            var tasks = new List<TaskItem> { new TaskItem { Id = 1, Title = "Test Task" } };
            var project = new Project { Id = 1, Name = "Test Project", Tasks = tasks };
            _mockProjectService.Setup(p => p.GetProjectById(1)).Returns(project);

            var result = _tasksController.GetTasks(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TaskItem>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetTasks_ShouldReturnNotFoundResult_WhenProjectDoesNotExist()
        {
            _mockProjectService.Setup(p => p.GetProjectById(1)).Returns((Project)null);

            var result = _tasksController.GetTasks(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CreateProject_ShouldReturnCreatedAtActionResult_WithCreatedProject()
        {
            var project = new Project { Id = 1, Name = "Test Project" };

            var result = _tasksController.CreateProject(project);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Project>(createdAtActionResult.Value);
            Assert.Equal(project, returnValue);
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