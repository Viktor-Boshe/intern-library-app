namespace LibraryApiService.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetUsers();
        void AddUser(Users user);
        IEnumerable<Users> GetUser(string username);
        void ResetPassword(Users user);
    }
}
