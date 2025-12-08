using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerTask.Core.Dtos;

namespace CustomerTask.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        //Task<CustomerDto> GetCustomerForEditAsync(int id);
        Task<CustomerDto> GetCustomerWithLookupsAsync(int? customerId = null); // For Create/Edit GET
        Task<int> CreateCustomerAsync(CustomerDto model);
        Task UpdateCustomerAsync(CustomerDto model);
        Task<int> DeleteCustomerAsync(int id);
        Task TakeNumberAsync();
    }
}
