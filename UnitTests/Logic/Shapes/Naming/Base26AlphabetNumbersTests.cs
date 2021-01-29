using Logic.Shapes.Naming;
using NUnit.Framework;
using System;

namespace UnitTest.Shapes.Naming
{
    [TestFixture]
    public class Base26AlphabetNumbersTests
    {
        private Base26AlphabetNumbers _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Base26AlphabetNumbers();
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void NumberToName_WithNumberLessThanOrEqualToZero_ThrowsException(int number)
        {
            var ex = Assert.Throws<ArgumentException>(() => _sut.NumberToName(number));
            StringAssert.StartsWith("Number must be greater than 0", ex.Message);
        }

        [TestCase(1, ExpectedResult = "A")]
        [TestCase(13, ExpectedResult = "M")]
        [TestCase(26, ExpectedResult = "Z")]
        public string NumberToName_WithValidSingleDigitNumber_ReturnsExcepedString(int number)
        {
            var result = _sut.NumberToName(number);
            return result;
        }

        [TestCase(27, ExpectedResult = "AA")]
        [TestCase(122, ExpectedResult = "DR")]
        [TestCase(702, ExpectedResult = "ZZ")]
        public string NumberToName_WithValidDoubleDigitNumbers_ReturnsExcepedString(int number)
        {
            var result = _sut.NumberToName(number);
            return result;
        }

        [Test]
        public void NumberToName_WithMaxInt_ReturnsExcepedString()
        {
            var result = _sut.NumberToName(int.MaxValue);
            Assert.That(result, Is.EqualTo("FXSHRXW"));
        }
    }
}
