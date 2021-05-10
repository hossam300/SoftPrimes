using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
   public class CheckPointTourCommentDTO
    {
        public int Id { get; set; }
        public int TourCheckPointId { get; set; }
        public TourCheckPointDTO TourCheckPoint { get; set; }
        public int CommentId { get; set; }
        public CommentDTO Comment { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
