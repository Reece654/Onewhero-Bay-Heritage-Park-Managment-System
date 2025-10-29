using Supabase;
using Onewhero_Bay_Heritage_Park_Managment_System.Client.Models;
using Microsoft.Extensions.Configuration;

namespace Onewhero_Bay_Heritage_Park_Managment_System.Client.Services;

// Simplified Supabase service - much easier than manual HTTP requests!
public class SupabaseService
{
    private readonly Supabase.Client _supabase;
    private readonly IConfiguration _configuration;

    public SupabaseService(IConfiguration configuration)
    {
        _configuration = configuration;
        
        // Initialize Supabase client with configuration
        var supabaseUrl = _configuration["Supabase:Url"] ?? "https://vmwvwmcxthohpkacmmxg.supabase.co";
        var supabaseKey = _configuration["Supabase:AnonKey"] ?? "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZtd3Z3bWN4dGhvaHBrYWNtbXhnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjA4NjgxMTYsImV4cCI6MjA3NjQ0NDExNn0.cIlGE6V265oDW-3qU2m2OmEazsr9xlNNkFseXXi2tnI";
        
        _supabase = new Supabase.Client(supabaseUrl, supabaseKey);
    }

    // Test connection - much simpler!
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            // Try a simple query to test the connection
            var response = await _supabase
                .From<UserData>()
                .Select("*")
                .Limit(1)
                .Get();
            
            // If we get here without exception, connection is working
            Console.WriteLine("Supabase connection successful - users table accessible");
            return true;
        }
        catch (Exception ex)
        {
            // Log the error for debugging
            Console.WriteLine($"Supabase connection error: {ex.Message}");
            
            // Check if the client is properly initialized
            if (_supabase != null)
            {
                Console.WriteLine("Supabase client is initialized, but table query failed");
                Console.WriteLine("This might be due to table permissions or schema issues");
                return false;
            }
            else
            {
                Console.WriteLine("Supabase client is not properly initialized");
                return false;
            }
        }
    }

    // Get user by email and password - much simpler!
    public async Task<UserData?> GetUserAsync(string email, string password)
    {
        try
        {
            var response = await _supabase
                .From<UserData>()
                .Select("*")
                .Where(x => x.Email == email)
                .Where(x => x.Password == password)
                .Single();

            return response;
        }
        catch
        {
            return null;
        }
    }

    // Get user by email only - for checking if user exists
    public async Task<UserData?> GetUserByEmailAsync(string email)
    {
        try
        {
            var response = await _supabase
                .From<UserData>()
                .Select("*")
                .Where(x => x.Email == email)
                .Single();

            return response;
        }
        catch
        {
            return null;
        }
    }

    // Create new user - much simpler!
    public async Task<UserData?> CreateUserAsync(UserData userData)
    {
        try
        {
            var response = await _supabase
                .From<UserData>()
                .Insert(userData);

            return response.Models.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
}