using System;

namespace Library
{
public class InvalidBoardFormatException: Exception
{
  public InvalidBoardFormatException(string message): base(message)
  {

  }
}
}