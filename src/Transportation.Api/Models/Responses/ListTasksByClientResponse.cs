using Tranportation.Api.Responses;

namespace Transportation.Api.Responses;

public class ListTasksByClientResponse
{
    public TaskResponse? Task { get; set; }
    public MemberResponse? Client { get; set; }

}
