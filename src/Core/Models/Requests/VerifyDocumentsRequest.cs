namespace Core.Models.Requests;

public class VerifyDocumentsRequest
{
    public List<ulong> documentIds { get; set; }
    public ulong ServantId { get; set; }
}