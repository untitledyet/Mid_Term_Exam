using System;
using System.IO;
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
            var creditCardNumber = CardNumberInput();

            Console.Write("შეიყვანეთ ბარათის მოქმედების ვადა: ");
            var expDate = ExpiratinDateInput();

            Console.Write("  CVC კოდი: ");
            var cvc = CvcValidationInput();


            
            if (creditCardNumber == person.CardDetails.CardNumber && expDate == person.CardDetails.ExpirationDate && cvc == person.CardDetails.CVC)
            {
                Console.Write("PIN : ");
                var pin = PinValidationInput();

                if (pin==person.PinCode)
                {
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("არასწორი PIN  ! \u274c ");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ასეთი ბარათი არ არსებობს ! \u274c ");
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

        static string PinValidationInput()
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
    }
}