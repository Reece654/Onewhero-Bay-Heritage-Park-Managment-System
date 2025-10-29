using Supabase.Postgrest.Models;        // For BaseModel inheritance to work with Supabase database
using Supabase.Postgrest.Attributes;   // For [Column] attributes to map C# properties to database columns

namespace Onewhero_Bay_Heritage_Park_Managment_System.Client.Models;

// UserData model that works with Supabase
// This class defines the structure for storing visitor information in the database.
// It acts as a blueprint that tells the system what user data to collect and how to store it.
// When visitors register or log in, their information is organized using this UserData template.
[Table("users")]  // Map to the 'users' table in the database
public class UserData : BaseModel
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("email")]
    public string Email { get; set; } = "";
    
    [Column("password")]
    public string Password { get; set; } = ""; // Note: Hash this in production!
    
    [Column("first_name")]
    public string FirstName { get; set; } = "";
    
    [Column("last_name")]
    public string LastName { get; set; } = "";
    
    [Column("interests")]
    public List<string> Interests { get; set; } = new List<string>();
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
