namespace Template.Model.Exceptions;
public class BadRequestException : Exception
{
    public BadRequestException()
    {
    }

    public BadRequestException(string msg) : base(msg) { }
}
