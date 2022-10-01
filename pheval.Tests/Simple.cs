using System.Linq;
using FluentAssertions;

namespace pheval.Tests
{
    public class Simple
    {
        [Fact]
        public void can_convert_from_byte_array()
        {
            var ids = new byte[] { 0, 4, 8, 12, 16 };
            
            var expected = "2c3c4c5c6c";

            var actual = Card.CardsToString(Card.Cards(ids));

            actual.Should().Be(expected);
        }
    }
}