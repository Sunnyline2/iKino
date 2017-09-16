namespace iKino.API.Models
{
    public class ResponseBody
    {
        //public string Code { get; private set; }
        public string Message { get; private set; }

        private ResponseBody()
        {

        }

        public static ResponseBody Create(string code, string message)
        {
            return new ResponseBody
            {
                //Code = code,
                Message = message,
            };
        }

        public static ResponseBody Create(string message)
        {
            return new ResponseBody
            {
                Message = message,
            };
        }


    }
}