using System.Text.Json;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = LoadObject();
            try
            {
                Person person = JsonSerializer.Deserialize<Person>(json);
                Console.Write("შეიყვანეთ ბარათის ნომერი: ");
                var creditCardNumber = CardNumberInput();

                Console.Write("შეიყვანეთ ბარათის მოქმედების ვადა: ");
                var expDate = ExpiratinDateInput();

                Console.Write("  CVC კოდი: ");
                var cvc = CvcValidationInput();

                if (creditCardNumber == person.CardDetails.CardNumber && expDate == person.CardDetails.ExpirationDate &&
                    cvc == person.CardDetails.CVC)
                {
                    Console.Write("PIN : ");
                    var pin = PinValidationInput();

                    if (pin == person.PinCode)
                    {
                        int actionNumber = 0;

                        MainMenu(person.FirstName, person.LastName);

                        while (actionNumber != 7)
                        {
                            Console.WriteLine();
                            Console.Write("აირჩიეთ სასურველი მოქმედება: ");
                            actionNumber = ChoosenAction();

                            Operations.DoAction(actionNumber);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("არასწორი PIN  ! ❌ ");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ასეთი ბარათი არ არსებობს ! ❌ ");
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("არასწორი JSON ფორმატი");
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"მოხდა შეცდომა: {ex.Message}");
            }


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


        public static string LoadObject()
        {
            string filePath =
                "/Users/meskhdav/Library/CloudStorage/OneDrive-TheStarsGroup/Desktop/Mid_Term_Exam/Exam/Exam/files/Person.json";

            try
            {
                string json = File.ReadAllText(filePath);
                return json;
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File '{filePath}' not found.");
                Console.ForegroundColor = ConsoleColor.White;

                // Handle the exception by returning null or an empty string
                return null;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;

                // Handle the exception by returning null or an empty string
                return null;
            }
        }


        static string CardNumberInput()
        {
            string cardNumber = "";
            int count = 0;

            while (cardNumber.Length < 19)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); //უყურებს კონსოლს
                if (char.IsDigit(key.KeyChar)) // იღებს მხოლოდ ციფრებს
                {
                    cardNumber += key.KeyChar;
                    count++;
                    Console.Write(key.KeyChar);
                    if (count % 4 == 0 && count != 16)
                    {
                        Console.Write("-");
                        cardNumber += "-";
                    }
                    else if (key.Key == ConsoleKey.Backspace && cardNumber.Length > 0)
                    {
                        if (cardNumber[cardNumber.Length - 1] == '-')
                        {
                            cardNumber = cardNumber.Substring(0, cardNumber.Length - 2);
                            Console.Write("\b \b");
                            if (count % 4 == 0 && count != 16)
                            {
                                count--;
                                Console.Write("\b \b");
                            }
                        }
                        else
                        {
                            cardNumber = cardNumber.Substring(0, cardNumber.Length - 1);


                            if (count % 4 != 0 && count != 16)
                            {
                                count--;
                                Console.Write("\b \b");
                            }
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Backspace && cardNumber.Length > 0)
                {
                    if (cardNumber[cardNumber.Length - 1] == '-')
                    {
                        cardNumber = cardNumber.Substring(0, cardNumber.Length - 2);
                        Console.Write("\b \b");
                        if (count % 4 == 0 && count != 16)
                        {
                            count--;
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        cardNumber = cardNumber.Substring(0, cardNumber.Length - 1);


                        if (count % 4 != 0 && count != 16)
                        {
                            count--;
                            Console.Write("\b \b");
                        }
                    }
                }
            }

            Console.WriteLine();
            return cardNumber;
        }

        static string ExpiratinDateInput()
        {
            string expDate = "";
            int count = 0;

            while (expDate.Length < 5)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); //უყურებს კონსოლს
                if (char.IsDigit(key.KeyChar)) // იღებს მხოლოდ ციფრებს
                {
                    expDate += key.KeyChar;
                    count++;
                    Console.Write(key.KeyChar);
                    if (count % 2 == 0 && count != 4)
                    {
                        Console.Write("/");
                        expDate += "/";
                    }
                    else if (key.Key == ConsoleKey.Backspace && expDate.Length > 0)
                    {
                        if (expDate[expDate.Length - 1] == '/')
                        {
                            expDate = expDate.Substring(0, expDate.Length - 2);
                            Console.Write("\b \b");
                            if (count % 2 == 0 && count != 4)
                            {
                                count--;
                                Console.Write("\b \b");
                            }
                        }
                        else
                        {
                            expDate = expDate.Substring(0, expDate.Length - 1);


                            if (count % 2 != 0 && count != 4)
                            {
                                count--;
                                Console.Write("\b \b");
                            }
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Backspace && expDate.Length > 0)
                {
                    if (expDate[expDate.Length - 1] == '/')
                    {
                        expDate = expDate.Substring(0, expDate.Length - 2);
                        Console.Write("\b \b");
                        if (count % 2 == 0 && count != 4)
                        {
                            count--;
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        expDate = expDate.Substring(0, expDate.Length - 1);


                        if (count % 2 != 0 && count != 4)
                        {
                            count--;
                            Console.Write("\b \b");
                        }
                    }
                }
            }

            //Console.WriteLine();

            return expDate;
        }

        static string CvcValidationInput()
        {
            string cvc = "";
            int count = 0;

            while (cvc.Length < 3)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (char.IsDigit(key.KeyChar))
                {
                    cvc += key.KeyChar;
                    count++;
                    Console.Write(key.KeyChar);
                }
                else if (key.Key == ConsoleKey.Backspace && cvc.Length > 0)
                {
                    cvc = cvc.Substring(0, cvc.Length - 1);


                    if (cvc.Length < 3)
                    {
                        count--;
                        Console.Write("\b \b");
                    }
                }
            }

            Console.WriteLine();
            return cvc;
        }

        public static string PinValidationInput()
        {
            string pin = "";
            int count = 0;

            while (pin.Length < 4)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (char.IsDigit(key.KeyChar))
                {
                    pin += key.KeyChar;
                    count++;
                    Console.Write("*");
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

            Console.WriteLine();
            return pin;
        }

        public static void MainMenu(string FirstName, string LastName)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"მოგესალმებით : {FirstName} {LastName}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1) ნაშთის ნახვა");
            Console.WriteLine("2) თანხის გამოტანა ანგარიშიდან");
            Console.WriteLine("3) ბოლო 5 ოპერაცია");
            Console.WriteLine("4) თანხის შეტანა ანგარიშზე");
            Console.WriteLine("5) პინ კოდის შეცვლა");
            Console.WriteLine("6) ვალუტის კონვერტაცია");
            Console.WriteLine("7) გამოსვლა");
        }

        static int ChoosenAction()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); // Read key without displaying it

                if (char.IsDigit(key.KeyChar)) // შემოწმება ციფრზე
                {
                    int input = key.KeyChar - '0'; // Convert character to integer value

                    if (input >= 1 && input <= 7)
                    {
                        return input;
                    }
                    else
                    {
                        Console.WriteLine(input + " - არ არის ნებადართული");
                        Console.Write("აირჩიეთ სასურველი მოქმედება: ");
                    }
                }
                else
                {
                    //Console.WriteLine("Invalid input. Please enter a digit between 1 and 7:");
                }
            }
        }


        public static void InsertTransaction(string newTransactionType, decimal? newAmountGEL = null,
            decimal? newAmountUSD = null,
            decimal? newAmountEUR = null)
        {
            string json = LoadObject();
            Person person = JsonSerializer.Deserialize<Person>(json);

            
            decimal defaultAmountGEL =
                person.TransactionHistory.Count > 0 ? person.TransactionHistory[^1].AmountGEL : 0;
            decimal defaultAmountUSD =
                person.TransactionHistory.Count > 0 ? person.TransactionHistory[^1].AmountUSD : 0;
            decimal defaultAmountEUR =
                person.TransactionHistory.Count > 0 ? person.TransactionHistory[^1].AmountEUR : 0;

            
            decimal amountGEL = newAmountGEL ?? defaultAmountGEL;
            decimal amountUSD = newAmountUSD ?? defaultAmountUSD;
            decimal amountEUR = newAmountEUR ?? defaultAmountEUR;

            
            if (newAmountGEL.HasValue)
            {
                amountGEL += defaultAmountGEL;
            }
            else if (newAmountUSD.HasValue)
            {
                amountUSD += defaultAmountUSD;
            }
            else if (newAmountEUR.HasValue)
            {
                amountEUR += defaultAmountEUR;
            }

            // Create a new transaction object
            var newTransactionObject = new Transaction
            {
                TransactionDate = DateTime.Now,
                TransactionType = newTransactionType,
                AmountGEL = amountGEL,
                AmountUSD = amountUSD,
                AmountEUR = amountEUR
            };

            person.TransactionHistory.Add(newTransactionObject);

            try
            {
                string updatedJson =
                    JsonSerializer.Serialize(person, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(
                    "/Users/meskhdav/Library/CloudStorage/OneDrive-TheStarsGroup/Desktop/Mid_Term_Exam/Exam/Exam/files/Person.json",
                    updatedJson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}