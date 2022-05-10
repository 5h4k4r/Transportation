using Core.Models;
using Core.Repositories;
using Infra.Responses;

namespace Infra.Responses;

public class ListTasksByClient
{
    public TaskResponse? Task { get; set; }
    public MemberDTO? Client { get; set; }

}
