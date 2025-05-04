using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.Locations;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class LocationService : ILocationService
    {
        IDataProvider _data = null;
        IUserProfileMapper _profileMapper = null;

        public LocationService(IDataProvider data, IUserProfileMapper profileMapper)
        {
            _data = data;
            _profileMapper = profileMapper;
        }

        #region Get Methods
        private Location MapSingleLocation(IDataReader reader, ref int startingIndex)
        {
            Location newLocation = new Location();
            newLocation.LocationType = new LookUp();

            newLocation.Id = reader.GetSafeInt32(startingIndex++);
            newLocation.LocationType.Id = reader.GetSafeInt32(startingIndex++);
            newLocation.LocationType.Name = reader.GetSafeString(startingIndex++);
            newLocation.LineOne = reader.GetSafeString(startingIndex++);
            newLocation.LineTwo = reader.GetSafeString(startingIndex++);
            newLocation.City = reader.GetSafeString(startingIndex++);
            newLocation.Zip = reader.GetSafeString(startingIndex++);

            newLocation.State = new State();
            newLocation.State.Id = reader.GetSafeInt32(startingIndex++);
            newLocation.State.Name = reader.GetSafeString(startingIndex++);
            newLocation.State.Code = reader.GetSafeString(startingIndex++);

            newLocation.Latitude = reader.GetSafeDouble(startingIndex++);
            newLocation.Longitude = reader.GetSafeDouble(startingIndex++);
            newLocation.DateCreated = reader.GetSafeDateTime(startingIndex++);
            newLocation.DateModified = reader.GetSafeDateTime(startingIndex++);
            newLocation.CreatedBy = reader.GetSafeInt32(startingIndex++);
            newLocation.ModifiedBy = reader.GetSafeInt32(startingIndex++);

            return newLocation;
        }

        public Location GetById(int id)
        {

            string procName = "[dbo].[Locations_Select_ById]";

            Location blog = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                blog = MapSingleLocation(reader, ref startingIndex);
            }
            );

            return blog;
        }

        public Paged<Location> GetAll(int pageIndex, int pageSize)
        {
            Paged<Location> pagedList = null;
            List<Location> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Locations_SelectAll]";

            _data.ExecuteCmd(
                procName
                , (param) =>

                {
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;
                    Location aLocation = MapSingleLocation(reader, ref startingIndex);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<Location>();
                    }

                    list.Add(aLocation);
                });
            if (list != null)
            {
                pagedList = new Paged<Location>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public Paged<Location> GetByAuthor(int authorId, int pageIndex, int pageSize)
        {
            Paged<Location> pagedList = null;
            List<Location> list = null;
            int totalCount = 0;
            string procName = "[dbo].[Locations_Select_ByCreatedBy]";

            _data.ExecuteCmd(
                procName
                , (param) =>

                {
                    param.AddWithValue("@CreatedBy", authorId);
                    param.AddWithValue("@PageIndex", pageIndex);
                    param.AddWithValue("@PageSize", pageSize);
                },
                (reader, recordSetIndex) =>
                {
                    int startingIndex = 0;
                    Location aLocation = MapSingleLocation(reader, ref startingIndex);

                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }

                    if (list == null)
                    {
                        list = new List<Location>();
                    }

                    list.Add(aLocation);
                });
            if (list != null)
            {
                pagedList = new Paged<Location>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public List<Location> GetByGeo(double lat, double lng, int distance)
        {
            List<Location> listOfLocations = null;

            string procName = "[dbo].[Locations_Select_ByGeo]";

            _data.ExecuteCmd(procName, (param) =>
            {
                param.AddWithValue("@Latitude", lat);
                param.AddWithValue("@Longitude", lng);
                param.AddWithValue("@Distance", distance);
            }, delegate (IDataReader reader, short set)
            {
                Location aLocation = new Location();

                int startingIndex = 0;

                aLocation = MapSingleLocation(reader, ref startingIndex);

                if (listOfLocations == null)
                {
                    listOfLocations = new List<Location>();
                }

                listOfLocations.Add(aLocation);
            }
            );

            return listOfLocations;
        }
      

        #endregion

        #region Create and Update Methods
        private static void AddCommonParams(LocationAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@LocationTypeId", model.LocationTypeId);
            col.AddWithValue("@LineOne", model.LineOne);
            col.AddWithValue("@LineTwo", model.LineTwo);
            col.AddWithValue("@City", model.City);
            col.AddWithValue("@Zip", model.Zip);
            col.AddWithValue("@StateId", model.StateId);
            col.AddWithValue("@Latitude", model.Latitude);
            col.AddWithValue("@Longitude", model.Longitude);
        }

        public int Add(LocationAddRequest model, int userId)
        {
            int Id = 0;

            string procName = "[dbo].[Locations_Insert]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@CreatedBy", userId);
                col.AddWithValue("@ModifiedBy", userId);

                SqlParameter IdOut = new SqlParameter("@Id", SqlDbType.Int);
                IdOut.Direction = ParameterDirection.Output;

                col.Add(IdOut);

            }, returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object oId = returnCollection["@Id"].Value;
                int.TryParse(oId.ToString(), out Id);
                Console.WriteLine("");
            });
            return Id;
        }

        public void Update(LocationUpdateRequest model, int userId)
        {

            string procName = "[dbo].[Locations_Update]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@ModifiedBy", userId);

                col.AddWithValue("@Id", model.Id);

            }, returnParameters: null);
        }

        #endregion

        #region Delete Method
        public void Delete(int id)
        {
            string procName = "[dbo].[Locations_Delete_ById]";
            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);

            }, returnParameters: null);
        }
        #endregion

    }


}
