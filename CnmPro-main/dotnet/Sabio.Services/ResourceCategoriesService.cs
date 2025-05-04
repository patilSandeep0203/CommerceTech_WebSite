using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models.Requests.ResourceCategories;
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
    public class ResourceCategoriesService : IResourceCategoriesService
    {
        private IAuthenticationService<int> _authService = null;

        IDataProvider _data = null;

        public ResourceCategoriesService(IDataProvider data, ILookUpService lookUpService,
            IAuthenticationService<int> authService
        )
        {
            _data = data;
            _authService = authService;
        }

        public List<LookUp> GetAll()
        {
            string procName = "[dbo].[ResourceCategories_SelectAll]";

            List<LookUp> list = null;

            _data.ExecuteCmd(procName, inputParamMapper: null, delegate (IDataReader reader, short set) 
            {
                int startingIndex = 0;
                LookUp resourceCat = MapSingleCategory(reader, ref startingIndex);

                if (list == null)
                {
                    list = new List<LookUp>();
                }
                list.Add(resourceCat);
            }
           );

            return list;
        }

        public int Create(ResourceCategoryAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[ResourceCategories_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {

                    col.AddWithValue("@Category", model.Category);

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

        public void Update(ResourceCategoryUpdateRequest model)
        {
            string procName = "[dbo].[ResourceCategories_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);
                }, returnParameters: null);
        }

        public LookUp MapSingleCategory(IDataReader reader, ref int startingIndex)
        {
            LookUp category = new LookUp();

            category.Id = reader.GetSafeInt32(startingIndex++);
            category.Name = reader.GetSafeString(startingIndex++);

            return category;
        }

        private static void AddCommonParams(ResourceCategoryAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@Category", model.Category);
        }
    }
}
