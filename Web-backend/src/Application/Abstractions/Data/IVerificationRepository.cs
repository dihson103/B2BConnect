
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IVerificationRepository
{
    public void Add(Verification verification);

    public void Update(Verification verification);  
}
