using System;
using System.Collections.Generic;

namespace Auction.Domain
{
    public class Auction
    {
        public string Name { get; }

        public decimal StartPrice { get; }

        public decimal? ReservePrice { get; }

        public decimal CurrentPrice => _bids.Count == 0 ? StartPrice : _bids.Peek().Value;

        public bool IsReserveMet => !ReservePrice.HasValue || ReservePrice <= CurrentPrice;

        private readonly Stack<Bid> _bids = new Stack<Bid>();

        public IEnumerable<Bid> Bids => _bids;

        public Auction(string name, decimal startPrice, decimal? reservePrice)
        {
            Name = name;
            StartPrice = startPrice;
            ReservePrice = reservePrice;
        }

        public void Bid(Bid bid)
        {
            if (bid.Value > CurrentPrice)
            {
                _bids.Push(bid);
            }
            else
            {
                throw new InvalidOperationException("Bid value is less than current price.");
            }
        }
    }
}