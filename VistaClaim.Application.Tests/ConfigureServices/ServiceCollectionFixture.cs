using AutoMapper;
using EventStore.ClientAPI;
using System;
using VistaClaim.Application.Common.Interfaces;
using VistaClaim.Application.Interfaces;
using VistaClaim.Application.Tests.Common.Mappings;
using VistaClaim.Persistence.EF;
using VistaClaim.Persistence.EventStore;
using VistaClaim.Persistence.Repositories;
using Xunit;

namespace VistaClaim.Application.Tests.ConfigureServices
{
    [CollectionDefinition("ServiceCollection")]
    public class ServiceCollectionFixture : ICollectionFixture<ServiceCollectionFixture>, IDisposable
    {
        private MappingTestsFixture _mappingTestsFixture { get; set; }

        public ApplicationContextEF DbContext { get; private set; }
        public IUnitOfWork UnitOfWork { get; private set; }
        public ICompanyRepository CompanyRepository { get; private set; }
        public IMapper Mapper => _mappingTestsFixture.Mapper;

        public ServiceCollectionFixture()
        {
            var dbContext = new EFApplicationContextFixture();

            DbContext = dbContext.Instance;
            UnitOfWork = new EFUnitOfWork(dbContext.Instance);
            CompanyRepository = new CompanyRepository(dbContext.Instance);
            _mappingTestsFixture = new MappingTestsFixture();
        }

        public void Dispose()
        {
            UnitOfWork = null;
            CompanyRepository = null;
            CompanyRepository = null;
            _mappingTestsFixture = null;
        }
    }
}