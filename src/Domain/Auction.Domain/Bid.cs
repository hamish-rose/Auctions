using System;

namespace Auction.Domain
{
    public struct Bid
    {
        public int UserId { get; }
        
        public decimal Value { get; }

        public DateTime Timestamp { get; }
        
        public Bid(int userId, decimal value)
        {
            UserId = userId;
            Value = value;
            Timestamp = DateTime.Now;
        }
    }
}