namespace salesSystem.api.utilidad
{
  public class Response<T>
  {
    public bool status { get;set; }
    public T Value { get; set; }
    public string msg { get; set; }

  }
}
