using System;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace Auction.Domain.UnitTests
{
    public class AuctionBiddingTests
    {
        private static Fixture Fixture { get; } = new Fixture();

        private Func<Auction> AuctionFactory = () => Fixture.Create<Auction>(); 
            
        [Fact]
        public void AddBidIncreasesCurrent()
        {
            var auction = AuctionFactory();
            var bid = new Bid(1, auction.CurrentPrice + 0.01m);
            
            auction.Bid(bid);
            
            auction.CurrentPrice.Should().Be(bid.Value);
            auction.CurrentPrice.Should().NotBe(auction.StartPrice);
        }

        [Fact]
        public void InvalidBidNotAdded()
        {
            var auction = AuctionFactory();
            var bid = new Bid(1, auction.CurrentPrice);
            
            Action a1 = () => auction.Bid(bid);
            a1.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void LatestBidIsCurrentPrice()
        {
            var auction = AuctionFactory();
            
            auction.Bid(new Bid(1, auction.CurrentPrice + 0.005m));
            auction.Bid(new Bid(2, auction.CurrentPrice + 0.005m));
            auction.Bid(new Bid(3, auction.CurrentPrice + 0.005m));
            auction.Bid(new Bid(4, auction.CurrentPrice + 0.005m));
            var latest = new Bid(1, auction.CurrentPrice + 0.005m);
            
            auction.Bid(latest);

            auction.CurrentPrice.Should().Be(latest.Value);
            auction.Bids.Should().HaveCount(5);
        }
    }
}