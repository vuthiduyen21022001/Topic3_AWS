using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace UploadAws.Sales
{
    public class SaleDto : AuditedEntityDto<Guid>
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
        public bool UseCredentials { get; set; }
        public bool UseTemporaryCredentials { get; set; }
        public bool UseTemporaryFederatedCredentials { get; set; }
        public string ProfileName { get; set; }
        public string ProfilesLocation { get; set; }
        public string Region { get; set; }
        public string Name { get; set; }
        public string Policy { get; set; }
        public double DurationSeconds { get; set; }
        public string ContainerName { get; set; }
        public bool CreateContainerIfNotExists { get; set; }

    }
}
