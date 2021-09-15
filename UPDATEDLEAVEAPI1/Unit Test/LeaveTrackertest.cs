using EmployeeDataAccess;
using LEAVEAPI.Controllers;
using LEAVEAPI.EmployeeDataAccess;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Unit_Test
{
    public class LeaveControllerTest
    {
        [Fact]
        public void GetOwnLeavesTest()
        {
            int employeeId = 100;
            var list = new List<LEAVE>();
            var leaveData = new LEAVE()
            {
                employeeId = 102,
                name = "Hazel Nutt",
                managerId = 100,
                title = "leave",
                description = "vaccination",
                startDate = Convert.ToDateTime("2020/09/09"),
                endDate = Convert.ToDateTime("2020/ 09/ 10"),
                status = "Open",


            }; list.Add(leaveData);

            Mock<ILeaveDb> MockLeave = new Mock<ILeaveDb>();
            MockLeave.Setup(t => t.GetLeaves(employeeId)).Returns(list);

            var LeaveController = new LeaveController(MockLeave.Object);

            var lList = LeaveController.GetOwnLeaves(employeeId);
            Assert.True(list.Count == lList.Count);
            Assert.NotNull(lList);

        }
        [Fact]
        public void GetAssignedLeavesTest()
        {
            int managerId = 100;
            var list = new List<LEAVE>();
            var leaveData = new LEAVE()
            {
                employeeId = 102,
                name = "Hazel Nutt",
                managerId = 100,
                title = "leave",
                description = "vaccination",
                startDate = Convert.ToDateTime("2020/09/09"),
                endDate = Convert.ToDateTime("2020/ 09/ 10"),
                status = "Open",
            }; list.Add(leaveData);

            Mock<ILeaveDb> MockLeave = new Mock<ILeaveDb>();
            MockLeave.Setup(t => t.AssignedLeaves(managerId)).Returns(list);

            var LeaveController = new LeaveController(MockLeave.Object);

            var lList = LeaveController.GetAssignedLeaves(managerId);
            Assert.True(list.Count == lList.Count);
            Assert.NotNull(lList);

        }
    }
}
