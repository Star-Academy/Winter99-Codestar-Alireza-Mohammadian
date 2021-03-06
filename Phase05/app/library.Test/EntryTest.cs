using Xunit;

namespace library.Test
{
    public class EntryTest
    {
        private Entry entry;

        public EntryTest()
        {
            this.entry = new Entry("test", 11);
        }

        [Fact]
        public void GetIndexTest()
        {
            Assert.Equal(entry.Index, 11);
        }

        [Fact]
        public void ToStringTest()
        {
            Assert.Equal(entry.ToString(), "Document Id: test, Index: 11");
        }

        [Theory]
        [InlineData("test", 11)]
        [InlineData("test", 12)]
        public void EqualsTestTrue(string docName, int index)
        {
            Assert.True(this.entry.Equals(new Entry(docName, index)));
        }

        [Theory]
        [InlineData("wrong test", 11)]
        [InlineData("wrong test", 12)]
        public void EqualsTestFalse(string docName, int index)
        {
            Assert.False(this.entry.Equals(new Entry(docName, index)));
        }

        [Fact]
        public void GetHashCodeTest()
        {
            Assert.Equal(entry.GetHashCode(), "test".GetHashCode());
        }
    }
}
