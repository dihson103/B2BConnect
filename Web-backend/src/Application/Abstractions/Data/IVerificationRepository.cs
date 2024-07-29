
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IVerificationRepository
{
    public void Add(Verification verification);
    Task<Verification> GetById(Guid verId);
    public void Update(Verification verification);  
}
