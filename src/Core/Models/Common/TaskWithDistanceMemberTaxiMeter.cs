using Core.Models.Base;
using Core.Models.Repositories;

namespace Core.Models.Common;

public class TaskWithDistanceMemberTaxiMeter
{
   public TaskDto Task { get; set; }
   public IEnumerable<DestinationDto>? DestinationDtos { get; set; }
   public TaxiMeterDto? TaxiMeter { get; set; }
   public MemberDto? Member { get; set; }
}