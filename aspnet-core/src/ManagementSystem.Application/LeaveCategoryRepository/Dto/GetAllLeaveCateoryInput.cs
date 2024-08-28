using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.LeaveRepositorys.Dtos
{
    public class GetAllLeaveCateoryInput: PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string FilterText { get; set; }
        public string CategoryNameFilter { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
        public string Sorting { get; set; }
    }
}
