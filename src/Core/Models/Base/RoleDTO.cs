using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class RoleDTO
    {

        public byte Id { get; set; }
        /// <summary>
        /// [1 =&gt; [Admin , SuperAdmin], 2 =&gt; [ Client , Servant , Organization ]]
        /// </summary>
        public sbyte Type { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
