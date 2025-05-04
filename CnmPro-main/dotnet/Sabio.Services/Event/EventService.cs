using Sabio.Data.Providers;
using Sabio.Models.Interfaces;
using Sabio.Models.Requests.Events;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabio.Models.Domain.Events;
using Sabio.Models.Requests;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Organizations;
using Sabio.Models;
using Sabio.Data;



namespace Sabio.Services
{
    public class EventService : IEventService
    {
        IDataProvider _data = null;
        IUserProfileMapper _profileMapper = null;
        ILookUpService _lookUp = null;
        public EventService(IDataProvider data, IUserProfileMapper profileMapper, ILookUpService lookUp)
        {
            _data = data;
            _profileMapper = profileMapper;
            _lookUp = lookUp;
        }
        public int AddEvent(EventAddRequest model, int userId)
        {
           int id = 0;

            string procName = "[dbo].[Events_InsertV2]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@CreatedBy", userId);
                    AddInsertParams(model, col);
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);

                });

            return id;
        }

        public Event GetEventById(int id)
        {
            Event events = null;
            string procName = "[dbo].[Events_SelectById]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                events = MapSingleEvent(reader, ref startingIndex);
            });

            return events;
        }


        public List<EventBase> GetEventByOrganization(int id)
        {
            List<EventBase> list = null;
            string procName = "[dbo].[Events_Select_ByOrganization_Base]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@OrganizationId", id);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                EventBase events = new EventBase();
                events.Id = reader.GetSafeInt32(startingIndex++);
                events.Title = reader.GetSafeString(startingIndex++);
                events.Description = reader.GetSafeString(startingIndex++);
                events.ImageUrl = reader.GetSafeString(startingIndex++);


                if (list == null)
                {
                    list = new List<EventBase>();
                }
                list.Add(events);
            });

            return list;
        }

        public Paged<Event> SelectEventByCreatedBy(int createdBy, int pageIndex, int pageSize)
        {
            Paged<Event> pagedList = null;
            List<Event> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Event_Select_ByCreatedBy]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@pageIndex", pageIndex);
                parameterCollection.AddWithValue("@pageSize", pageSize);
                parameterCollection.AddWithValue("@CreatedBy", createdBy);


            }, delegate (IDataReader reader, short set)
            {

                int startingIndex = 0;
                Event events = MapSingleEvent(reader, ref startingIndex);
                totalCount = reader.GetSafeInt32(startingIndex++);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Event>();
                }

                list.Add(events);
            });
            if (list != null)
            {
                pagedList = new Paged<Event>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<Event> SelectAll(int pageIndex, int pageSize)
        {
            Paged<Event> pagedList = null;
            List<Event> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Events_SelectAll]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
            {

                int startingIndex = 0;
                Event events = MapSingleEvent(reader, ref startingIndex);
                totalCount = reader.GetSafeInt32(startingIndex++);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<Event>();
                }

                list.Add(events);
            });
            if (list != null)
            {
                pagedList = new Paged<Event>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<Event> GetSearchedEvents(int pageIndex, int pageSize, string query)
        {
            Paged<Event> pagedList = null;
            List<Event> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Events_Search_Pagination]"
                , (param) =>

                {
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);
                    param.AddWithValue("@Query", query);
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;
                    Event events = MapSingleEvent(reader, ref startingIndex);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<Event>();
                    }

                    list.Add(events);
                });
            if (list != null)
            {
                pagedList = new Paged<Event>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<Event> GetSearchedEventsByLocation(int pageIndex, int pageSize, string query)
        {
            Paged<Event> pagedList = null;
            List<Event> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Events_Search_Pagination_By_Location_Only]"
                , (param) =>

                {
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);
                    param.AddWithValue("@Query", query);
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;
                    Event events = MapSingleEvent(reader, ref startingIndex);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<Event>();
                    }

                    list.Add(events);
                });
            if (list != null)
            {
                pagedList = new Paged<Event>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        public Paged<EventSearchResult> GetSearchedEventsWithLocationRadius(int pageIndex, int pageSize, string query, double latParam, double longParam, int distance)
        {
            Paged<EventSearchResult> pagedList = null;
            List<EventSearchResult> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(
                "[dbo].[Events_Search_Pagination_With_Location_Radius]"
                , (param) =>

                {
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);
                    param.AddWithValue("@Query", query);
                    param.AddWithValue("@Lat", latParam);
                    param.AddWithValue("@Long", longParam);
                    param.AddWithValue("@Distance", distance);
                },
                    (reader, recordSetIndex) =>
                    {
                        int startingIndex = 0;
                        EventSearchResult events = MapSingleEventWithDistance(reader, ref startingIndex);

                        if (totalCount == 0)
                        {
                            totalCount = reader.GetSafeInt32(startingIndex++);
                        }

                        if (list == null)
                        {
                            list = new List<EventSearchResult>();
                        }

                        list.Add(events);
                    });
            if (list != null)
            {
                pagedList = new Paged<EventSearchResult>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }
        public void Update(EventUpdateRequest model)
        {
            string procName = "[dbo].[Events_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", model.Id);
                    AddCommonParams(model, col);
                }, returnParameters: null);
        }
        public void DeleteEventById(int id)
        {
            string procName = "[dbo].[Events_Delete]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@id", id);
                }, returnParameters: null);

        }

        private Event MapSingleEvent(IDataReader reader, ref int startingIndex)
        {
            Event events = new Event();
            events.Location = new Location();
            events.Id = reader.GetSafeInt32(startingIndex++);
            events.EventType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            events.Title = reader.GetSafeString(startingIndex++);
            events.Description = reader.GetSafeString(startingIndex++);
            events.Location.Id = reader.GetSafeInt32(startingIndex++);
            events.ImageUrl = reader.GetSafeString(startingIndex++);
            events.Location.LocationType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            events.Location.LineOne = reader.GetSafeString(startingIndex++);
            events.Location.LineTwo = reader.GetSafeString(startingIndex++);
            events.Location.City = reader.GetSafeString(startingIndex++);
            events.Location.Zip = reader.GetSafeString(startingIndex++);
            events.Location.State = new State();
            events.Location.State.Id = reader.GetSafeInt32(startingIndex++);
            events.Location.State.Name = reader.GetSafeString(startingIndex++);
            events.Location.State.Code = reader.GetSafeString(startingIndex++);
            events.Location.Latitude = reader.GetSafeDouble(startingIndex++);
            events.Location.Longitude = reader.GetSafeDouble(startingIndex++);
            events.Organization = new Organization();
            events.Organization.Id = reader.GetSafeInt32(startingIndex++);
            events.Organization.OrganizationType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            events.Organization.Name = reader.GetSafeString(startingIndex++);
            events.Organization.Headline = reader.GetSafeString(startingIndex++);
            events.Organization.Description = reader.GetSafeString(startingIndex++);
            events.Organization.Logo = reader.GetSafeString(startingIndex++);
            events.Organization.Phone = reader.GetSafeString(startingIndex++);
            events.Organization.SiteUrl = reader.GetSafeString(startingIndex++);
            events.CreatedBy = _profileMapper.Map(reader, ref startingIndex);
            events.DateCreated = reader.GetSafeDateTime(startingIndex++);
            events.DateModified = reader.GetSafeDateTime(startingIndex++);

            return events;
        }

        private EventSearchResult MapSingleEventWithDistance(IDataReader reader, ref int startingIndex)
        {
            EventSearchResult events = new EventSearchResult();
            events.Id = reader.GetSafeInt32(startingIndex++);
            events.EventType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            events.Title = reader.GetSafeString(startingIndex++);
            events.Description = reader.GetSafeString(startingIndex++);
            events.ImageUrl = reader.GetSafeString(startingIndex++);
            events.Location = new Location();
            events.Location.Id = reader.GetSafeInt32(startingIndex++);
            events.Location.LocationType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            events.Location.LineOne = reader.GetSafeString(startingIndex++);
            events.Location.LineTwo = reader.GetSafeString(startingIndex++);
            events.Location.City = reader.GetSafeString(startingIndex++);
            events.Location.Zip = reader.GetSafeString(startingIndex++);
            events.Location.State = new State();
            events.Location.State.Id = reader.GetSafeInt32(startingIndex++);
            events.Location.State.Name = reader.GetSafeString(startingIndex++);
            events.Location.State.Code = reader.GetSafeString(startingIndex++);
            events.Location.Latitude = reader.GetSafeDouble(startingIndex++);
            events.Location.Longitude = reader.GetSafeDouble(startingIndex++);
            events.Organization = new Organization();
            events.Organization.Id = reader.GetSafeInt32(startingIndex++);
            events.Organization.OrganizationType = _lookUp.MapSingleLookUp(reader, ref startingIndex);
            events.Organization.Name = reader.GetSafeString(startingIndex++);
            events.Organization.Headline = reader.GetSafeString(startingIndex++);
            events.Organization.Description = reader.GetSafeString(startingIndex++);
            events.Organization.Logo = reader.GetSafeString(startingIndex++);
            events.Organization.Phone = reader.GetSafeString(startingIndex++);
            events.Organization.SiteUrl = reader.GetSafeString(startingIndex++);
            events.CreatedBy = _profileMapper.Map(reader, ref startingIndex);
            events.StartDate = reader.GetSafeDateTime(startingIndex++);
            events.EndDate = reader.GetSafeDateTime(startingIndex++);
            events.DateCreated = reader.GetSafeDateTime(startingIndex++);
            events.DateModified = reader.GetSafeDateTime(startingIndex++);
            events.Distance = reader.GetSafeDouble(startingIndex++);

            return events;
        }

        private static void AddCommonParams(EventAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@EventTypeId", model.EventTypeId);
            col.AddWithValue("@LocationId", model.LocationId);
            col.AddWithValue("@OrganizationId", model.OrganizationId);
            col.AddWithValue("@Title", model.Title);
            col.AddWithValue("@Description", model.Description);
            col.AddWithValue("@IsMultiDay", model.IsMultiDay);
            col.AddWithValue("@ImageUrl", model.ImageUrl);
            col.AddWithValue("@UserId", model.CreatedBy);

        }

        private static void AddInsertParams(EventAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@EventTypeId", model.EventTypeId);
            col.AddWithValue("@Title", model.Title);
            col.AddWithValue("@Description", model.Description);
            col.AddWithValue("@IsMultiDay", model.IsMultiDay);
            col.AddWithValue("@ImageUrl", model.ImageUrl);
            col.AddWithValue("@OrganizationId", model.OrganizationId);
            col.AddWithValue("@LocationTypeId", model.LocationTypeId);
            col.AddWithValue("@LineOne", model.LineOne);
            col.AddWithValue("@LineTwo", model.LineTwo);
            col.AddWithValue("@City", model.City);
            col.AddWithValue("@Zip", model.Zip);
            col.AddWithValue("@StateId", model.StateId);
            col.AddWithValue("@Latitude", model.Latitude);
            col.AddWithValue("@Longitude", model.Longitude);
        }
    }   
}

