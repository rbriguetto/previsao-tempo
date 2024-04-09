namespace PrevisaoTempo.Application.Exceptions;

public class PrevisaoTempoException: Exception
{
    public PrevisaoTempoException()
    {
    }

    public PrevisaoTempoException(string message)
        : base(message)
    {
    }

    public PrevisaoTempoException(string message, Exception inner)
        : base(message, inner)
    {
    }
}