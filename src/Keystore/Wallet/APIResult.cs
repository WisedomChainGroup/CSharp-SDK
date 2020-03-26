using System;

namespace C__SDK
{
    public class APIResult
    {
        public string data;

        public string message;

        public APIResult(string data, string message)
        {
            this.data = data;
            this.message = message;
        }

        public void setData(string data)
        {
            this.data = data;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }

        public string GetData()
        {
            return this.data;
        }

        public string GetMessage()
        {
            return this.message;
        }
    }
}