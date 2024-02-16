using NLog.Fluent;

namespace Exam;

public class Logger
{
    private static readonly string logFilePath = "/Users/meskhdav/Library/CloudStorage/OneDrive-TheStarsGroup/Desktop/Mid_Term_Exam/Exam/Exam/files/Log.txt";

    public static void Log(string message)
    {
        try
        {
            
            if (!File.Exists(logFilePath))
            {
                using (FileStream fs = File.Create(logFilePath))
                {
                    
                }
            }

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"[{DateTime.Now.ToString()}] {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while logging: {ex.Message}");

        }
    }

   
}
