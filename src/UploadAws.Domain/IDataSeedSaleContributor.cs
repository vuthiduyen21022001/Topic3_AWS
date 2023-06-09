using System;
using System.Threading.Tasks;
using UploadAws.Sales;

using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace UploadAws;

public class UploadAwsDataSeederSaleContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Sale, Guid> _saleRepository;

    public UploadAwsDataSeederSaleContributor(IRepository<Sale, Guid> saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _saleRepository.GetCountAsync() <= 0)
        {
            await _saleRepository.InsertAsync(
                new Sale
                {
                    AccessKeyId = "AKIATNBK4DPL7SHEJN55",
                    SecretAccessKey = "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",
                    UseCredentials = false,
                    UseTemporaryCredentials = false,
                    UseTemporaryFederatedCredentials = false,
                    ProfileName = "the name of the profile to get credentials from",
                    ProfilesLocation = "https://hqsoftawsadmin.signin.aws.amazon.com/console",
                    Region = "ap-southeast-1",
                    Name = "demo01",
                    Policy = "{\r\n    " +
                     "\"Version\": \"2012-10-17\",\r\n    " +
                     "\"Statement\": [\r\n        {\r\n            " +
                     "\"Effect\": \"Allow\",\r\n           " +
                     " \"Action\": [\r\n                " +
                     "\"s3:*\",\r\n                " +
                     "\"s3-object-lambda:*\"\r\n            ],\r\n            " +
                     "\"Resource\": \"*\"\r\n        }\r\n    ]\r\n}",
                    DurationSeconds = 1000,
                    ContainerName = "awsdemo02",
                    CreateContainerIfNotExists = true
                },
                autoSave: true
            );

            await _saleRepository.InsertAsync(
                new Sale
                {
                    AccessKeyId = "AKIATNBK4DPL7SHEJN55",
                    SecretAccessKey = "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",
                    UseCredentials = false,
                    UseTemporaryCredentials = false,
                    UseTemporaryFederatedCredentials = false,
                    ProfileName = "the name of the profile to get credentials from",
                    ProfilesLocation = "https://hqsoftawsadmin.signin.aws.amazon.com/console",
                    Region = "ap-southeast-1",
                    Name = "demo01",
                    Policy = "{\r\n    " +
                     "\"Version\": \"2012-10-17\",\r\n    " +
                     "\"Statement\": [\r\n        {\r\n            " +
                     "\"Effect\": \"Allow\",\r\n           " +
                     " \"Action\": [\r\n                " +
                     "\"s3:*\",\r\n                " +
                     "\"s3-object-lambda:*\"\r\n            ],\r\n            " +
                     "\"Resource\": \"*\"\r\n        }\r\n    ]\r\n}",
                    DurationSeconds = 1000,
                    ContainerName = "awsdemo02",
                    CreateContainerIfNotExists = true
                },
                autoSave: true
            );
        }
    }
}
