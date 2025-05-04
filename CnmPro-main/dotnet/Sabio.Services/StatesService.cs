using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class StatesService : IStatesService
    {
        public IDataProvider _data = null;

        public StatesService(IDataProvider data)
        {
            _data = data;
        }

        public Paged<State> GetAll(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[States_SelectAll]";

            Paged<State> pagedList = null;
            List<State> list = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, (param) =>
            {
                param.AddWithValue("@PageIndex", pageIndex);
                param.AddWithValue("@PageSize", pageSize);
            }, (reader, recordSetIndex) =>
            {
                int startingIndex = 0;
                State aState = MapAState(reader, ref startingIndex);
                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (list == null)
                {
                    list = new List<State>();
                }

                list.Add(aState);
            });

            if (list != null)
            {
                pagedList = new Paged<State>(list, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }

        private static State MapAState(IDataReader reader, ref int startingIndex)
        {
            State aState = new State();

            aState.Id = reader.GetSafeInt32(startingIndex++);
            aState.Code = reader.GetSafeString(startingIndex++);
            aState.Name = reader.GetSafeString(startingIndex++);

            return aState;
        }
    }
}
