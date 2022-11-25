namespace HMS.Business.Exceptions
{

    public class UserNotFoundExceptionException : Exception
    {
        public UserNotFoundExceptionException(string msg) : base(msg) { }

    }
}