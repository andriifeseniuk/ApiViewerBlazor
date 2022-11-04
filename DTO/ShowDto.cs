namespace ApiViewerBlazor.DTO
{
    public class ShowDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual PersonDto[] Cast { get; set; }
    }
}