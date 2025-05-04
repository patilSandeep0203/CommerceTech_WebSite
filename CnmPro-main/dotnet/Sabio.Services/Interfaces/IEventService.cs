using Sabio.Models.Domain.Events;
using Sabio.Models.Requests.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sabio.Models.Interfaces
{
    public interface IEventService
    {
        int AddEvent(EventAddRequest model, int id);
        Event GetEventById(int id);
        List<EventBase> GetEventByOrganization(int id);
        Paged<Event> GetSearchedEvents(int pageIndex, int pageSize, string query);
        Paged<Event> GetSearchedEventsByLocation(int pageIndex, int pageSize, string query);
        Paged<EventSearchResult> GetSearchedEventsWithLocationRadius(int pageIndex, int pageSize, string query, double latParam, double longParam, int distance);
        Paged<Event> SelectEventByCreatedBy(int CreatedBy, int pageIndex, int pageSize);
        Paged<Event> SelectAll(int pageIndex, int pageSize);
        void Update(EventUpdateRequest model);
        void DeleteEventById(int id);
    }
}
