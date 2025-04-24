// Ben

namespace PropertyManagement
{

public class Property
{
    public int PropertyID { get; set; }
    public string Address { get; set; }
    public string PropertyImage { get; set; }
    public int Bedrooms { get; set; }
    public int Baths { get; set; }
    public int SqFt { get; set; }
    public double Acres { get; set; }
    public int Garage { get; set; }
    public int MonthlyRent { get; set; }
    public int ContractLength { get; set; }
    public int FloorLevels { get; set; }
    public static List<Property> ListOfProperties = new List<Property>(); // List to hold properties

    public Property(int propertyID, string address, string propertyImage, int bedrooms, int baths, int sqFt,
                    double acres, int garage, int monthlyRent, int contractLength, int floorLevels)
    {
        PropertyID = propertyID;
        Address = address;
        PropertyImage = propertyImage;
        Bedrooms = bedrooms;
        Baths = baths;
        SqFt = sqFt;
        Acres = acres;
        Garage = garage;
        MonthlyRent = monthlyRent;
        ContractLength = contractLength;
        FloorLevels = floorLevels;
    }

    public static Property CreateProperty()
{
    Console.WriteLine("Please enter the property ID:");
    int propertyID = int.Parse(Console.ReadLine());

    Console.Write("Enter the property address: ");
    string address = Console.ReadLine();

    Console.Write("Enter the image file name or URL: ");
    string image = Console.ReadLine();

    Console.Write("Enter number of bedrooms: ");
    int bedrooms = int.Parse(Console.ReadLine());

    Console.Write("Enter number of bathrooms: ");
    int baths = int.Parse(Console.ReadLine());

    Console.Write("Enter square footage: ");
    int sqft = int.Parse(Console.ReadLine());

    Console.Write("Enter number of acres: ");
    double acres = double.Parse(Console.ReadLine());

    Console.Write("Enter number of garage spaces: ");
    int garage = int.Parse(Console.ReadLine());

    Console.Write("Enter monthly rent: ");
    int rent = int.Parse(Console.ReadLine());

    Console.Write("Enter contract length (months): ");
    int contract = int.Parse(Console.ReadLine());

    Console.Write("Enter number of floor levels: ");
    int levels = int.Parse(Console.ReadLine());

    return new Property(propertyID, address, image, bedrooms, baths, sqft, acres, garage, rent, contract, levels);
}
    
    public static void LoadSampleProperties()
{
    ListOfProperties.Add(new Property(1, "123 Maple St", "image1.jpg", 3, 2, 1500, 0.25, 2, 1800, 12, 2));
    ListOfProperties.Add(new Property(2, "456 Oak Ave", "image2.jpg", 4, 3, 2200, 0.4, 3, 2400, 24, 2));
    ListOfProperties.Add(new Property(3, "789 Pine Rd", "image3.jpg", 2, 1, 1100, 0.15, 1, 1300, 6, 1));
    ListOfProperties.Add(new Property(4, "321 Birch Blvd", "image4.jpg", 5, 4, 3000, 0.5, 3, 3100, 36, 3));
    ListOfProperties.Add(new Property(5, "654 Cedar Ct", "image5.jpg", 3, 2, 1600, 0.2, 2, 1700, 18, 2));
    
    Console.WriteLine("Sample properties loaded.");
}

    public static void AddProperty(Property property)
    {
        // Add the property to the list
        ListOfProperties.Add(property);
        Console.WriteLine("Property added successfully.");
    }

    // public static void ViewProperty(int propertyID)
    // {
        
        
    // }

    public void DisplayPropertyDetails()
{
    Console.WriteLine("----- Property Details -----");
    Console.WriteLine($"Property ID: {PropertyID}");
    Console.WriteLine($"Address: {Address}");
    Console.WriteLine($"Image: {PropertyImage}");
    Console.WriteLine($"Bedrooms: {Bedrooms}");
    Console.WriteLine($"Bathrooms: {Baths}");
    Console.WriteLine($"Square Feet: {SqFt}");
    Console.WriteLine($"Acres: {Acres}");
    Console.WriteLine($"Garage Spaces: {Garage}");
    Console.WriteLine($"Monthly Rent: ${MonthlyRent}");
    Console.WriteLine($"Contract Length: {ContractLength} months");
    Console.WriteLine($"Floor Levels: {FloorLevels}");
    Console.WriteLine("----------------------------");
}


     public static void ListALLProperties(int propertyID)
    {
        

    }


    public void RemoveProperty()
    {


    }


}

}