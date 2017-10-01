using System;

namespace eKino.Core.Domain
{
    public class Video
    {
        public Guid VideoId { get;  set; }
        public string Path { get;  set; }
        public VideoService VideoService { get;  set; }
    }

    public enum VideoService
    {
        YouTube,
        Dailymotion,
    }
}