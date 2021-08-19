using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Service.IServices
{
    public interface ITourService : IBusinessService<Tour, TourDTO>
    {
        List<HomeTourDTO> GetTodayTours(float lat, float longs, string AgentId);
        TourCheckpointDetailsDTO GetTourPoints(int tourId);
        List<TourCommentDTO> GetAdminComments(int tourId);
        bool ChangeTourState(int tourId, int state);
        List<HomeTourDTO> GetTourHistory(float lat, float longs, string id);
        List<TourTemplateDTO> GetTemplates(string searchText,int take);
        TourCreateDTO InsertTour(TourCreateDTO tour);
        bool ActiveDisActiveTemplate(int tourId, bool state);
    }
}
