using HexagonalArch.API.Models;
using HexagonalArch.API.Repositories;

namespace HexagonalArch.API.Services;


public class CustomerService
{
    private readonly CustomerRepository _repository;

    public CustomerService(CustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer> SaveAsync(Customer customer)
    {
        await _repository.AddAsync(customer);
        return customer;
    }

    public async Task<Customer?> FindByIdAsync(long id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public async Task<Customer?> FindByCpfAsync(string cpf)
    {
        return await _repository.FindByCpfAsync(cpf);
    }

    public async Task<Customer?> FindByEmailAsync(string email)
    {
        return await _repository.FindByEmailAsync(email);
    }
}

