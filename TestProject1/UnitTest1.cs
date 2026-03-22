namespace TestProject1;

public class UnitTest1
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 8)]
    [InlineData(8, 1)]
    [InlineData(8, 8)]
    [InlineData(4, 4)]
    public void Constructor_ValidCoordinates_ShouldCreateInstance(byte x, byte y)
    {
        // Act
        var position = new CheckerBoardPosition(x, y);

        // Assert
        Assert.Equal(x, position.X);
        Assert.Equal(y, position.Y);
    }
    [Theory]
    [InlineData(0, 1)]
    [InlineData(9, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 9)]
    [InlineData(0, 0)]
    [InlineData(9, 9)]
    public void Constructor_InvalidCoordinates_ShouldThrowArgumentOutOfRangeException(byte x, byte y)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new CheckerBoardPosition(x, y));
    }
    

    [Theory]
    [InlineData(1, 'A')]
    [InlineData(2, 'B')]
    [InlineData(3, 'C')]
    [InlineData(4, 'D')]
    [InlineData(5, 'E')]
    [InlineData(6, 'F')]
    [InlineData(7, 'G')]
    [InlineData(8, 'H')]
    public void XLetter_ValidX_ShouldReturnCorrectLetter(byte x, char expectedLetter)
    {
        // Arrange
        var position = new CheckerBoardPosition(x, 1);

        // Act
        var letter = position.XLetter;

        // Assert
        Assert.Equal(expectedLetter, letter);
    }
    

    [Theory]
    [InlineData(1, 1, "A1")]
    [InlineData(1, 8, "A8")]
    [InlineData(8, 1, "H1")]
    [InlineData(8, 8, "H8")]
    [InlineData(4, 5, "D5")]
    public void ToString_ShouldReturnCorrectStringRepresentation(byte x, byte y, string expected)
    {
        // Arrange
        var position = new CheckerBoardPosition(x, y);

        // Act
        var result = position.ToString();

        // Assert
        Assert.Equal(expected, result);
    }
    

    [Theory]
    [InlineData("A1", 1, 1)]
    [InlineData("A8", 1, 8)]
    [InlineData("H1", 8, 1)]
    [InlineData("H8", 8, 8)]
    [InlineData("D5", 4, 5)]
    [InlineData("E2", 5, 2)]
    [InlineData("G7", 7, 7)]
    public void Parse_ValidString_ShouldReturnCorrectPosition(string input, byte expectedX, byte expectedY)
    {
        // Act
        var position = CheckerBoardPosition.Parse(input, null);

        // Assert
        Assert.Equal(expectedX, position.X);
        Assert.Equal(expectedY, position.Y);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("A")]
    [InlineData("1")]
    [InlineData("A10")]
    [InlineData("I1")]
    [InlineData("A0")]
    [InlineData("Z9")]
    [InlineData("a1")] 
    [InlineData("A 1")]
    [InlineData("A1 ")]
    [InlineData(" A1")]
    public void Parse_InvalidString_ShouldThrowFormatException(string? input)
    {
        // Act & Assert
        Assert.Throws<FormatException>(() => CheckerBoardPosition.Parse(input, null));
    }


    [Theory]
    [InlineData("A1", 1, 1)]
    [InlineData("A8", 1, 8)]
    [InlineData("H1", 8, 1)]
    [InlineData("H8", 8, 8)]
    [InlineData("D5", 4, 5)]
    [InlineData("E2", 5, 2)]
    [InlineData("G7", 7, 7)]
    public void TryParse_ValidString_ShouldReturnTrueAndPosition(string input, byte expectedX, byte expectedY)
    {
        // Act
        var result = CheckerBoardPosition.TryParse(input, null, out var position);

        // Assert
        Assert.True(result);
        Assert.NotNull(position);
        Assert.Equal(expectedX, position!.X);
        Assert.Equal(expectedY, position.Y);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("A")]
    [InlineData("1")]
    [InlineData("A10")]
    [InlineData("I1")]
    [InlineData("A0")]
    [InlineData("Z9")]
    [InlineData("a1")]
    [InlineData("A 1")]
    public void TryParse_InvalidString_ShouldReturnFalseAndNullPosition(string? input)
    {
        // Act
        var result = CheckerBoardPosition.TryParse(input, null, out var position);

        // Assert
        Assert.False(result);
        Assert.Null(position);
    }
    

    [Fact]
    public void MultipleInstances_ShouldBeIndependent()
    {
        // Arrange
        var position1 = new CheckerBoardPosition(1, 1);
        var position2 = new CheckerBoardPosition(8, 8);

        // Assert
        Assert.NotEqual(position1.X, position2.X);
        Assert.NotEqual(position1.Y, position2.Y);
        Assert.NotEqual(position1.XLetter, position2.XLetter);
    }

    [Theory]
    [InlineData(1, 1, "A1")]
    [InlineData(8, 8, "H8")]
    public void ParseAndToString_ShouldBeConsistent(byte x, byte y, string expectedString)
    {
        // Arrange
        var original = new CheckerBoardPosition(x, y);
        var stringRepresentation = original.ToString();

        // Act
        var parsed = CheckerBoardPosition.Parse(stringRepresentation, null);

        // Assert
        Assert.Equal(original.X, parsed.X);
        Assert.Equal(original.Y, parsed.Y);
        Assert.Equal(expectedString, stringRepresentation);
    }

    [Fact]
    public void XLetter_ShouldBeReadOnly()
    {
        // Arrange
        var position = new CheckerBoardPosition(1, 1);
        var propertyInfo = typeof(CheckerBoardPosition).GetProperty("XLetter");

        // Assert
        Assert.NotNull(propertyInfo);
        Assert.False(propertyInfo!.CanWrite);
    }

    [Fact]
    public void Properties_ShouldBeReadOnly()
    {
        // Arrange
        var position = new CheckerBoardPosition(1, 1);
        var xProperty = typeof(CheckerBoardPosition).GetProperty("X");
        var yProperty = typeof(CheckerBoardPosition).GetProperty("Y");

        // Assert
        Assert.NotNull(xProperty);
        Assert.NotNull(yProperty);
        Assert.False(xProperty!.CanWrite);
        Assert.False(yProperty!.CanWrite);
    }

    [Fact]
    public void Constructor_WithBoundaryValues_ShouldCreateValidInstances()
    {
        // Act
        var minPosition = new CheckerBoardPosition(1, 1);
        var maxPosition = new CheckerBoardPosition(8, 8);

        // Assert
        Assert.Equal(1, minPosition.X);
        Assert.Equal(1, minPosition.Y);
        Assert.Equal(8, maxPosition.X);
        Assert.Equal(8, maxPosition.Y);
        Assert.Equal('A', minPosition.XLetter);
        Assert.Equal('H', maxPosition.XLetter);
    }
}