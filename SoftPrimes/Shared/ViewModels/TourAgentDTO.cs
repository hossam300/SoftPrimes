using SoftPrimes.Shared.Domains;
using System;
using System.Collections.Generic;

namespace SoftPrimes.Shared.ViewModels
{
  public class TourAgentDTO
  {
    public int Id { get; set; }
    public int TourId { get; set; }
    public TourDTO Tour { get; set; }
    public DateTimeOffset TourDate { get; set; }
    public TourType TourType { get; set; }
    public List<TourCheckPointDTO> CheckPoints { get; set; }
    public string AgentId { get; set; }
    public AgentDTO Agent { get; set; }
    public TourState TourState { get; set; }
    public double EstimatedDistance { get; set; }
    public DateTimeOffset? EstimatedEndDate { get; set; }
    public List<TourStateLogDTO> TourStateLogs { get; set; }
    public List<TourCommentDTO> Comments { get; set; }
  }
}