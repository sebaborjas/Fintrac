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
        private WorkspaceService _workspaceService;
        private UserService _userService;

        private Workspace _workspace;
        private User _user;


        [TestInitialize]
        public void SetUp()
        {
            FintracContext _database = TestContextFactory.CreateContext();
            _service = new AccountService(_database);
            _workspaceService = new WorkspaceService(_database);
            _userService = new UserService(_database);

            _user = new User { Email = "test@test.com", Name = "Test", LastName = "Test", Password = "12345678901" };

            _workspace = new Workspace(_user, "Test");

            _userService.Add(_user);
            _workspaceService.Add(_user, _workspace);

       
        }

        [TestMethod]
        public void AddPersonalAccount() 
        {
            PersonalAccount personalAccount = new PersonalAccount { 
                Name = "Test", 
                StartingAmount = 1000, 
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace 
            };
            _service.Add(personalAccount.WorkSpace, personalAccount);

            Assert.AreEqual(personalAccount, _service.Get(personalAccount.WorkSpace,personalAccount.Name));
        }

        [TestMethod]
        public void AddCreditCard() 
        {
            CreditCard creditCardAccount = new CreditCard {
                BankName = "Santander", 
                LastDigits = "1234", 
                AvailableCredit = 10000,
                DeadLine = 26, 
                Name = "Credit Santander", 
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            _service.Add(creditCardAccount.WorkSpace, creditCardAccount);
            Assert.AreEqual(creditCardAccount, _service.Get(creditCardAccount.WorkSpace, creditCardAccount.Name));
        }

        [TestMethod]
        public void GetAccount() 
        {
            String name = "Credit Santander";

            Account creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = "1234",
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = name,
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            _service.Add(creditCardAccount.WorkSpace, creditCardAccount);

            Assert.AreEqual(creditCardAccount, _service.Get(creditCardAccount.WorkSpace, creditCardAccount.Name));
        }

        [TestMethod]
        public void GetAccountDoesntExists() 
        {
            Account account = _service.Get(_workspace ,"Santander");
            Assert.IsNull(account);

        }

        [TestMethod]
        public void ModifyPersonalAccount() 
        {
            PersonalAccount personalAccount = new PersonalAccount
            {
                Name = "Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            Account personalAccountModified = new PersonalAccount
            {
                Name = personalAccount.Name,
                StartingAmount = personalAccount.StartingAmount,
                Currency = personalAccount.Currency,
                WorkSpace = personalAccount.WorkSpace
            };

            _service.Add(personalAccount.WorkSpace,personalAccount);    

            String accountName = personalAccount.Name;

            String newName = "BROU";

            personalAccountModified.Name = newName;

            _service.Modify(accountName, personalAccountModified);

            Account account = _service.Get(personalAccountModified.WorkSpace,newName);

            Assert.AreEqual(personalAccountModified, account);

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
                WorkSpace = _workspace
            };

            PersonalAccount secondPersonalAccount = new PersonalAccount
            {
                Name = "Second Test",
                StartingAmount = 1000,
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            String newName = "Test";

            PersonalAccount accountModified = new PersonalAccount
            {
                Name = newName,
                StartingAmount = secondPersonalAccount.StartingAmount,
                Currency = secondPersonalAccount.Currency,
                WorkSpace = secondPersonalAccount.WorkSpace
            };

            _service.Add( firstPersonalAccount.WorkSpace,firstPersonalAccount);
            _service.Add(secondPersonalAccount.WorkSpace, secondPersonalAccount);

            String nameToFind = secondPersonalAccount.Name;

            _service.Modify(nameToFind, accountModified);

        }

        [TestMethod]
        public void ModifyCreditCard() 
        {
            CreditCard creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = "1234",
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Credit Santander",
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            String nameToFind = creditCardAccount.Name;

            _service.Add(creditCardAccount.WorkSpace, creditCardAccount);

            String newName = "Credit Oca";
            string newLastDigits = "4321";

            CreditCard creditCardAccountModified = new CreditCard
            {
                BankName = "Santander",
                LastDigits = newLastDigits,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = newName,
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            _service.Modify(nameToFind, creditCardAccountModified);

            Account creditCard = _service.Get(creditCardAccount.WorkSpace, newName);

            Assert.AreEqual(creditCardAccountModified, creditCard);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountAlreadyExistsException))]
        public void ModifyCreditCardAlreadyExists() 
        {
            CreditCard creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = "1234",
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Credit Santander",
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            CreditCard secondCreditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = "1234",
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Credit Brou",
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            String nameToFind = secondCreditCardAccount.Name;

            _service.Add(creditCardAccount.WorkSpace, creditCardAccount);
            _service.Add(creditCardAccount.WorkSpace, secondCreditCardAccount);

            String newName = "Credit Santander";
            string newLastDigits = "4321";

            CreditCard creditCardAccountModified = new CreditCard
            {
                BankName = "Santander",
                LastDigits = newLastDigits,
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = newName,
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
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
                WorkSpace = _workspace
            };

            String nameToFind = personalAccount.Name;

            _service.Add(personalAccount.WorkSpace, personalAccount);

            _service.Delete(personalAccount.WorkSpace, nameToFind);

            Assert.IsNull(_service.Get(personalAccount.WorkSpace, nameToFind));
        }

        [TestMethod]
        public void DeleteCreditCard() 
        {
            CreditCard creditCardAccount = new CreditCard
            {
                BankName = "Santander",
                LastDigits = "1234",
                AvailableCredit = 10000,
                DeadLine = 26,
                Name = "Test",
                Currency = CurrencyType.UYU,
                WorkSpace = _workspace
            };

            String nameToFind = creditCardAccount.Name;

            _service.Add(creditCardAccount.WorkSpace, creditCardAccount);

            _service.Delete(creditCardAccount.WorkSpace, nameToFind);

            Assert.IsNull(_service.Get(creditCardAccount.WorkSpace, nameToFind));
        }
    }
}
