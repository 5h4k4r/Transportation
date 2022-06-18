namespace Core.Models.Base;

public class MemberDto
{
    public ulong Id { get; set; }
    public ulong UserId { get; set; }
    public bool Requester { get; set; }
    public double Lat { get; set; }
    public double Lng { get; set; }
    public int Price { get; set; }

    /// <summary>
    /// 1 =&gt; active, 0 =&gt; deactive
    /// </summary>
    public sbyte Status { get; set; }

    public string ModelType { get; set; } = null!;
    public ulong ModelId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserDto? User { get; set; }

    public ICollection<MemberPaymentTypeDto> MemberPaymentTypes { get; set; }
}