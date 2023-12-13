using MontgomeryInsuranceData.Enums;

namespace MontgomeryInsuranceData;

public class Claim
{

    public int Id { get; set; }
    public Claim () {}
    public Queue<Claim> Queue { get; set; } = new Queue<Claim>();

    public Claim(ClaimType claimType, string description, double claimAmount, DateTime dateOfIncident, DateTime dateOfClaim)
    {
        ClaimType = claimType;
        Description = description;
        ClaimAmount = claimAmount;
        DateOfIncident = dateOfIncident;
        DateOfClaim = dateOfClaim;
    }
    public ClaimType ClaimType { get; set; }
    public string Description { get; set; } = string.Empty;
    public double ClaimAmount { get; set; }
    public DateTime DateOfIncident { get; set; }
    public DateTime DateOfClaim { get; set; }
    public bool IsValid
    {
        get
        {
            TimeSpan difference = DateOfClaim - DateOfIncident;
            return difference.Days <= 30 && difference.Days >= 0;
        }
    }
}
