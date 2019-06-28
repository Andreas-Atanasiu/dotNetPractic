using dotNetLabPractic.DTOs;
using dotNetLabPractic.Models;
using dotNetLabPractic.Services;
using dotNetLabPractic.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    class Tests
    {
        private IOptions<AppSettings> config;
        private IRegisterValidator registerValidator;
        private PostUserDto user;


        [SetUp]
        public void Setup()
        {
            config = Options.Create(new AppSettings
            {
                Secret = "adlkfadlkasfaskldffalaksfaDFLKAjdflkadjfaldkfjsd"
            });

            registerValidator = new RegisterValidator();

            user = new PostUserDto
            {
                FirstName = "user_test_big",
                LastName = "user_test_big",
                Username = "test_test",
                Password = "1234567"
            };
        }

        [Test]
        public void ValidRegisterShouldCreateANewUser()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseInMemoryDatabase(databaseName: "ValidRegisterShouldCreateANewUser")
                .Options;

            using (var context = new UsersDbContext(options))
            {
                var usersService = new UsersService(context, registerValidator, config);

                var added = new PostUserDto
                {
                    FirstName = "test_firstName",
                    LastName = "test_lastName",
                    Username = "test_user",
                    Password = "123456"
                };

                var result =  usersService.Register(added);
                Assert.IsNull(result);

                var newUser = context.Users.Last();
                Assert.AreEqual(added.FirstName, newUser.FirstName);
                Assert.AreEqual(added.LastName, newUser.LastName);
                Assert.AreEqual(added.Username, newUser.Username);

            }
        }

        [Test]
        public void AuthenticateShouldLoginAUser()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(AuthenticateShouldLoginAUser))
              .Options;

            using (var context = new UsersDbContext(options))
            {
                var usersService = new UsersService(context, registerValidator, config);
                var added = new PostUserDto

                {
                    FirstName = "test_first",
                    LastName = "test_last",
                    Username = "test_user",
                    Password = "1234567"
                };
                var result = usersService.Register(added);
                var authenticated = new PostLoginDto
                {
                    Username = "test_user",
                    Password = "1234567"
                };

                var authresult = usersService.Authenticate(added.Username, added.Password);
                Assert.IsNotNull(authresult);
                Assert.AreEqual(authenticated.Username, authresult.Username);
            }
        }

        [Test]
        public void GetAllShouldReturnAllUsers()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetAllShouldReturnAllUsers))
              .Options;

            using (var context = new UsersDbContext(options))
            {
                var usersService = new UsersService(context, registerValidator, config);
                var added1 = new PostUserDto

                {
                    FirstName = "aaaaa",
                    LastName = "bbbbb",
                    Username = "aaabbb",
                    Password = "1234567"
                };
                var added2 = new PostUserDto

                {
                    FirstName = "ccccc",
                    LastName = "ddddd",
                    Username = "cccddd",
                    Password = "1234567"
                };

                usersService.Register(added1);
                usersService.Register(added2);

                int numberOfElements = usersService.GetAll().Count();

                Assert.NotZero(numberOfElements);
                Assert.AreEqual(2, numberOfElements);

            }
        }

        [Test]
        public void GetUserByIdShouldReturnTheCorrectUser()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
             .UseInMemoryDatabase(databaseName: nameof(GetUserByIdShouldReturnTheCorrectUser))
             .Options;

            using (var context = new UsersDbContext(options))
            {

                var UsersService = new UsersService(context, registerValidator, config);

                UsersService.Register(user);

                var addedUser = context.Users.Last();

                User userById = UsersService.GetUserById(addedUser.Id);

                Assert.AreEqual(userById.Id, addedUser.Id);
            }
        }

        [Test]
        public void UpdateUserShouldUpdateUserDetails()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
             .UseInMemoryDatabase(databaseName: nameof(UpdateUserShouldUpdateUserDetails))
             .Options;

            using (var context = new UsersDbContext(options))
            {
                var UsersService = new UsersService(context, registerValidator, config);

                UsersService.Register(user);

                var addedUser = context.Users.Last();

                int id = addedUser.Id;

                //int id = (from user in context.Users orderby user.Id descending select user.Id).First();
                //string username = (from user in context.Users orderby user.Id descending select user.Username).First();

                Assert.AreEqual(addedUser.Username, user.Username);

                User newUserDetails = new User
                {
                    FirstName = "newnew",
                    LastName = "newnew",
                    Username = "newnew",
                    Password = "1234567"
                };

                context.Entry(addedUser).State = EntityState.Detached;

                UsersService.UpdateUserNoRoleChange(id, newUserDetails);

                string newUsername = (from user in context.Users orderby user.Id descending select user.Username).First();

                Assert.AreEqual(newUsername, newUserDetails.Username);
            }
        }

        [Test]
        public void DeleteShouldDeleteUser()
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
             .UseInMemoryDatabase(databaseName: nameof(DeleteShouldDeleteUser))
             .Options;

            using (var context = new UsersDbContext(options))
            {

                var UsersService = new UsersService(context, registerValidator, config);

                var newUser = new PostUserDto
                {
                    FirstName = "toDel",
                    LastName = "toDel",
                    Password = "1234567",
                    Username = "toDelTest"
                };

                UsersService.Register(newUser);

                User addedUser = context.Users.Last();

                context.Entry(addedUser).State = EntityState.Detached;

                //var addedUser = context.Users.Where(u => u.Username == "alina3").FirstOrDefault();

                UsersService.DeleteUser(addedUser.Id);

                int users = UsersService.GetAll().Count();

                Assert.Zero(users);
            }
        }
    }
}
