using System;
using System.Collections.Generic;
using BikeRental.Domain.Enum;
using BikeRental.Domain.Models;

namespace BikeRental.Tests.Seed;

/// <summary>
/// Provides a predefined data set for unit testing the BikeRental domain models.
/// </summary>
public class DataSeed
{
    /// <summary>Collection of available bike models.</summary>
    public readonly List<Model> Models;

    /// <summary>Collection of bikes.</summary>
    public readonly List<Bike> Bikes;

    /// <summary>Collection of renters.</summary>
    public readonly List<Renter> Renters;

    /// <summary>Collection of rental records.</summary>
    public readonly List<Rent> Rents;

    /// <summary>
    /// Initializes a new instance of the DataSeed class and populates it with sample data.
    /// </summary>
    public DataSeed()
    {
        Models = InitializeModels();
        Renters = InitializeRenters();
        Bikes = InitializeBikes();
        Rents = InitializeRents();
    }

    /// <summary>Initializes the list of bike models.</summary>
    private static List<Model> InitializeModels()
    {
        return new List<Model>
        {
            new Model { Id = 1, WheelSize = 29, MaxPassengerWeight = 120, BikeWeight = 13.8f, BrakeType = "Disc hydraulic", ModelYear = 2025, PricePerHour = 7.50m, BikeType = BikeType.Mountain },
            new Model { Id = 2, WheelSize = 27.5f, MaxPassengerWeight = 110, BikeWeight = 11.2f, BrakeType = "Rim v-brake", ModelYear = 2024, PricePerHour = 8.20m, BikeType = BikeType.Sport },
            new Model { Id = 3, WheelSize = 26, MaxPassengerWeight = 130, BikeWeight = 15.9f, BrakeType = "Disc mechanical", ModelYear = 2023, PricePerHour = 5.90m, BikeType = BikeType.City },
            new Model { Id = 4, WheelSize = 28, MaxPassengerWeight = 125, BikeWeight = 12.7f, BrakeType = "Disc hydraulic", ModelYear = 2025, PricePerHour = 6.80m, BikeType = BikeType.Track },
            new Model { Id = 5, WheelSize = 20, MaxPassengerWeight = 100, BikeWeight = 10.4f, BrakeType = "Rim", ModelYear = 2022, PricePerHour = 4.50m, BikeType = BikeType.Track },
            new Model { Id = 6, WheelSize = 24, MaxPassengerWeight = 95, BikeWeight = 9.8f, BrakeType = "Rim", ModelYear = 2021, PricePerHour = 3.90m, BikeType = BikeType.Mini },
            new Model { Id = 7, WheelSize = 27.5f, MaxPassengerWeight = 115, BikeWeight = 12.1f, BrakeType = "Disc mechanical", ModelYear = 2024, PricePerHour = 6.20m, BikeType = BikeType.Mountain },
            new Model { Id = 8, WheelSize = 29, MaxPassengerWeight = 140, BikeWeight = 18.3f, BrakeType = "Disc hydraulic", ModelYear = 2023, PricePerHour = 9.10m, BikeType = BikeType.Electric },
            new Model { Id = 9, WheelSize = 28, MaxPassengerWeight = 105, BikeWeight = 8.9f, BrakeType = "Disc hydraulic", ModelYear = 2025, PricePerHour = 10.50m, BikeType = BikeType.City },
            new Model { Id = 10, WheelSize = 26, MaxPassengerWeight = 135, BikeWeight = 16.5f, BrakeType = "Drum", ModelYear = 2022, PricePerHour = 5.20m, BikeType = BikeType.City }
        };
    }

    /// <summary>Initializes the list of bikes.</summary>
    private List<Bike> InitializeBikes()
    {
        return new List<Bike>
        {
            new Bike { Id = 1, SerialNumber = "202501001", Color = "Black", Model = Models[0] },
            new Bike { Id = 2, SerialNumber = "2024R01015", Color = "Red", Model = Models[1] },
            new Bike { Id = 3, SerialNumber = "2023X03210", Color = "Blue", Model = Models[2] },
            new Bike { Id = 4, SerialNumber = "2025B05077", Color = "Olive", Model = Models[3] },
            new Bike { Id = 5, SerialNumber = "2022G06342", Color = "Yellow", Model = Models[4] },
            new Bike { Id = 6, SerialNumber = "2021W08908", Color = "White", Model = Models[5] },
            new Bike { Id = 7, SerialNumber = "2024O04556", Color = "Orange", Model = Models[6] },
            new Bike { Id = 8, SerialNumber = "2023G09999", Color = "Graphite", Model = Models[7] },
            new Bike { Id = 9, SerialNumber = "2025S01555", Color = "Silver", Model = Models[8] },
            new Bike { Id = 10, SerialNumber = "2022T12640", Color = "Turquoise", Model = Models[9] }
        };
    }

