using System;

namespace Library
{
public class NegativePointsOrCoinsException: Exception
{
  public NegativePointsOrCoinsException(string message): base(message)
  {

  }
}
}