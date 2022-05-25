namespace Core.Models.Base
{
    public class ServantWorkDayDto
    {

        public ulong Id { get; set; }
        public DateOnly Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

 
    }
}