    /// <summary>Initializes the list of renters.</summary>
    private static List<Renter> InitializeRenters()
    {
        return new List<Renter>
        {
            new Renter { Id = 1, LastName = "Kovalev", Name = "Dmitry", MiddleName = "Ilyich", PhoneNumber = "+7 901 111-11-11" },
            new Renter { Id = 2, LastName = "Egorova", Name = "Sofia", MiddleName = "Antonovna", PhoneNumber = "+7 902 222-22-22" },
            new Renter { Id = 3, LastName = "Leontiev", Name = "Maxim", MiddleName = "Olegovich", PhoneNumber = "+7 903 333-33-33" },
            new Renter { Id = 4, LastName = "Romanova", Name = "Maria", MiddleName = "Sergeevna", PhoneNumber = "+7 904 444-44-44" },
            new Renter { Id = 5, LastName = "Gusev", Name = "Igor", MiddleName = "Valerievich", PhoneNumber = "+7 905 555-55-55" },
            new Renter { Id = 6, LastName = "Frolova", Name = "Alena", MiddleName = "Alexandrovna", PhoneNumber = "+7 906 666-66-66" },
            new Renter { Id = 7, LastName = "Semenov", Name = "Pavel", MiddleName = "Andreevich", PhoneNumber = "+7 907 777-77-77" },
            new Renter { Id = 8, LastName = "Morozova", Name = "Ekaterina", MiddleName = "Dmitrievna", PhoneNumber = "+7 908 888-88-88" },
            new Renter { Id = 9, LastName = "Nazarov", Name = "Artur", MiddleName = "Petrovich", PhoneNumber = "+7 909 999-99-99" },
            new Renter { Id = 10, LastName = "Voronova", Name = "Olga", MiddleName = "Igorevna", PhoneNumber = "+7 900 000-00-00" }
        };
    }

    /// <summary>Initializes the list of rental transactions.</summary>
    private List<Rent> InitializeRents()
    {
        return new List<Rent>
        {
            new Rent { Id = 1, StartTime = new DateTime(2025, 8, 2, 9, 0, 0), Duration = 2, Bike = Bikes[0], Renter = Renters[0] },
            new Rent { Id = 2, StartTime = new DateTime(2025, 8, 3, 14, 30, 0), Duration = 3, Bike = Bikes[1], Renter = Renters[1] },
            new Rent { Id = 3, StartTime = new DateTime(2025, 8, 5, 10, 15, 0), Duration = 1, Bike = Bikes[2], Renter = Renters[2] },
            new Rent { Id = 4, StartTime = new DateTime(2025, 8, 7, 16, 0, 0), Duration = 4, Bike = Bikes[3], Renter = Renters[3] },
            new Rent { Id = 5, StartTime = new DateTime(2025, 8, 10, 11, 45, 0), Duration = 5, Bike = Bikes[4], Renter = Renters[4] },
            new Rent { Id = 6, StartTime = new DateTime(2025, 8, 12, 13, 0, 0), Duration = 2, Bike = Bikes[5], Renter = Renters[5] },
            new Rent { Id = 7, StartTime = new DateTime(2025, 8, 14, 15, 30, 0), Duration = 3, Bike = Bikes[6], Renter = Renters[6] },
            new Rent { Id = 8, StartTime = new DateTime(2025, 8, 16, 9, 30, 0), Duration = 4, Bike = Bikes[7], Renter = Renters[7] },
            new Rent { Id = 9, StartTime = new DateTime(2025, 8, 18, 12, 0, 0), Duration = 1, Bike = Bikes[8], Renter = Renters[8] },
            new Rent { Id = 10, StartTime = new DateTime(2025, 8, 20, 17, 0, 0), Duration = 2, Bike = Bikes[9], Renter = Renters[9] },
            new Rent { Id = 11, StartTime = new DateTime(2025, 9, 1, 10, 0, 0), Duration = 3, Bike = Bikes[0], Renter = Renters[1] },
            new Rent { Id = 12, StartTime = new DateTime(2025, 9, 3, 14, 0, 0), Duration = 5, Bike = Bikes[1], Renter = Renters[2] },
            new Rent { Id = 13, StartTime = new DateTime(2025, 9, 5, 16, 30, 0), Duration = 2, Bike = Bikes[2], Renter = Renters[3] },
            new Rent { Id = 14, StartTime = new DateTime(2025, 9, 7, 11, 0, 0), Duration = 4, Bike = Bikes[3], Renter = Renters[4] },
            new Rent { Id = 15, StartTime = new DateTime(2025, 9, 9, 13, 0, 0), Duration = 1, Bike = Bikes[4], Renter = Renters[5] },
            new Rent { Id = 16, StartTime = new DateTime(2025, 9, 12, 15, 0, 0), Duration = 3, Bike = Bikes[5], Renter = Renters[6] },
            new Rent { Id = 17, StartTime = new DateTime(2025, 9, 14, 9, 0, 0), Duration = 2, Bike = Bikes[6], Renter = Renters[7] },
            new Rent { Id = 18, StartTime = new DateTime(2025, 9, 16, 12, 30, 0), Duration = 5, Bike = Bikes[7], Renter = Renters[8] },
            new Rent { Id = 19, StartTime = new DateTime(2025, 9, 18, 14, 0, 0), Duration = 3, Bike = Bikes[8], Renter = Renters[9] },
            new Rent { Id = 20, StartTime = new DateTime(2025, 9, 20, 16, 0, 0), Duration = 4, Bike = Bikes[9], Renter = Renters[0] }
        };
    }
}
