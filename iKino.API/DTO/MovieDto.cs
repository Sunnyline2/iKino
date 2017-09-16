using System;

namespace iKino.API.Dto
{
    public class MovieDto
    {
        public Guid MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
