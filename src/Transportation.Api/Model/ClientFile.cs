using System;
using System.Collections.Generic;

namespace Transportation.Api.Model
{
  public partial class ClientFile
  {
    public ulong Id { get; set; }
    public ulong FileId { get; set; }
    public uint LanguageId { get; set; }
    public string Platform { get; set; } = null!;
    public string? Version { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual File File { get; set; } = null!;
    public virtual Language Language { get; set; } = null!;
  }

  public class a : ClientFile
  {
    public override Language Language { get; set; }
  }
}
