using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using MontgomeryInsuranceData;
using MontgomeryInsuranceData.Enums;
using MontgomeryRepository;

namespace MontgomeryInsuranceClaims;

public class ProgramUI
{

         private readonly MontgomeryQueueRepository _repo = new MontgomeryQueueRepository();

        public void Run()
        {
            RunApplication();
        }

        private void RunApplication()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Montgomery Insurance Claims\n" +
                                  "1. Add Claim to Queue\n" +
                                  "2. Get Claim Queue\n" +
                                  "3. Remove Claim From Queue\n" +
                                  "4. Check Claim Validity\n" +
                                  "5. Delete Claim\n" +
                                  "6. Get Valid Claims\n" +
                                  "0. Exit Application\n");

                if (!int.TryParse(Console.ReadLine(), out var userInput))
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                    continue;
                }

                switch (userInput)
                {
                    case 1:
                        AddClaimToQueue();
                        break;
                    case 2:
                        GetQueue();
                        break;
                    case 3:
                        RemoveClaimFromQueue();
                        break;
                    case 4:
                        CheckClaimValidity();
                        break;
                    case 5:
                        DeleteClaim();
                        break;
                    case 6:
                        GetValidClaims();
                        break;
                    case 0:
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }


private void GetQueue()
{
    Console.Clear();
    var claimQueue = _repo.GetQueue();

    if (claimQueue.Any())
    {
        foreach (var claim in claimQueue)
        {
            Console.WriteLine($"Claim ID: {claim.Id}, Type: {claim.ClaimType}, Description: {claim.Description}, Amount: {claim.ClaimAmount}, Date of Incident: {claim.DateOfIncident.ToShortDateString()}, Date of Claim: {claim.DateOfClaim.ToShortDateString()}");
        }
    }
    else
    {
        Console.WriteLine("No claims in the queue.");
    }

    PressAnyKey();
}
private void RemoveClaimFromQueue()
{
    Console.Clear();
    Console.WriteLine("Enter the ID of the claim to remove:");
    int claimId = int.Parse(Console.ReadLine()!);

    if (_repo.RemoveClaimFromQueue(claimId))
    {
        Console.WriteLine("Claim removed successfully.");
    }
    else
    {
        Console.WriteLine("Claim not found or unable to remove.");
    }

    PressAnyKey();
}
        private void CheckClaimValidity()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID of the claim to check for validity:");

            if (int.TryParse(Console.ReadLine(), out int claimId))
            {
                bool isValid = _repo.CheckClaimValidity(claimId);
                if (isValid)
                {
                    Console.WriteLine("The claim is valid.");
                }
                else
                {
                    Console.WriteLine("The claim is invalid or not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid claim ID.");
            }

            PressAnyKey();
        }
private void DeleteClaim()
{
    Console.Clear();
    Console.WriteLine("Enter the ID of the claim to delete:");
    int claimId = int.Parse(Console.ReadLine()!);

    if (_repo.DeleteClaim(claimId))
    {
        Console.WriteLine("Claim deleted successfully.");
    }
    else
    {
        Console.WriteLine("Claim not found or unable to delete.");
    }

    PressAnyKey();
}
private void GetValidClaims()
{
    Console.Clear();
    var validClaims = _repo.GetValidClaims();

    if (validClaims.Any())
    {
        foreach (var claim in validClaims)
        {
            Console.WriteLine($"Claim ID: {claim.Id}, Type: {claim.ClaimType}, Description: {claim.Description}, Amount: {claim.ClaimAmount}, Date of Incident: {claim.DateOfIncident.ToShortDateString()}, Date of Claim: {claim.DateOfClaim.ToShortDateString()}");
        }
    }
    else
    {
        Console.WriteLine("No valid claims found.");
    }

    PressAnyKey();
}


private void AddClaimToQueue()
{
    Console.Clear();
    Console.WriteLine("Welcome to Montgomery Insurance Claims\n" +
                      "Please enter the following details for the claim:");

    Console.WriteLine("Enter Claim Type (1 for Car, 2 for Home, 3 for Theft):");
    int claimTypeInput = int.Parse(Console.ReadLine()!);
    ClaimType claimType = (ClaimType)(claimTypeInput - 1); // Assuming enum starts from 0


    Console.WriteLine("Enter Description of Claim:");
    string claimDescription = Console.ReadLine()!;


    Console.WriteLine("Enter Amount of Claim (use max 2 decimals for dollar amount):");
    double claimAmount = double.Parse(Console.ReadLine()!);


    Console.WriteLine("Enter Date of Incident (MM-DD-YYYY):");
    DateTime dateOfIncident = DateTime.Parse(Console.ReadLine()!);


    Console.WriteLine("Enter Date of Claim (MM-DD-YYYY):");
    DateTime dateOfClaim = DateTime.Parse(Console.ReadLine()!);


    Claim newClaim = new Claim(claimType, claimDescription, claimAmount, dateOfIncident, dateOfClaim);

    if (_repo.AddClaimToQueue(newClaim))
    {
        Console.WriteLine("Claim added successfully.");
    }
    else
    {
        Console.WriteLine("Failed to add claim.");
    }

    PressAnyKey();
}
private void PressAnyKey()
{
    System.Console.WriteLine("Press Any Key to Continue..");
    System.Console.ReadKey();
}




}
