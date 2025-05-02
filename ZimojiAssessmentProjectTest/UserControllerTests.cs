using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimojiAssessmentProject.Controllers;
using ZimojiAssessmentProject.Interfaces;
using ZimojiAssessmentProject.Models;

namespace ZimojiAssessmentProjectTest
{
        public class UserControllerTests
        {
            private readonly Mock<IUserTaskActions> _mockUserTaskActions;
            private readonly UserController _controller;

            public UserControllerTests()
            {
                _mockUserTaskActions = new Mock<IUserTaskActions>();
                _controller = new UserController(_mockUserTaskActions.Object, null); // userTaskContext not used in controller logic
            }

            [Fact]
            public async Task GetTasksByUserID_ReturnsOkResult_WithListOfTasks()
            {
                // Arrange
                int userId = 1;
                var mockTasks = new List<AvailableTasks>
            {
                new AvailableTasks { Task_Id = 1, Task_Name = "Task1", Task_Description = "Description1" },
                new AvailableTasks { Task_Id = 2, Task_Name = "Task2", Task_Description = "Description2" }
            };

                _mockUserTaskActions.Setup(x => x.GetAllAvailableTasksForUserAsync(userId))
                                    .ReturnsAsync(mockTasks);

                // Act
                var result = await _controller.GetTasksByUserID(userId);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsAssignableFrom<IEnumerable<AvailableTasks>>(okResult.Value);
                Assert.Equal(2, ((List<AvailableTasks>)returnValue).Count);
            }

            [Fact]
            public async Task GetTaskByID_ReturnsOkResult_WithTaskDetails()
            {
                // Arrange
                int taskId = 10;
                var mockTask = new List<AvailableTasks>
            {
                new AvailableTasks { Task_Id = 10, Task_Name = "MockTask", Task_Description = "MockDescription" }
            };

                _mockUserTaskActions.Setup(x => x.GetAvailableTaskByIdAsync(taskId))
                                    .ReturnsAsync(mockTask);

                // Act
                var result = await _controller.GetTaskByID(taskId);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                var returnValue = Assert.IsAssignableFrom<IEnumerable<AvailableTasks>>(okResult.Value);
                Assert.Single(returnValue);
            }
        }
    

}

