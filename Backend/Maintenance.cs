// Marc
using System.Text.Json;
using System.IO;

public class Maintenance
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool Completed { get; set; }
    public int Priority { get; set; }
    public DateTime? CompletionTime { get; set; }

    public void CreateMaintenance(string title, string description, DateTime dueDate, int priority)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        Completed = false;
        CompletionTime = null;

        Console.WriteLine("Maintenance task created:");
        Console.WriteLine(ToString());
    }


    public void Update(bool markAsCompleted)
    {
        if (markAsCompleted && !Completed)
        {
            Completed = true;
            CompletionTime = DateTime.Now;
            Console.WriteLine($"Maintenance task \"{Title}\" marked as complete on {CompletionTime}.");
        }
        else if (!markAsCompleted && Completed)
        {
            Completed = false;
            CompletionTime = null;
            Console.WriteLine($"Maintenance task \"{Title}\" marked as incomplete.");
        }
        else
        {
            Console.WriteLine($"Maintenance task \"{Title}\" is already {(Completed ? "complete" : "incomplete")}.");
        }
    }

    public override string ToString()
    {
        return $"Title: {Title}, Priority: {Priority}, Due: {DueDate.ToShortDateString()}, " +
               $"Completed: {Completed}, Completed On: {(CompletionTime?.ToString("g") ?? "N/A")}";
    }
}

public class MaintenanceManager
{
    private List<Maintenance> tasks = new List<Maintenance>();
    private readonly string filePath = "maintenance.json";

    public MaintenanceManager()
    {
        LoadTasks();
    }

    public void AddTask(Maintenance task)
    {
        tasks.Add(task);
        SaveTasks();
        Console.WriteLine($"Task \"{task.Title}\" added.");
    }

    public void UpdateTask(string title, bool markAsCompleted)
    {
        var task = tasks.FirstOrDefault(t => t.Title == title);
        if (task != null)
        {
            task.Update(markAsCompleted);
            SaveTasks();
        }
        else
        {
            Console.WriteLine($"Task \"{title}\" not found.");
        }
    }

    public void RemoveTask(string title)
    {
        var task = tasks.FirstOrDefault(t => t.Title == title);
        if (task != null)
        {
            tasks.Remove(task);
            SaveTasks();
            Console.WriteLine($"Task \"{title}\" removed.");
        }
        else
        {
            Console.WriteLine($"Task \"{title}\" not found.");
        }
    }

    public List<Maintenance> GetAllTasks() => new List<Maintenance>(tasks);

    private void SaveTasks()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(tasks, options));
    }

    private void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            tasks = JsonSerializer.Deserialize<List<Maintenance>>(json) ?? new List<Maintenance>();
        }
        else
        {
            tasks = new List<Maintenance>();
            SaveTasks(); // Creates the file initially
        }
    }
    // Filter jazz
    public List<Maintenance> GetOverdueTasks()
    {
        return tasks
            .Where(t => !t.Completed && t.DueDate < DateTime.Now)
            .OrderBy(t => t.DueDate)
            .ToList();
    }

    public List<Maintenance> GetHighPriorityTasks()
    {
        return tasks
            .Where(t => !t.Completed)
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToList();
    }

    public List<Maintenance> GetUpcomingTasks(int days)
    {
        DateTime now = DateTime.Now;
        DateTime cutoff = now.AddDays(days);

        return tasks
            .Where(t => !t.Completed && t.DueDate <= cutoff && t.DueDate >= now)
            .OrderBy(t => t.DueDate)
            .ToList();
    }

    public List<Maintenance> SortByDueDate()
    {
        return tasks
            .OrderBy(t => t.DueDate)
            .ToList();
    }

    public Maintenance CreateMaintenancePrompt()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        DateTime userDateTime;
        Console.WriteLine("Please enter a date (e.g., MM/DD/YYY):");

        string input = Console.ReadLine();

        while (!DateTime.TryParse(input, out userDateTime))
        {
            Console.WriteLine("That format wasn't quite right. Please enter the date like this: 4/25/2025");
            input = Console.ReadLine();
        }

        Console.Write("Priority (1/10, 10 is highest): ");
        int priority = int.Parse(Console.ReadLine());

        return new Maintenance
        {
            Title = title,
            Description = description,
            DueDate = userDateTime,
            Priority = priority,
            Completed = false,
            CompletionTime = null
        };
    }

}


// Example use

// var maintenanceManager = new MaintenanceManager();

// var task = new Maintenance();
// task.CreateMaintenance("Check HVAC", "Seasonal inspection", DateTime.Today.AddDays(10), 1);

// maintenanceManager.AddTask(task);
// maintenanceManager.UpdateTask("Check HVAC", true); // Mark as completed

// var allTasks = maintenanceManager.GetAllTasks();
// foreach (var t in allTasks)
// {
//     Console.WriteLine(t);
// }







// Console.WriteLine("\nHigh Priority Tasks:");
// foreach (var t in maintenanceManager.GetHighPriorityTasks())
//     Console.WriteLine(t);

// Console.WriteLine("\nOverdue Tasks:");
// foreach (var t in maintenanceManager.GetOverdueTasks())
//     Console.WriteLine(t);

// Console.WriteLine("\nTasks Due in Next 7 Days:");
// foreach (var t in maintenanceManager.GetUpcomingTasks(7))
//     Console.WriteLine(t);
