using MontgomeryInsuranceData;
using MontgomeryInsuranceData.Enums;

namespace MontgomeryRepository
{
    public class MontgomeryQueueRepository
    {
        private readonly Queue<Claim> _claimQueue = new Queue<Claim>();
        private int _claimId;

        public MontgomeryQueueRepository()
        {
            Seed();
        }

        private void Seed()
        {
            // Example seed claim
            var seedClaim = new Claim(ClaimType.Car, "Tree Fell On Roof", 2485.90, new DateTime(2023, 10, 14), new DateTime(2023, 11, 29));
            AddClaimToQueue(seedClaim);
        }

        private void IncrementId(Claim claim)
        {
            _claimId++;
            claim.Id = _claimId;
        }

        public bool AddClaimToQueue(Claim claim)
        {
            if (claim != null)
            {
                IncrementId(claim);
                _claimQueue.Enqueue(claim);
                return true;
            }
            return false;
        }

        public Queue<Claim> GetQueue()
        {
            return _claimQueue;
        }

        public Claim? GetClaimById(int claimId)
        {
            return _claimQueue.FirstOrDefault(x => x.Id == claimId);
        }

        public bool RemoveClaimFromQueue(int claimId)
        {
            var claimToRemove = GetClaimById(claimId);
            if (claimToRemove != null)
            {
                var tempQueue = new Queue<Claim>(_claimQueue.Where(x => x.Id != claimId));
                _claimQueue.Clear();
                while (tempQueue.Any())
                {
                    _claimQueue.Enqueue(tempQueue.Dequeue());
                }
                return true;
            }
            return false;
        }

        public bool DeleteClaim(int id)
        {
            return RemoveClaimFromQueue(id);
        }
        public bool CheckClaimValidity(int claimId)
        {
            var claim = GetClaimById(claimId);
            if (claim != null)
            {
                TimeSpan difference = claim.DateOfClaim - claim.DateOfIncident;
                return difference.Days <= 30 && difference.Days >= 0;
            }
            return false;
        }
        public IEnumerable<Claim> GetValidClaims()
        {
            return _claimQueue.Where(claim => claim.IsValid);
        }
    }
}