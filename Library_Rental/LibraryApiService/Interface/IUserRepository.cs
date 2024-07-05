namespace LibraryApiService.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetUsers();
        void AddUser(Users user);
    }
}
