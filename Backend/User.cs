// Marc


public class User
{
    public string name { get; set; }
    public string phone_number { get; set; }
    public string email { get; set; }
    
    public void AddUser()
    {

    }
    
    public void RemoveUser()
    {

    }
}

public class Tenant : User
{
    public int payment { get; set; }
    public bool employed { get; set; }
    public int monthlyIncome { get; set; }
    public int occupants { get; set; }
    public bool convicted { get; set; }
    
    // Tenant-specific methods can be added here
}

public class Staff : User
{

}