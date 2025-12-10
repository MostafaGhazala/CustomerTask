
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
        public async Task TakeNumberAsync()
        {
            //get all Numbers 
            /*
              1- get bigger number from numbers and not reserved 
              2- get all reserved numbers which are greater than bigger number
              3- if there is no reserved number greater than bigger number then insert bigger number +1
              4- will use search binary with sorted reserved numbers to get the first reserved number greater than bigger number and not reserved
              5-  
             */
            //1
            var biggerNumber = await _unitOfWork.Numbers.GetBiggestNumber();

            //2
            var reservedNumbers =  _unitOfWork.ReservedNumbers.GetAllAsQuery(x=>x.ReservedNumber>biggerNumber)
                                    .OrderBy(x=>x.ReservedNumber).Select(x=>x.ReservedNumber).ToHashSet();
            //3
                var newNumber = new Numbers();
                newNumber.Number = biggerNumber + 1;
            if (reservedNumbers.Count != 0)
            {
                var res = reservedNumbers.Contains(newNumber.Number);
                while (res)
                {
                    newNumber.Number += 1;
                    res = reservedNumbers.Contains(newNumber.Number);
                }
            } 
            
            await _unitOfWork.Numbers.AddAsync(newNumber);
            await _unitOfWork.SaveAsync();
            return;

            //i want to insert number last number biggest number in numbers and the same time not one of reserved number 
            // Binary search 



        }
        public static int BinarySearch(List<int> numbers, int target)
        {
            int left = 0;
            int right = numbers.Count - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (numbers[mid] == target)
                    return mid;           

                if (numbers[mid] < target)
                    left = mid + 1;       
                else
                    right = mid - 1;      
            }

            return -1; 
        }
    }
}
