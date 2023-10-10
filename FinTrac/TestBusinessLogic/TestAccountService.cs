using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain.DataTypes;
using Domain;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestAccountService
    {
        private AccountService _service;
        [TestInitialize]
        public void SetUp()
        {
            _service = new AccountService(new MemoryDatabase());
        }

        [TestMethod]
        public void AddPersonalAccount() 
        {
        }

        [TestMethod]
        public void AddCreditCard() { }

        [TestMethod]
        public void GetAccounts() { }

        [TestMethod]
        public void ModifyPersonalAccount() { }

        [TestMethod]
        public void ModifyCreditCard() { }

        [TestMethod]
        public void DeletePersonalAccount() { }

        [TestMethod]
        public void DeleteCreditCard() { }
    }
}
