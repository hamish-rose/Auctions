using FluentAssertions;
using Xunit;

namespace Auction.Domain.UnitTests
{
    public class AuctionTests
    {
        [Fact]
        public void NewAuctionStartAndReserve()
        {
            var auction = new Auction("Test Auction", 1, 1);

            auction.Name.Should().Be("Test Auction");
            auction.StartPrice.Should().Be(1);
            auction.ReservePrice.Should().Be(1);
        }

        [Fact]
        public void ReserveMetWhenStartEqReserve()
        {
            var auction = new Auction("Test Auction", 1, 1);
            auction.IsReserveMet.Should().BeTrue();
        }

        [Fact]
        public void ReserveMetWhenNoReserve()
        {
            var auction = new Auction("Test Auction", 1, null);
            auction.IsReserveMet.Should().BeTrue();
        }

        [Fact]
        public void CurrentIsStartWhenNew()
        {
            var auction = new Auction("Test Auction", 1, null);
            auction.CurrentPrice.Should().Be(auction.StartPrice);
        }
    }
}