using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Aplication.Customers.Create
{
    internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler()
        {
        }

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository ?? throw new ArgumentException(nameof(customerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
          try{
              if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            {
                return Error.Validation("Customer.PhonNumber", "El numero de telefono no es valido");
            }

            if (Address.Create(command.Country, command.Line1, command.Line2, command.City,
                        command.State, command.ZipCode) is not Address address)
            {
                return Error.Validation("Custoer.Address", " la direccion no es valida");
            }

            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                command.Name,
                command.LastName,
                command.Email,
                phoneNumber,
                address,
                true
            );

            _customerRepository.add(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;

          }catch(Exception ex){
            return Error.Failure("Customer.Failure:", ex.Message);
          }
        }

    }
}