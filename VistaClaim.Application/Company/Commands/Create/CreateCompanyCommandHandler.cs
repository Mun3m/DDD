using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using VistaClaim.Application.Interfaces;
using VistaClaim.Domain.Entities.CompanyEntity.Properties;
using VistaClaim.Entities._Base;

namespace VistaClaim.Application.Company.Commands.Create
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
    {
        public CreateCompanyCommandHandler()
        {

        }

        private readonly IUnitOfWork unitOfWork;
        private readonly ICompanyRepository companyRepository;

        //public CreateCompanyCommandHandler(IUnitOfWork unitOfWork, ICompanyRepository companyRepository)
        //{
        //    this.unitOfWork = unitOfWork;
        //    this.companyRepository = companyRepository;
        //}

        public async Task<Unit> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.CompanyEntity.Company(
                EntityId.FromGuid(request.Id),
                CompanyName.FromString(request.Name),
                CompanyCode.FromString(request.Code));

            await companyRepository.Add(entity);
            await unitOfWork.Commit();

            return Unit.Value;
        }
    }
}
