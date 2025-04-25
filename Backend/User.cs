// Marc
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;


public class User
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

public class Tenant : User
{
    public int Payment { get; set; }
    public bool Employed { get; set; }
    public int MonthlyIncome { get; set; }
    public int Occupants { get; set; }
    public bool Convicted { get; set; }
}

public class Staff : User
{
    // Add staff specific properties if needed
}

public class UserManager
{
    private List<User> users = new List<User>();
    private readonly string filePath = "users.json";

    public UserManager()
    {
        LoadUsers();
    }

    public void AddUser(User user)
    {
        if (users.Any(u => u.Email == user.Email))
        {
            Console.WriteLine($"User with email {user.Email} already exists.");
            return;
        }

        users.Add(user);
        SaveUsers(); // Save after adding
        Console.WriteLine($"User {user.Name} added successfully.");
    }

    public bool RemoveUser(string email)
    {
        var userToRemove = users.FirstOrDefault(u => u.Email == email);
        if (userToRemove != null)
        {
            users.Remove(userToRemove);
            SaveUsers();
            Console.WriteLine($"User with email {email} removed.");
            return true;
        }

        Console.WriteLine($"User with email {email} not found.");
        return false;
    }

    public List<User> GetAllUsers() => new List<User>(users);
    public List<Tenant> GetAllTenants() => users.OfType<Tenant>().ToList();
    public List<Staff> GetAllStaff() => users.OfType<Staff>().ToList();

    private void SaveUsers()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter(), new UserJsonConverter() }
        };

        File.WriteAllText(filePath, JsonSerializer.Serialize(users, options));
    }

    private void LoadUsers()
    {
        if (File.Exists(filePath))
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter(), new UserJsonConverter() }
            };

            string json = File.ReadAllText(filePath);
            users = JsonSerializer.Deserialize<List<User>>(json, options) ?? new List<User>();
        }
    }
}

public class UserJsonConverter : JsonConverter<User>
{
    public override User Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        string? type = root.GetProperty("Type").GetString();

        Type targetType = type switch
        {
            "Tenant" => typeof(Tenant),
            "Staff" => typeof(Staff),
            _ => typeof(User)
        };

        return (User)JsonSerializer.Deserialize(root.GetRawText(), targetType, options)!;
    }

    public override void Write(Utf8JsonWriter writer, User value, JsonSerializerOptions options)
    {
        var typeDiscriminator = value switch
        {
            Tenant => "Tenant",
            Staff => "Staff",
            _ => "User"
        };

        var json = JsonSerializer.SerializeToElement(value, value.GetType(), options);
        writer.WriteStartObject();
        writer.WriteString("Type", typeDiscriminator);

        foreach (var property in json.EnumerateObject())
        {
            property.WriteTo(writer);
        }

        writer.WriteEndObject();
    }
}


public static class TenantManager
{
    public static Tenant CreateTenantFromInput()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Phone Number: ");
        string phone = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Monthly Payment Amount: ");
        int payment = int.Parse(Console.ReadLine());

        Console.Write("Are you currently employed? (yes/no): ");
        bool employed = Console.ReadLine().ToLower() == "yes";

        Console.Write("Monthly Income: ");
        int income = int.Parse(Console.ReadLine());

        Console.Write("How many total occupants (including yourself)?: ");
        int occupants = int.Parse(Console.ReadLine());

        Console.Write("Have you ever been convicted of a crime? (yes/no): ");
        bool convicted = Console.ReadLine().ToLower() == "yes";

        return new Tenant
        {
            Name = name,
            PhoneNumber = phone,
            Email = email,
            Payment = payment,
            Employed = employed,
            MonthlyIncome = income,
            Occupants = occupants,
            Convicted = convicted
        };
    }
}