using System;

namespace Library
{
public class InvalidArgumentException: Exception
{
  public InvalidArgumentException(string message): base(message)
  {

  }
}
}