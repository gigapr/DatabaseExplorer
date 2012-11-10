using System.Linq;
using DatabaseSchemaReader.Website.Mappers.Interfaces;
using DatabaseSchemaReader.Website.Model;
using DatabaseSchemaReader.Website.Services.Interfaces;
using DatabaseSchemaReader.Website.ViewModels;
using Raven.Client;

namespace DatabaseSchemaReader.Website.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDocumentStore _documentStore;
        private readonly IUserMapper _userMapper;

        public AccountService(IDocumentStore documentStore, IUserMapper userMapper)
        {
            _documentStore = documentStore;
            _userMapper = userMapper;
        }

        public bool SignIn(SignIn signIn)
        {
            bool isFound;

            var user = _userMapper.Map(signIn);

            using (var session = _documentStore.OpenSession())
            {
                isFound = session.Load<User>(user.Email, user.Password).Any();
            }

            return isFound;
        }

        public bool RegisterUser(Register register)
        {
            var succeeded = false;

            var user = _userMapper.Map(register);

            using (var session = _documentStore.OpenSession())
            {
                var alreadyregistered = (session.Query<User>()
                                            .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                                            .Count(u => u.Email == register.Email)) > 0;

                if (!alreadyregistered)
                {
                    session.Store(user);
                    session.SaveChanges();

                    succeeded = true;
                }

            }
            return succeeded;
        } 
    }
}