namespace BikeRental.Application.Contracts;

/// <summary>
/// Generic service interface for managing DTOs (Data Transfer Objects).
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
 where TDto : class
 where TCreateUpdateDto : class
 where TKey : struct
{
    /// <summary>
    /// Creating a DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns></returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Getting a DTO by ID
    /// </summary>
    /// <param name="dtoId">DTO ID</param>
    /// <returns></returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Getting the entire list of DTOs
    /// </summary>
    /// <returns></returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Update DTO
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="dtoId">DTO ID</param> 
    /// <returns></returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Delete DTO
    /// </summary>
    /// <param name="dtoId">DTO ID</param>
    public Task<bool> Delete(TKey dtoId);
}