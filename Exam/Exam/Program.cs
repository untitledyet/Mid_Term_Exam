using System.Text.Json;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            string json =
                File.ReadAllText(
                    "/Users/meskhdav/Library/CloudStorage/OneDrive-TheStarsGroup/Desktop/Mid_Term_Exam/Exam/Exam/files/Person.json");

            Person person = JsonSerializer.Deserialize<Person>(json);


            Console.Write("შეიყვანეთ ბარათის ნომერი: ");
            string cardNumber = "";
            int count = 0;

            while (cardNumber.Length < 16)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (char.IsDigit(key.KeyChar))
                {
                    cardNumber += key.KeyChar;
                    count++;
                    Console.Write(key.KeyChar);
                    if (count % 4 == 0 && count != 16)
                    {
                        Console.Write("-");
                    }
                }
            }

            Console.WriteLine();


            /*
            // Display some information
            Console.WriteLine($"Name: {person.FirstName} {person.LastName}");
            Console.WriteLine($"Card Number: {person.CardDetails.CardNumber}");
            Console.WriteLine($"Expiration Date: {person.CardDetails.ExpirationDate}");
            Console.WriteLine($"CVC: {person.CardDetails.CVC}");
            Console.WriteLine($"Pin Code: {person.PinCode}");
            Console.WriteLine("Transaction History:");
            foreach (var transaction in person.TransactionHistory)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"  Date: {transaction.TransactionDate}");
                Console.WriteLine($"  Type: {transaction.TransactionType}");
                Console.WriteLine($"  Amount (GEL): {transaction.AmountGEL}");
                Console.WriteLine($"  Amount (USD): {transaction.AmountUSD}");
                Console.WriteLine($"  Amount (EUR): {transaction.AmountEUR}");
            } */
        }
    }
}