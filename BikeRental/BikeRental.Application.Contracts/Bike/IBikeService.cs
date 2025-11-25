using BikeRental.Application.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Application.Contracts.Bike;
/// <summary>
/// Сервис для работы с велосипедами.
/// </summary>
public interface IBikeService : IApplicationService<BikeDto, BikeCreateUpdateDto, int>
{
    /// <summary>
    /// Получает список велосипедов указанной модели.
    /// </summary>
    public Task<IList<BikeDto>> GetBikesByModelAsync(int modelId);

    /// <summary>
    /// Получает DTO модели для указанного велосипеда.
    /// </summary>
    public Task<ModelDto?> GetModelByBikeIdAsync(int bikeId);
}