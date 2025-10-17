
using AutoMapper;
using CustomerTask.Core.Dtos;
using CustomerTask.Core.Entites;
using CustomerTask.Core.Interfaces;

namespace CustomerTask.Services.Services
{
 
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private async Task<List<LookupDto>> GetLookupsWithParentAsync<T>(IGenericRepository<T> repository, Func<T, int?> parentSelector = null) where T : class
        {
            var entities = await repository.GetAllAsync();
            return entities.Select(e => new LookupDto
            {
                Id = (e as dynamic).Id,
                Name = (e as dynamic).Name,
                ParentId = parentSelector != null ? parentSelector(e) : null
            }).ToList();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync(x=>x.Gender); 
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerWithLookupsAsync(int? customerId = null)
        {
            var model = customerId.HasValue && customerId.Value > 0
                ? _mapper.Map<CustomerDto>(await _unitOfWork.Customers.GetByIdAsync(customerId.Value))
                : new CustomerDto();

            // Populate all look-up data
            model.Governorates = await GetLookupsWithParentAsync(_unitOfWork.Governorates);
            model.Districts = await GetLookupsWithParentAsync(_unitOfWork.Districts, d => (d as dynamic).GovernorateId);
            model.Villages = await GetLookupsWithParentAsync(_unitOfWork.Villages, v => (v as dynamic).DistrictId);
            model.Genders = await GetLookupsWithParentAsync(_unitOfWork.Genders);
            return model;
        }

        // --- WRITE Operations ---
        public async Task<int> CreateCustomerAsync(CustomerDto model)
        {
            var customerEntity = _mapper.Map<Customer>(model);
            await _unitOfWork.Customers.AddAsync(customerEntity);
            return await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCustomerAsync(CustomerDto model)
        {
            var existingCustomer = await _unitOfWork.Customers.GetByIdAsync(model.Id);

            if (existingCustomer != null)
            {
                _mapper.Map(model, existingCustomer);

                _unitOfWork.Customers.Update(existingCustomer);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<int> DeleteCustomerAsync(int id)
        {
            var customerEntity = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customerEntity != null)
            {
                _unitOfWork.Customers.Delete(customerEntity);
                return await _unitOfWork.SaveAsync();
            }
            return -1;
        }
    }
}
