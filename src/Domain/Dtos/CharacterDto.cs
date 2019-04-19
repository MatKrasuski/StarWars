namespace Domain.Dtos
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Episodes { get; set; }
        public string Planet { get; set; }
        public string[] Friends { get; set; }
    }
}
