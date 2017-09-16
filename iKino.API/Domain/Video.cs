using System;

namespace iKino.API.Domain
{
    public class Video
    {
        public Guid VideoId { get; protected set; }
        public string Path { get; protected set; }
        public VideoService VideoService { get; protected set; }

        public Guid MovieId { get; protected set; }
        public virtual Movie Movie { get; protected set; }

        protected Video()
        {

        }
    }

    public enum VideoService
    {
        YouTube,
        Dailymotion,
    }
}