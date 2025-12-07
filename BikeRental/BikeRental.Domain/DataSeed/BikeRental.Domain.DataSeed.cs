using BikeRental.Domain.Enum;
using BikeRental.Domain.Models;

namespace BikeRental.Domain.DataSeed;

/// <summary>
///     Provides a predefined data set for unit testing the BikeRental domain models.
/// </summary>
public class DataSeed
{
    /// <summary>Collection of models.</summary>
    public List<Model> Models { get; } = [];

    /// <summary>Collection of bikes.</summary>
    public List<Bike> Bikes { get; } = [];

    /// <summary>Collection of renters.</summary>
    public List<Renter> Renters { get; } = [];

    /// <summary>Collection of rental records.</summary>
    public List<Rent> Rents { get; } = [];

    /// <summary>Constructor initializes fixture of bike models.</summary>
    public DataSeed()
    {
        Models.AddRange([
            new Model
            {
                Id = 1,
                WheelSize = 29,
                MaxPassengerWeight = 120,
                BikeWeight = 13.8f,
                BrakeType = "Disc hydraulic",
                ModelYear = 2025,
                PricePerHour = 7.50m,
                BikeType = BikeType.Mountain
            },
           new Model
            {
                Id = 2,
                WheelSize = 27.5f,
                MaxPassengerWeight = 110,
                BikeWeight = 11.2f,
                BrakeType = "Rim v-brake",
                ModelYear = 2024,
                PricePerHour = 8.20m,
                BikeType = BikeType.Sport
            },
           new Model
            {
                Id = 3,
                WheelSize = 26,
                MaxPassengerWeight = 130,
                BikeWeight = 15.9f,
                BrakeType = "Disc mechanical",
                ModelYear = 2023,
                PricePerHour = 5.90m,
                BikeType = BikeType.City
            },
           new Model
            {
                Id = 4,
                WheelSize = 28,
                MaxPassengerWeight = 125,
                BikeWeight = 12.7f,
                BrakeType = "Disc hydraulic",
                ModelYear = 2025,
                PricePerHour = 6.80m,
                BikeType = BikeType.Track
            },
           new Model
            {
                Id = 5,
                WheelSize = 20,
                MaxPassengerWeight = 100,
                BikeWeight = 10.4f,
                BrakeType = "Rim",
                ModelYear = 2022,
                PricePerHour = 4.50m,
                BikeType = BikeType.Track
            },
           new Model
            {
                Id = 6,
                WheelSize = 24,
                MaxPassengerWeight = 95,
                BikeWeight = 9.8f,
                BrakeType = "Rim",
                ModelYear = 2021,
                PricePerHour = 3.90m,
                BikeType = BikeType.Mini
            },
            new Model
            {
                Id = 7,
                WheelSize = 27.5f,
                MaxPassengerWeight = 115,
                BikeWeight = 12.1f,
                BrakeType = "Disc mechanical",
                ModelYear = 2024,
                PricePerHour = 6.20m,
                BikeType = BikeType.Mountain
            },
            new Model
            {
                Id = 8,
                WheelSize = 29,
                MaxPassengerWeight = 140,
                BikeWeight = 18.3f,
                BrakeType = "Disc hydraulic",
                ModelYear = 2023,
                PricePerHour = 9.10m,
                BikeType = BikeType.Electric
            },
           new Model
            {
                Id = 9,
                WheelSize = 28,
                MaxPassengerWeight = 105,
                BikeWeight = 8.9f,
                BrakeType = "Disc hydraulic",
                ModelYear = 2025,
                PricePerHour = 10.50m,
                BikeType = BikeType.City
            },
            new Model
            {
                Id = 10,
                WheelSize = 26,
                MaxPassengerWeight = 135,
                BikeWeight = 16.5f,
                BrakeType = "Drum",
                ModelYear = 2022,
                PricePerHour = 5.20m,
                BikeType = BikeType.City
            }
        ]);

        Bikes.AddRange([
           new Bike
            {
                Id = 1,
                SerialNumber = "202501001",
                Color = "Black",
                ModelId = 1,
                Model = Models[0]
            },
            new Bike
            {
                Id = 2,
                SerialNumber = "2024R01015",
                Color = "Red",
                ModelId = 2,
                Model = Models[1]
            },
            new Bike
            {
                Id = 3,
                SerialNumber = "2023X03210",
                Color = "Blue",
                ModelId = 3,
                Model = Models[2]
            },
            new Bike
            {
                Id = 4,
                SerialNumber = "2025B05077",
                Color = "Olive",
                ModelId = 4,
                Model = Models[3]
            },
            new Bike
            {
                Id = 5,
                SerialNumber = "2022G06342",
                Color = "Yellow",
                ModelId = 5,
                Model = Models[4]
            },
            new Bike
            {
                Id = 6,
                SerialNumber = "2021W08908",
                Color = "White",
                ModelId = 6,
                Model = Models[5]
            },
            new Bike
            {
                Id = 7,
                SerialNumber = "2024O04556",
                Color = "Orange",
                ModelId = 7,
                Model = Models[6]
            },
            new Bike
            {
                Id = 8,
                SerialNumber = "2023G09999",
                Color = "Graphite",
                ModelId = 8,
                Model = Models[7]
            },
            new Bike
            {
                Id = 9,
                SerialNumber = "2025S01555",
                Color = "Silver",
                ModelId = 9,
                Model = Models[8]
            },
            new Bike
            {
                Id = 10,
                SerialNumber = "2022T12640",
                Color = "Turquoise",
                ModelId = 10,
                Model = Models[9]
            }
        ]);


        Renters.AddRange([
            new Renter
            {
                Id = 1,
                LastName = "Kovalev",
                Name = "Dmitry",
                MiddleName = "Ilyich",
                PhoneNumber = "+7 901 111-11-11"
            },
            new Renter
            {
                Id = 2,
                LastName = "Egorova",
                Name = "Sofia",
                MiddleName = "Antonovna",
                PhoneNumber = "+7 902 222-22-22"
            },
            new Renter
            {
                Id = 3,
                LastName = "Leontiev",
                Name = "Maxim",
                MiddleName = "Olegovich",
                PhoneNumber = "+7 903 333-33-33"
            },
            new Renter
            {
                Id = 4,
                LastName = "Romanova",
                Name = "Maria",
                MiddleName = "Sergeevna",
                PhoneNumber = "+7 904 444-44-44"
            },
            new Renter
            {
                Id = 5,
                LastName = "Gusev",
                Name = "Igor",
                MiddleName = "Valerievich",
                PhoneNumber = "+7 905 555-55-55"
            },
           new Renter
            {
                Id = 6,
                LastName = "Frolova",
                Name = "Alena",
                MiddleName = "Alexandrovna",
                PhoneNumber = "+7 906 666-66-66"
            },
            new Renter
            {
                Id = 7,
                LastName = "Semenov",
                Name = "Pavel",
                MiddleName = "Andreevich",
                PhoneNumber = "+7 907 777-77-77"
            },
           new Renter
            {
                Id = 8,
                LastName = "Morozova",
                Name = "Ekaterina",
                MiddleName = "Dmitrievna",
                PhoneNumber = "+7 908 888-88-88"
            },
           new Renter
            {
                Id = 9,
                LastName = "Nazarov",
                Name = "Artur",
                MiddleName = "Petrovich",
                PhoneNumber = "+7 909 999-99-99"
            },
            new Renter
            {
                Id = 10,
                LastName = "Voronova",
                Name = "Olga",
                MiddleName = "Igorevna",
                PhoneNumber = "+7 900 000-00-00"
            }
        ]);

        CreateRent(1, new DateTime(2025, 8, 2, 9, 0, 0), 2, 0, 0);
        CreateRent(2, new DateTime(2025, 8, 3, 14, 30, 0), 3, 1, 1);
        CreateRent(3, new DateTime(2025, 8, 5, 10, 15, 0), 1, 2, 2);
        CreateRent(4, new DateTime(2025, 8, 7, 16, 0, 0), 4, 3, 3);
        CreateRent(5, new DateTime(2025, 8, 10, 11, 45, 0), 5, 4, 4);
        CreateRent(6, new DateTime(2025, 8, 12, 13, 0, 0), 2, 5, 5);
        CreateRent(7, new DateTime(2025, 8, 14, 15, 30, 0), 3, 6, 6);
        CreateRent(8, new DateTime(2025, 8, 16, 9, 30, 0), 4, 7, 7);
        CreateRent(9, new DateTime(2025, 8, 18, 12, 0, 0), 1, 8, 8);
        CreateRent(10, new DateTime(2025, 8, 20, 17, 0, 0), 2, 9, 9);
        CreateRent(11, new DateTime(2025, 9, 1, 10, 0, 0), 3, 0, 1);
        CreateRent(12, new DateTime(2025, 9, 3, 14, 0, 0), 5, 1, 2);
        CreateRent(13, new DateTime(2025, 9, 5, 16, 30, 0), 2, 2, 3);
        CreateRent(14, new DateTime(2025, 9, 7, 11, 0, 0), 4, 3, 4);
        CreateRent(15, new DateTime(2025, 9, 9, 13, 0, 0), 1, 4, 5);
        CreateRent(16, new DateTime(2025, 9, 12, 15, 0, 0), 3, 5, 6);
        CreateRent(17, new DateTime(2025, 9, 14, 9, 0, 0), 2, 6, 7);
        CreateRent(18, new DateTime(2025, 9, 16, 12, 30, 0), 5, 7, 8);
        CreateRent(19, new DateTime(2025, 9, 18, 14, 0, 0), 3, 8, 9);
        CreateRent(20, new DateTime(2025, 9, 20, 16, 0, 0), 4, 9, 0);
    }

    /// <summary>
    /// Creates a rental object with the specified parameters.
    /// </summary>
    /// <param name="id">Rental ID.</param>
    /// <param name="startTime">Rental start time.</param>
    /// <param name="duration">Rental duration in hours.</param>
    /// <param name="bikeIndex">Bike index in the Bikes collection.</param>
    /// <param name="renterIndex">Renter index in the Renters collection.</param>
    private void CreateRent(
        int id,
        DateTime startTime,
        int duration,
        int bikeIndex,
        int renterIndex)
    {
        var bike = Bikes.ElementAtOrDefault(bikeIndex)
            ?? throw new InvalidOperationException($"Bike at index {bikeIndex} not found in Bikes collection.");

        var renter = Renters.ElementAtOrDefault(renterIndex)
            ?? throw new InvalidOperationException($"Renter at index {renterIndex} not found in Renters collection.");

        var rent = new Rent
        {
            Id = id,
            StartTime = startTime,
            Duration = duration,
            BikeId = bike.Id,
            Bike = bike,
            RenterId = renter.Id,
            Renter = renter
        };

        Rents.Add(rent);
    }
}