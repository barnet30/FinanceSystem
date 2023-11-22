using AutoMapper;
using FinanceSystem.Abstractions.Interfaces;
using FinanceSystem.Data.Entities;
using FinanceSystem.Data.Interfaces;

namespace FinanceSystem.Services.Resolver;

public sealed class ReferenceItemMapResolver<TEntity> : IReferenceItemMapResolver<TEntity> where TEntity : BaseReferenceEntity
{
    private readonly IReferenceRepository<TEntity> _referenceRepository;

    public ReferenceItemMapResolver(IReferenceRepository<TEntity> referenceRepository)
    {
        _referenceRepository = referenceRepository;
    }

    public TEntity Resolve(object source, IEntity destination, int sourceMember, TEntity destMember, ResolutionContext context)
    {
        try
        {
            return _referenceRepository.GetByIdAsync(sourceMember).GetAwaiter().GetResult();;
        }
        catch
        {
            return null!;
        }
    }
}