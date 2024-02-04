using NUnit.Framework;
using System;

[TestFixture]
public class RestaurantTableTests
{
    [Test]
    public void Book_ValidDate_ShouldBookTable()
    {
        var table = new RestaurantTable();
        var date = new DateTime(2023, 12, 25);

        var result = table.Book(date);

        Assert.That(result, Is.True);
    }

    [Test]
    public void IsBooked_BookedDate_ShouldReturnTrue()
    {
        var table = new RestaurantTable();
        var date = new DateTime(2023, 12, 25);
        table.Book(date);

        var result = table.IsBooked(date);

        Assert.That(result, Is.True);
    }
}
