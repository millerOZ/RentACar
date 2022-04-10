namespace RentACar.Data.Entities
{
    public class Reserve
    {
        public int Id { get; set; }
        public DateTime DateReserve { get; set; }   
        public DateTime DateStartReserve { get; set; }   
        public DateTime DateFinishReserve { get; set; }   
        public String  PlaceFinishReserve { get; set; }   
        public Boolean  StartReserve { get; set; }   
    }
}
