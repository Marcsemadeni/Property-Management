// Ben

public class Property
{
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


    public Property(string address, string propertyImage, int bedrooms, int baths, int sqFt,
                    double acres, int garage, int monthlyRent, int contractLength, int floorLevels)
    {
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


    public void RemoveProperty()
    {


    }


}

