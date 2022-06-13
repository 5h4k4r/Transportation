using Core.Models.Base;
using Core.Models.Responses;

namespace Core.Models.Common;

public class ListTasksByClient
{
    public TaskResponse? Task { get; set; }
    public MemberDto? Client { get; set; }
}