using Supabase;
using Onewhero_Bay_Heritage_Park_Managment_System.Client.Models;

namespace Onewhero_Bay_Heritage_Park_Managment_System.Client.Services;

// Simplified Supabase service - much easier than manual HTTP requests!
public class SupabaseService
{
    private readonly Supabase.Client _supabase;

    public SupabaseService()
    {
        // Initialize Supabase client - much simpler than manual HTTP!
        _supabase = new Supabase.Client(
            "https://vmwvwmcxthohpkacmmxg.supabase.co",
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZtd3Z3bWN4dGhvaHBrYWNtbXhnIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjA4NjgxMTYsImV4cCI6MjA3NjQ0NDExNn0.cIlGE6V265oDW-3qU2m2OmEazsr9xlNNkFseXXi2tnI"
        );
    }

    // Test connection - much simpler!
    public async Task<bool> TestConnectionAsync()
    {
        try
        {
            var response = await _supabase
                .From<UserData>()
                .Select("*")
                .Limit(1)
                .Get();
            return true;
        }
        catch
        {
            return false;
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