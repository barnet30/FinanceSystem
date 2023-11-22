using AutoMapper;

namespace FinanceSystem.Abstractions.Interfaces;

/// <summary>
/// Резолвер для подстановки сущностей по идентификатору
/// </summary>
public interface IReferenceItemMapResolver<TEntity> : IMemberValueResolver<object, IEntity, int, TEntity>
    where TEntity : IReferenceEntity
{ }