using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Abstractions.Data;
public interface IEventRepository
{
    void Add(Event Event);
    Task<bool> IsEventExistAsync(string name);
    Task<Event> GetByIdAsync(int id);
}
