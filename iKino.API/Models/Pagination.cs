namespace iKino.API.Models
{
    public sealed class Pagination
    {
        public object Content { get; private set; }
        public int Page { get; private set; }
        public int Size { get; private set; }

        private Pagination()
        {

        }

        public static Pagination Create(object content, int page, int size)
        {
            return new Pagination
            {
                Content = content,
                Page = page,
                Size = size,
            };
        }
    }
}