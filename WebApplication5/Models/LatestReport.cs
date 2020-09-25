using System.Collections.Generic;

namespace WebApplication5.Models
{
  public class LatestReport
  {
    public string latest_update { get; set; }
    public List<Detail> details { get; set; }
    public string cachetype { get; set; }
  }
}
