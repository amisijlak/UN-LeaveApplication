using LeaveApplication.BLL.Data;
using LeaveApplication.BLL.Leave;
using LeaveApplication.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveApplication.TEST
{
    [TestClass]
    [TestCategory("Leave Requests")]
    public class TestLeaveRequestValidation
    {
        private List<LeaveRequest> Requests;
        private List<Employee> Employees;
        private ILeaveRequestService leaveRequestService;
        private IDbRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = GetRepository();
            leaveRequestService = new LeaveRequestService(repository);
        }

        private IDbRepository GetRepository()
        {
            Employees = new List<Employee>
            {
                new Employee
                {
                   Id=1, FirstName ="Amisi", LastName ="Kale", DepartmentId=2
                },
                new Employee
                {
                   Id=2,FirstName ="John", LastName ="Kale", DepartmentId=2
                },
                new Employee
                {
                   Id=3, FirstName ="Mick", LastName ="Tony", DepartmentId=2
                },
                new Employee
                {
                   Id=4, FirstName ="Jule", LastName ="Min", DepartmentId=1
                }
            };

            Requests = new List<LeaveRequest>
            {
                new LeaveRequest
                {
                    Description = "New Leave",EmployeeId = 1,StartDate = DateTime.Today,EndDate = DateTime.Today.AddDays(7), Id = 1,LeaveTypeId = 2,
                    Employee = Employees.Where(r=>r.Id == 1).FirstOrDefault(),
                },
                new LeaveRequest
                {
                    Description = "New Leave",EmployeeId = 2,StartDate = DateTime.Today,EndDate = DateTime.Today.AddDays(10),Id = 1,LeaveTypeId = 2,
                    Employee = Employees.Where(r=>r.Id == 2).FirstOrDefault(),
                }
            };

            var repoMock = new Mock<IDbRepository>();

            repoMock.Setup(x => x.Set<LeaveRequest>()).Returns(GetQueryableMockDbSet(Requests));
            repoMock.Setup(x => x.Set<Employee>()).Returns(GetQueryableMockDbSet(Employees));

            return repoMock.Object;
        }

        private DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        /// <summary>
        /// We test here the first validation Rule
        /// Employee should not have requests whose dates overlap
        /// </summary>
        [TestMethod]
        public void ThenFailForOverLapingDate()
        {
            var model = new LeaveRequest
            {
                Description = "New Leave",
                EmployeeId = 1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(6)
            };

            var result = leaveRequestService.ValidateLeaveRequest(model);
            Assert.AreEqual(false, result.Item1);
            Assert.AreEqual("The dates you have selected ovalap with one of your previous requests!", result.Item2);
        }

        [TestMethod]
        public void ThenFailForOverlapingInSameDepartment()
        {
            var model = new LeaveRequest
            {
                Description = "New Leave",
                EmployeeId = 3,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(6)
            };

            var result = leaveRequestService.ValidateLeaveRequest(model);
            Assert.AreEqual(false, result.Item1);
            Assert.AreEqual("The dates you have selected ovalap with onether employee in your department!", result.Item2);
        }

        [TestMethod]
        public void ThenFailForApplicationWithinTheSameMonth()
        {
            var model = new LeaveRequest
            {
                Description = "New Leave",
                EmployeeId = 1,
                StartDate = DateTime.Today.AddDays(8),
                EndDate = DateTime.Today.AddDays(10)
            };

            var result = leaveRequestService.ValidateLeaveRequest(model);
            Assert.IsFalse(result.Item1);
        }

        [TestMethod]
        public void ThenPassForValidApplication()
        {
            var model = new LeaveRequest
            {
                Description = "New Leave",
                EmployeeId = 1,
                StartDate = DateTime.Today.AddDays(37),
                EndDate = DateTime.Today.AddDays(40)
            };

            var result = leaveRequestService.ValidateLeaveRequest(model);
            Assert.IsTrue(result.Item1);
        }
    }
}
