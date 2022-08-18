

using backend_api.Models;
using MongoDB.Driver;

namespace backend_api.Repositories;

public class UserRepository : IUserRepository
{
    //use IMongoCollection to access our collection of tweet objects called _tweets from MongoDb
    private readonly IMongoCollection<User> _users;
    public UserRepository(ITweeterDatabaseSettings settings)
    {
        //Pull the "Connection String" from the appsettings.json and initiate a new instance of the MongoClient class
        var client = new MongoClient(settings.ConnectionString);
        //Create a database using the "Database Name" from appsettings.json
        var database = client.GetDatabase(settings.DatabaseName);

        //Get the collection of Tweets from the database using the tweet collection name (save in _tweets variable)
        _users = database.GetCollection<User>(settings.UserCollectionName);
    }

    public async Task<User> CreateUser(User newUser)
    {
        newUser.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
        try {
            await _users.InsertOneAsync(newUser);
        } catch (Exception ex){
            Console.WriteLine(ex.ToString());
        }
        return newUser;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        var findUsers = await _users.FindAsync(user => true);
        var foundUser = findUsers.ToList().FirstOrDefault(user => user.Username == username);
        return foundUser;
        
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var allUsers = await _users.FindAsync(user => true);
        return allUsers.ToList();
    }

    public User FindUser(string username)
    {
        var findUsers = _users.Find(user => true);
        var foundUser = findUsers.ToList().FirstOrDefault(user => user.Username == username);
        return foundUser;
    }

}