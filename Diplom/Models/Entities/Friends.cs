namespace Diplom.Models.Entities
{
    public class Friends
    {
        public long Id { get; set; }
        public string User1Id { get; set; }
        public MyUser User1 { get; set; }
        public string User2Id { get; set; }
        public MyUser User2 { get; set; }

    }
}
