﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Domain;
using Domain.Exceptions;

namespace TestDomain
{
    [TestClass]
    public class TestWorkspace
    {
        public Workspace workspace;
        [TestInitialize]
        public void Setup()
        {
            workspace = new Workspace();
        }

        [TestMethod]
        public void TestWorkspaceName()
        {
            workspace.Name = "Test";
            Assert.AreEqual("Test", workspace.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(EmptyFieldException))]
        public void EmptyNameException()
        {
            workspace.Name = "";
        }

        [TestMethod]
        public void UserAdminNotNull()
        {
            User admin = new User
            {
                Name = "Test",
                LastName = "LastName",
                Address = "",
                Password = "1234567891",
                Email = "test@test.com"
            };
            workspace.UserAdmin = admin;
            Assert.IsNotNull(workspace.UserAdmin);
        }

        [TestMethod]
        public void AccountListNotNull()
        {
            Assert.IsNotNull(workspace.AccountList);
        }

        [TestMethod]
        public void ReportListNotNull()
        {
            Assert.IsNotNull(workspace.ReportList);

        }

        [TestMethod]
        public void ExchangeListNotNull()
        {
            Assert.IsNotNull(workspace.ExchangeList);
        }

        [TestMethod]
        public void CategoryListNotNull()
        {
            Assert.IsNotNull(workspace.CategoryList);
        }

        [TestMethod]
        public void GoalListNotNull()
        {
            Assert.IsNotNull(workspace.GoalList);
        }
    }
}
