// Marc
using System.Text.Json;
using System.IO;

public class Inspection
{
    public string Type { get; set; }
    public DateTime DateScheduled { get; set; }
    public string PropertyID { get; set; }
    public string PropertyAddress { get; set; }
    public string InspectorName { get; set; }

    public string ConditionNotes { get; set; }
    public bool ReportGenerated { get; set; }
    public string ReportContent { get; set; }

    public void Schedule(string type, DateTime date, string propertyID, string propertyAddress, string inspector)
    {
        Type = type;
        DateScheduled = date;
        PropertyID = propertyID;
        PropertyAddress = propertyAddress;
        InspectorName = inspector;
        ReportGenerated = false;
        ConditionNotes = "";
        ReportContent = "";

        Console.WriteLine($"Scheduled {type} inspection for {propertyAddress} on {date.ToShortDateString()} by {inspector}.");
    }

    public void EvaluateCondition(string notes)
    {
        ConditionNotes = notes;
        Console.WriteLine("Condition evaluated and saved.");
    }

    public void GenerateReport()
    {
        if (string.IsNullOrWhiteSpace(ConditionNotes))
        {
            Console.WriteLine("Cannot generate report without evaluation notes.");
            return;
        }

        ReportContent = $"Inspection Report\nType: {Type}\nProperty: {PropertyAddress} (ID: {PropertyID})\nDate: {DateScheduled}\nInspector: {InspectorName}\n\nCondition Notes:\n{ConditionNotes}";
        ReportGenerated = true;

        Console.WriteLine("Report generated.");
    }

    public override string ToString()
    {
        return $"Type: {Type}, Date: {DateScheduled.ToShortDateString()}, Property: {PropertyAddress}, Inspector: {InspectorName}, Report: {(ReportGenerated ? "Yes" : "No")}";
    }
}



public class InspectionManager
{
    private List<Inspection> inspections = new List<Inspection>();
    private readonly string filePath = "inspections.json";

    public InspectionManager()
    {
        LoadInspections();
    }

    public void AddInspection(Inspection inspection)
    {
        inspections.Add(inspection);
        SaveInspections();
        Console.WriteLine($"Inspection scheduled for {inspection.PropertyAddress}.");
    }

    public void RemoveInspection(string propertyID, DateTime date)
    {
        var match = inspections.FirstOrDefault(i => i.PropertyID == propertyID && i.DateScheduled.Date == date.Date);
        if (match != null)
        {
            inspections.Remove(match);
            SaveInspections();
            Console.WriteLine("Inspection removed.");
        }
        else
        {
            Console.WriteLine("Inspection not found.");
        }
    }


    public List<Inspection> GetAllInspections() => new List<Inspection>(inspections);

    public List<Inspection> GetUpcomingInspections()
    {
        return inspections
            .Where(i => i.DateScheduled >= DateTime.Today)
            .OrderBy(i => i.DateScheduled)
            .ToList();
    }

    private void SaveInspections()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(inspections, options));
    }

    private void LoadInspections()
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            inspections = JsonSerializer.Deserialize<List<Inspection>>(json) ?? new List<Inspection>();
        }
        else
        {
            inspections = new List<Inspection>();
            SaveInspections(); // Creates the file initially
        }
    }
}
