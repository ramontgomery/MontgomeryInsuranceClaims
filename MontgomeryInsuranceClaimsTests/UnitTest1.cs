

using MontgomeryInsuranceData;
using MontgomeryInsuranceData.Enums;
using MontgomeryRepository;

namespace MontgomeryInsuranceClaimsTests;

public class UnitTest1
{
[Fact]
public void AddClaimToQueue_ShouldAddClaim_WhenClaimIsValid()
{

    var repository = new MontgomeryQueueRepository();
    var claim = new Claim(ClaimType.Car, "Accident on highway", 1200.00, DateTime.Now.AddDays(-10), DateTime.Now);


    bool result = repository.AddClaimToQueue(claim);


    Assert.True(result);

}
[Fact]
public void CheckClaimValidity_ShouldReturnTrue_WhenClaimIsWithin30Days()
{

    var repository = new MontgomeryQueueRepository();
    var claim = new Claim(ClaimType.Home, "Water damage", 5000.00, DateTime.Now.AddDays(-20), DateTime.Now);
    repository.AddClaimToQueue(claim);


    bool isValid = repository.CheckClaimValidity(claim.Id);


    Assert.True(isValid);
}
[Fact]
public void RemoveClaimFromQueue_ShouldRemoveClaim_WhenClaimExists()
{

    var repository = new MontgomeryQueueRepository();
    var claim = new Claim(ClaimType.Theft, "Stolen bicycle", 300.00, DateTime.Now.AddDays(-15), DateTime.Now);
    repository.AddClaimToQueue(claim);
    int initialCount = repository.GetQueue().Count;


    bool isRemoved = repository.RemoveClaimFromQueue(claim.Id);


    Assert.True(isRemoved);
    Assert.Equal(initialCount - 1, repository.GetQueue().Count);
}

}