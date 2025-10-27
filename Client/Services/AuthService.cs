using Onewhero_Bay_Heritage_Park_Managment_System.Client.Models;

namespace Onewhero_Bay_Heritage_Park_Managment_System.Client.Services;

// Simplified AuthService 
public class AuthService
{
    private readonly SupabaseService _supabaseService;
    private bool isLoggedIn = false;
    private UserData? currentUser;

    // Constructor - inject SupabaseService instead of HttpClient
    public AuthService(SupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    // Public properties
    public bool IsLoggedIn => isLoggedIn;
    public UserData? CurrentUser => currentUser;

    // Simple login method - much cleaner!
    public async Task<bool> Login(string email, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            // Use SupabaseService instead of manual HTTP requests
            var user = await _supabaseService.GetUserAsync(email, password);
            
            if (user != null)
            {
                isLoggedIn = true;
                currentUser = user;
                return true;
            }
            
            return false;
        }
        catch
        {
            return false;
        }
    }

    // Simple registration method - much cleaner!
    public async Task<(bool Success, string ErrorMessage)> Register(string email, string password, string firstName, string lastName, List<string> interests)
    {
        try
        {
            // Check if user already exists
            var existingUser = await _supabaseService.GetUserByEmailAsync(email);
            if (existingUser != null)
            {
                return (false, "Email is already in use");
            }

            var userData = new UserData
            {
                Email = email,
                Password = password, // Note: In production, hash this password!
                FirstName = firstName,
                LastName = lastName,
                Interests = interests,
                CreatedAt = DateTime.UtcNow
            };

            // Use SupabaseService instead of manual HTTP requests
            var createdUser = await _supabaseService.CreateUserAsync(userData);
            
            if (createdUser != null)
            {
                isLoggedIn = true;
                currentUser = createdUser;
                return (true, "");
            }
            
            return (false, "Registration failed. Please try again.");
        }
        catch (Exception ex)
        {
            // Log the actual error for debugging
            Console.WriteLine($"Registration error: {ex.Message}");
            return (false, "Registration failed. Please try again.");
        }
    }

    // Simple logout method
    public void Logout()
    {
        isLoggedIn = false;
        currentUser = null;
    }

    // Simple connection test
    public async Task<bool> TestDatabaseConnection()
    {
        return await _supabaseService.TestConnectionAsync();
    }
}
