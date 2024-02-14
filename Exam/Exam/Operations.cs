using System.Text.Json;

namespace Exam;

public class Operations
{
    public static void DoAction(int actionNumber)
    {
        if (actionNumber == 1)
        {
            CurrentBalance();
        }
        else if (actionNumber == 2)
        {
            WIthdrawMonay();
        }
        else if (actionNumber == 3)
        {
            TransactionHistory();
        }
        else if (actionNumber == 4)
        {
            DepositeMonay();
        }
        else if (actionNumber == 5)
        {
            PinChange();
        }
        else if (actionNumber == 6)
        {
        }
        else if (actionNumber == 7)
        {
            Console.WriteLine("დასრულება");
        }
        else
        {
            Console.WriteLine("Something went wrong");
        }
    }

    public static void CurrentBalance()
    {
        string json = Program.LoadObject();
        Person person = JsonSerializer.Deserialize<Person>(json);

        if (person.TransactionHistory.Count > 0)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("არჩეული მოქმედება - ნაშთის ნახვა ");
                Console.WriteLine($"ნაშთი ანგარიშზე: ");
                Console.WriteLine($"GEL - {person.TransactionHistory[^1].AmountGEL}");
                Console.WriteLine($"USD - {person.TransactionHistory[^1].AmountUSD}");
                Console.WriteLine($"EUR - {person.TransactionHistory[^1].AmountEUR}");

                Program.InsertTransaction("DepositCeck");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Handle the exception gracefully
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"ნაშთი ანგარიშზე: ");
            Console.WriteLine($"GEL - 0");
            Console.WriteLine($"USD - 0");
            Console.WriteLine($"EUR - 0");
            Program.InsertTransaction("DepositCeck");
        }
    }

    public static void PinChange()
    {
        Console.WriteLine();
        Console.WriteLine("არჩეული მოქმედება - პინის შეცვლა ");
        Console.Write("შეიყვანეთ ახალი პინი: ");
        string pin = "";
        int count = 0;

        while (pin.Length < 4)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (char.IsDigit(key.KeyChar))
            {
                pin += key.KeyChar;
                count++;
                Console.Write(key.KeyChar);
            }
            else if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
            {
                pin = pin.Substring(0, pin.Length - 1);


                if (pin.Length < 4)
                {
                    count--;
                    Console.Write("\b \b");
                }
            }
        }

        try
        {
            Program.InsertTransaction(newTransactionType: "ChangePin");

            string newJson = Program.LoadObject();
            Person neLoadedPerson = JsonSerializer.Deserialize<Person>(newJson);
            if (neLoadedPerson.PinCode == pin)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("პინი წარმატებით შეიცვალა  ✅ ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public static void TransactionHistory()
    {
        var json = Program.LoadObject();
        Person person = JsonSerializer.Deserialize<Person>(json);

        if (person.TransactionHistory != null)
        {
            try
            {
                Console.WriteLine();
                Console.Write(new string('-', 15));
                Console.Write(" ტრანზაქციების ისტორია  ");
                Console.Write(new string('-', 15));
                int startIndex = Math.Max(0, person.TransactionHistory.Count - 5);
                for (int i = startIndex; i < person.TransactionHistory.Count; i++)
                {
                    Console.WriteLine();
                    var transaction = person.TransactionHistory[i];
                    Console.WriteLine($"Transaction Type: {transaction.TransactionType}");
                    Console.WriteLine($"Transaction Date: {transaction.TransactionDate}");
                    Console.WriteLine($"Amount GEL: {transaction.AmountGEL}");
                    Console.WriteLine($"Amount USD: {transaction.AmountUSD}");
                    Console.WriteLine($"Amount EUR: {transaction.AmountEUR}");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        else
        {
            Console.WriteLine("No transaction history found.");
        }
    }

    public static void DepositeMonay()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("არჩეული მოქმედება - თანხის შეტანა ანგარიშზე.");
        Console.WriteLine("1 - GEL");
        Console.WriteLine("2 - USD");
        Console.WriteLine("3 - EUR");
        Console.Write("აირჩიეთ ვალუტა : ");


        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(intercept: true);
        } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3');

        int choosenCurrency = int.Parse(key.KeyChar.ToString());

        

        switch (choosenCurrency)
        {
            case 1:
                Console.WriteLine("GEL");
                Console.Write("შემოიტანეთ სასურველი თანხა: ");
                
                decimal amountGEL = DepositAmount();

                if (amountGEL > 0 )
                {
                    Program.InsertTransaction("DepositeMonay", newAmountGEL: amountGEL);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"დეპოზიტი {amountGEL} ✅ ");
                    Console.ForegroundColor = ConsoleColor.White;
                    
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("0_ვანი ტრანზაქცია არ არის დაშვებული");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                break;

            case 2:
                
                Console.WriteLine("USD");
                Console.Write("შემოიტანეთ სასურველი თანხა: ");
                
                decimal amountUSD = DepositAmount();

                if (amountUSD> 0 )
                {
                    Program.InsertTransaction("DepositeMonay", newAmountUSD: amountUSD);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"დეპოზიტი {amountUSD} ✅ ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("0_ვანი ტრანზაქცია არ არის დაშვებული");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                break;
            case 3:
                Console.WriteLine("EUR");
                Console.Write("შემოიტანეთ სასურველი თანხა: ");
                
                decimal amountEUR = DepositAmount();

                if (amountEUR > 0 )
                {
                    Program.InsertTransaction("DepositeMonay", newAmountEUR: amountEUR);
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"დეპოზიტი {amountEUR} ✅ ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("0_ვანი ტრანზაქცია არ არის დაშვებული");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                break;
            
            
            
                
            default:
                Console.WriteLine("Invalid choice");
                break;
        }

         static decimal DepositAmount()
        {
            
            var amount = "";
            int amountLength = 0;

            
            
            while (amount.Length < 6)
            {
                ConsoleKeyInfo inputReadKey = Console.ReadKey(true);

                if (char.IsDigit(inputReadKey.KeyChar))
                {
                    amount += inputReadKey.KeyChar;
                    amountLength++;
                    Console.Write(inputReadKey.KeyChar);
                }
                else if (inputReadKey.Key == ConsoleKey.Backspace && amount.Length > 0)
                {
                    amount = amount.Substring(0, amount.Length - 1);
                    amountLength--;
                    Console.Write("\b \b");
                }

                if (inputReadKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            decimal.TryParse(amount, out decimal decimalAmount);
            return decimalAmount;
        }
    }

    public static void WIthdrawMonay()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("არჩეული მოქმედება - თანხის გამოტანა.");
        Console.WriteLine("1 - GEL");
        Console.WriteLine("2 - USD");
        Console.WriteLine("3 - EUR");
        Console.Write("აირჩიეთ ვალუტა : ");
        
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(intercept: true);
        } while (key.KeyChar != '1' && key.KeyChar != '2' && key.KeyChar != '3');

        int choosenCurrency = int.Parse(key.KeyChar.ToString());
        
        
        
        
        string json = Program.LoadObject();
        Person person = JsonSerializer.Deserialize<Person>(json);
                
        decimal defaultAmountGEL =
            person.TransactionHistory.Count > 0 ? person.TransactionHistory[^1].AmountGEL : 0;
        decimal defaultAmountUSD =
            person.TransactionHistory.Count > 0 ? person.TransactionHistory[^1].AmountUSD : 0;
        decimal defaultAmountEUR =
            person.TransactionHistory.Count > 0 ? person.TransactionHistory[^1].AmountEUR : 0;
        
        switch (choosenCurrency)
        {
            
            
            case 1:
                Console.WriteLine("GEL");
                Console.Write("გასატანი თანხა თანხა: ");
                
                decimal amountGEL = WithdrawAmount();
                

                if (defaultAmountGEL < -amountGEL )
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{-amountGEL} ლარის გატანა უარყოფილია ❌  ");
                    Console.WriteLine("მიზეზი - ანგარიშზე არ არის საკმარისი თანხა !");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (amountGEL == 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("0_ვანი ტრანზაქცია არ არის დაშვებული");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Program.InsertTransaction("WithdrawAmount", newAmountGEL: amountGEL);
                    Console.WriteLine($"თქვენ წარმატებით გაანაღდეთ {-amountGEL} ლარი ✅ ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                break;
            case 2:
                Console.WriteLine("USD");
                Console.Write("გასატანი თანხა: ");
                
                decimal amountUSD = WithdrawAmount();
                if (defaultAmountUSD < -amountUSD )
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{-amountUSD} დოლარის გატანა უარყოფილია ❌  ");
                    Console.WriteLine("მიზეზი - ანგარიშზე არ არის საკმარისი თანხა !");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (amountUSD == 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("0_ვანი ტრანზაქცია არ არის დაშვებული");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Program.InsertTransaction("WithdrawAmount", newAmountUSD: amountUSD);
                    Console.WriteLine($"თქვენ წარმატებით გაანაღდეთ {-amountUSD} დოლარი ✅ ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                break;
            case 3:
                Console.WriteLine("EUR");
                Console.Write("გასატანი თანხა: ");
                
                decimal amountEUR = WithdrawAmount();
                if (defaultAmountEUR < -amountEUR )
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{-amountEUR} ევროს გატანა უარყოფილია ❌  ");
                    Console.WriteLine("მიზეზი - ანგარიშზე არ არის საკმარისი თანხა !");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (amountEUR == 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("0_ვანი ტრანზაქცია არ არის დაშვებული");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Program.InsertTransaction("WithdrawAmount", newAmountEUR: amountEUR);
                    Console.WriteLine($"თქვენ წარმატებით გაანაღდეთ {-amountEUR} ევრო ✅ ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }

         static decimal WithdrawAmount()
        {
            
            var amount = "";
            int amountLength = 0;

            
            
            while (amount.Length < 6)
            {
                ConsoleKeyInfo inputReadKey = Console.ReadKey(true);

                if (char.IsDigit(inputReadKey.KeyChar))
                {
                    amount += inputReadKey.KeyChar;
                    amountLength++;
                    Console.Write(inputReadKey.KeyChar);
                }
                else if (inputReadKey.Key == ConsoleKey.Backspace && amount.Length > 0)
                {
                    amount = amount.Substring(0, amount.Length - 1);
                    amountLength--;
                    Console.Write("\b \b");
                }

                if (inputReadKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            decimal.TryParse(amount, out decimal decimalAmount);
            return -decimalAmount;
        }
        
    }
}