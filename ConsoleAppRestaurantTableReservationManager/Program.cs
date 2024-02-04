using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class TableReservationApp
{
    static void Main(string[] args)
    {
        var manager = new ReservationManager();
        manager.AddRestaurant("A", 10);
        manager.AddRestaurant("B", 5);

        Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3));
        Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3));
    }
}

public class ReservationManager
{
    private readonly List<Restaurant> Restaurants;

    public ReservationManager()
    {
        Restaurants = new List<Restaurant>();
    }

    public void AddRestaurant(string name, int tableCount)
    {
        try
        {
            var restaurant = new Restaurant(name, tableCount);
            Restaurants.Add(restaurant);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding restaurant: {ex.Message}");
        }
    }

    public void LoadRestaurantsFromFile(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    AddRestaurant(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading restaurants from file: {ex.Message}");
        }
    }

    public List<string> FindAllFreeTables(DateTime date)
    {
        try
        {
            var freeTables = new List<string>();
            foreach (var restaurant in Restaurants)
            {
                freeTables.AddRange(restaurant.GetAvailableTables(date));
            }
            return freeTables;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error finding free tables: {ex.Message}");
            return new List<string>();
        }
    }

    public bool BookTable(string restaurantName, DateTime date, int tableNumber)
    {
        try
        {
            var restaurant = Restaurants.FirstOrDefault(r => r.Name == restaurantName);
            if (restaurant != null && restaurant.BookTable(date, tableNumber))
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error booking table: {ex.Message}");
            return false;
        }
    }

    public void SortRestaurants(DateTime date)
    {
        try
        {
            Restaurants.Sort((r1, r2) => r1.CountAvailableTables(date).CompareTo(r2.CountAvailableTables(date)));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sorting restaurants: {ex.Message}");
        }
    }
}

public class Restaurant
{
    public string Name { get; }
    public List<RestaurantTable> Tables { get; }

    public Restaurant(string name, int tableCount)
    {
        Name = name;
        Tables = Enumerable.Range(1, tableCount).Select(i => new RestaurantTable()).ToList();
    }

    public List<string> GetAvailableTables(DateTime date)
    {
        return Tables
            .Where(table => !table.IsBooked(date))
            .Select((table, index) => $"{Name} - Table {index + 1}")
            .ToList();
    }

    public bool BookTable(DateTime date, int tableNumber)
    {
        if (tableNumber < 0 || tableNumber >= Tables.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(tableNumber));
        }

        return Tables[tableNumber].Book(date);
    }

    public int CountAvailableTables(DateTime date)
    {
        return Tables.Count(table => !table.IsBooked(date));
    }
}

public class RestaurantTable
{
    private readonly List<DateTime> BookedDates;

    public RestaurantTable()
    {
        BookedDates = new List<DateTime>();
    }

    public bool Book(DateTime date)
    {
        if (BookedDates.Contains(date))
        {
            return false;
        }

        BookedDates.Add(date);
        return true;
    }

    public bool IsBooked(DateTime date)
    {
        return BookedDates.Contains(date);
    }
}
