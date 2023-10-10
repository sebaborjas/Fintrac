using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain.DataTypes;
using Domain;
using Domain.Exceptions;

namespace TestBusinessLogic
{
    [TestClass]
    public class TestAccountService
    {
        private AccountService _service;
        private MemoryDatabase _database;
        private Workspace workspace;

        [TestInitialize]
        public void SetUp()
        {
            
            User user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };
            workspace = new Workspace(user, "Test");

            user.WorkspaceList.Add(workspace);
            _database.Users.Add(user);

            _database = new MemoryDatabase();
            _service = new AccountService(_database);

        }

        [TestMethod]
        public void AddPersonalAccount() 
        {
            PersonalAccount personalAccount = new PersonalAccount { 
                Name = "Test", 
                StartingAmount = 1000, 
                Currency = CurrencyType.UYU,
                WorkSpace = workspace 
            };
            _service.Add(personalAccount);

            Assert.AreEqual(personalAccount, workspace.AccountList.First(x => x.Name == personalAccount.Name));
        }

        [TestMethod]
        public void AddCreditCard() 
        {
            CreditCard creditCardAccount = new CreditCard {
                BankName = "Santander", 
                LastDigits = 1234, 
                AvailableCredit = 10000,
                DeadLine = 26, 
                Name = "Credit Santander", 
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            _service.Add(creditCardAccount);
            Assert.AreEqual(creditCardAccount, workspace.AccountList.First(x => x.Name == creditCardAccount.Name));
        }

        [TestMethod]
        public void GetAccount() 
        {
            String name = "Credit Santander";

            CreditCard creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = 1234,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = name,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            _service.Add(creditCardAccount);
            Account account = _service.Get(name);

            Assert.AreEqual(account, creditCardAccount);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementNotFoundException))]
        public void GetAccountDoesntExists() 
        {
            Account account = _service.Get("Santander");
        }

        [TestMethod]
        public void ModifyPersonalAccount() 
        {
            PersonalAccount personalAccount = new PersonalAccount
            {
                Name = "Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            PersonalAccount personalAccountModified = new PersonalAccount
            {
                Name = personalAccount.Name,
                StartingAmount = personalAccount.StartingAmount,
                Currency = personalAccount.Currency,
                WorkSpace = personalAccount.WorkSpace
            };


            workspace.AccountList.Add(personalAccount);

            String accountName = personalAccount.Name;

            String newName = "BROU";

            personalAccountModified.Name = newName;

            _service.Modify(accountName, personalAccountModified);

            Assert.AreEqual(personalAccountModified, workspace.AccountList.First(x => x.Name == newName));

        }

        [TestMethod]
        [ExpectedException(typeof(AccountAlreadyExistsException))]
        public void ModifyPersonalAccountAlreadyExists() 
        {
            PersonalAccount firstPersonalAccount = new PersonalAccount
            {
                Name = "Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            PersonalAccount secondPersonalAccount = new PersonalAccount
            {
                Name = "Second Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            String newName = "Test";

            PersonalAccount accountModified = new PersonalAccount
            {
                Name = newName,
                StartingAmount = secondPersonalAccount.StartingAmount,
                Currency = secondPersonalAccount.Currency,
                WorkSpace = secondPersonalAccount.WorkSpace
            };

            workspace.AccountList.Add(firstPersonalAccount);
            workspace.AccountList.Add(secondPersonalAccount);

            String nameToFind = secondPersonalAccount.Name;

            _service.Modify(nameToFind, accountModified);



        }

        [TestMethod]
        public void ModifyCreditCard() 
        {
            CreditCard creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = 1234,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Credit Santander",
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            String nameToFind = creditCardAccount.Name;

            workspace.AccountList.Add(creditCardAccount);

            String newName = "Credit Oca";
            int newLastDigits = 4321;

            CreditCard creditCardAccountModified = new CreditCard
            {
                BankName = "Santander",
                LastDigits = newLastDigits,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = newName,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            _service.Modify(nameToFind, creditCardAccountModified);

            Account creditCard = workspace.AccountList.First(x => x.Name == newName);

            Assert.AreEqual(creditCardAccountModified, creditCard);
        }

        [TestMethod]
        public void ModifyCreditCardAlreadyExists() 
        {
            CreditCard creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = 1234,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Credit Santander",
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            CreditCard secondCreditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = 1234,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Credit Brou",
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            String nameToFind = secondCreditCardAccount.Name;

            workspace.AccountList.Add(creditCardAccount);
            workspace.AccountList.Add(secondCreditCardAccount);

            String newName = "Credit Santander";
            int newLastDigits = 4321;

            CreditCard creditCardAccountModified = new CreditCard
            {
                BankName = "Santander",
                LastDigits = newLastDigits,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = newName,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            _service.Modify(nameToFind, creditCardAccountModified);
        }

        [TestMethod]
        public void DeletePersonalAccount() 
        {
            PersonalAccount personalAccount = new PersonalAccount
            {
                Name = "Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            String nameToFind = personalAccount.Name;

            workspace.AccountList.Add(personalAccount);

            _service.Delete(nameToFind);

            Assert.IsNull(workspace.AccountList.First(x => x.Name == nameToFind));
        }

        [TestMethod]
        public void DeleteCreditCard() 
        {
            CreditCard creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = 1234,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Test",
                Currency = CurrencyType.UYU,
                WorkSpace = workspace
            };

            String nameToFind = creditCardAccount.Name;

            workspace.AccountList.Add(creditCardAccount);

            _service.Delete(nameToFind);

            Assert.IsNull(workspace.AccountList.First(x => x.Name == nameToFind));
        }
    }
}
