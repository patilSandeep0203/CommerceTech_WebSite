using Microsoft.Extensions.Options;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.AppSettings;
using Sabio.Models.Domain.Subscriptions;
using Sabio.Models.Requests.StripeServiceRequests;
using Sabio.Services.Interfaces;
using Stripe;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sabio.Services.StripeService
{
    public class StripeService : IStripeService
    {
        IDataProvider _data = null;
        private AppKeys _appKeys;

        public StripeService(IOptions<AppKeys> appKeys, IDataProvider data)
        {
            _data = data;
            _appKeys = appKeys.Value;
        }

        public StripeList<Customer> GetCustomers()
        {
            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

            CustomerListOptions options = new CustomerListOptions
            {
                Limit = 25,
            };
            CustomerService service = new CustomerService();
            StripeList<Customer> customers = service.List(options);
            return customers;
        }
        public StripeList<Product> GetProduct()
        {

            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

            ProductListOptions options = new ProductListOptions
            {
                Limit = 3,
            };
            ProductService service = new ProductService();
            StripeList<Product> products = service.List(options);
            return (products);
        }
        public Subscription CreateSubscription(string customerId, string priceId)
        {

            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

            SubscriptionCreateOptions options = new SubscriptionCreateOptions
            {
               Customer = $"{customerId}",
               Items = new List<SubscriptionItemOptions>
               {
                   new SubscriptionItemOptions
                   {
                       Price = $"{priceId}",
                   },
               },
            };
            SubscriptionService service = new SubscriptionService();
            Subscription subscriptions = service.Create(options);
            return (subscriptions);
        }
        public Price GetPrice(string priceId)
        {

            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;
           
            PriceService service = new PriceService();
            Price price = service.Get(priceId);
            return (price);
        }
        public Invoice GetInvoice(string invoiceId)
        {
            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;
            InvoiceService service = new InvoiceService();
            Invoice invoice = service.Get(invoiceId);
            return invoice;
        }
        public Customer GetCustomer(string userId)
        {
            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

            CustomerService service = new CustomerService();
            Customer customer = service.Get(userId);
            return customer;
        }
        public Subscription GetSubscription(string subId)
        {
            StripeConfiguration.ApiKey = _appKeys.StripeConfigurationApiKey;

            SubscriptionService service = new SubscriptionService();
            Subscription subscription = service.Get(subId);

            return subscription;
        }

        public int AddPurchase(SubscriptionBuyAddRequest model, int id)
        {
            string procName = "[dbo].[SubscriptionTransactions_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@PurchaseAmount", model.PurchaseAmount);
                    col.AddWithValue("@PurchaseDate", model.PurchaseDate);
                    
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
        public int AddCancellation(SubscriptionCancelAddRequest model, int id)
        {
            string procName = "[dbo].[SubscriptionTransactions_Insert_Cancellation]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@CancellationDate", model.CancellationDate);
                    col.AddWithValue("@RefundedAmount", model.RefundedAmount);

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

        public int addSubscriptionStatus(SubscriptionStatusAddRequest model, int id)
        {
            string procName = "[dbo].[UserSubscriptionStatus_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                    {
                        AddStatusParams(model, col);
                        
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
        public void UpgradeSubscriptionStatus(SubscriptionStatusUpdateRequest model, int id)
        {
            string procName = "[dbo].[UserSubscriptionStatus_Update_Upgrade]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", model.Id);
                    AddStatusParams(model, col);

                },
                returnParameters: null);
        }
        public void CancelSubscriptionStatus(SubscriptionStatusCancelRequest model, int id)
        {
            string procName = "[dbo].[UserSubscriptionStatus_Update_Cancellation]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Id", model.Id);
                    col.AddWithValue("@SubscriptionEndDate", model.SubscriptionEndDate);
                    col.AddWithValue("@isSubscribed", model.isSubscribed);
                }, 
                returnParameters: null);
        }

        public Paged<ActiveSubscription> SelectAll (int pageIndex, int pageSize)
        {
            Paged<ActiveSubscription> pagedList = null;
            List<ActiveSubscription> list = null;
            int totalCount = 0;
            string procName = "[dbo].[UserSubscriptionStatus_SelectAll]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                ActiveSubscription subscriptionStatus = MapActiveStatus(reader, ref startingIndex);
                totalCount = reader.GetSafeInt32(startingIndex++);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<ActiveSubscription>();
                }
                list.Add(subscriptionStatus);
            });
            if (list != null)
            {
                pagedList = new Paged<ActiveSubscription>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        public SubscriptionStatus GetSubscriptionStatusById(int userId)
        {
            SubscriptionStatus subscriptionStatus = null;
            string procName = "[dbo].[UserSubscriptionStatus_SelectById]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@UserId", userId);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                subscriptionStatus = MapSingleStatus(reader, ref startingIndex);
            });
            return subscriptionStatus;
        }

        public Paged<ActiveSubscription> GetActiveSubscriptions(int pageIndex, int pageSize)
        {
            Paged<ActiveSubscription> pagedList = null;
            List<ActiveSubscription> list = null;
            int totalCount = 0;
            string procName = "[dbo].[UserSubscriptionStatus_Select_Active]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@PageIndex", pageIndex);
                paramCollection.AddWithValue("@PageSize", pageSize);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                ActiveSubscription subscriptionStatus = MapActiveStatus(reader, ref startingIndex);
                totalCount = reader.GetSafeInt32(startingIndex++);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<ActiveSubscription>();
                }
                list.Add(subscriptionStatus);
            });
            if (list != null)
            {
                pagedList = new Paged<ActiveSubscription>(list, pageIndex, pageSize, totalCount);
            }
            return pagedList;
        }

        private SubscriptionStatus MapSingleStatus(IDataReader reader, ref int startingIndex)
        {
            SubscriptionStatus subscriptionStatus = new SubscriptionStatus();
            subscriptionStatus.UserId = reader.GetSafeInt32(startingIndex++);
            subscriptionStatus.Name = reader.GetSafeString(startingIndex++);
            subscriptionStatus.SubscriptionStartDate = reader.GetSafeDateTime(startingIndex++);
            subscriptionStatus.SubscriptionEndDate = reader.GetSafeDateTime(startingIndex++);
            subscriptionStatus.isSubscribed = reader.GetBoolean(startingIndex++);

            return subscriptionStatus;
        }

        private ActiveSubscription MapActiveStatus(IDataReader reader, ref int startingIndex)
        {
            ActiveSubscription subscriptionStatus = new ActiveSubscription();
            subscriptionStatus.UserId = reader.GetSafeInt32(startingIndex++);
            subscriptionStatus.Name = reader.GetSafeString(startingIndex++);
            subscriptionStatus.SubscriptionStartDate = reader.GetSafeDateTime(startingIndex++);

            return subscriptionStatus;
        }

        private static void AddCommonParams(SubscriptionAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@SubscriptionId", model.SubscriptionId);
            col.AddWithValue("@PaymentAcctId", model.PaymentAcctId);
            col.AddWithValue("@UserId", model.UserId);
            col.AddWithValue("@StripeInvoiceId", model.StripeInvoiceId);
            col.AddWithValue("@StripeInvoiceUrl", model.StripeInvoiceUrl);
            col.AddWithValue("@StripeSubscriptionId", model.StripeSubscriptionId);
        }
        private static void AddStatusParams(SubscriptionStatusAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@SubscriptionId", model.SubscriptionId);
            col.AddWithValue("@UserId", model.UserId);
            col.AddWithValue("@StripeInvoiceId", model.SubscriptionStartDate);
            col.AddWithValue("@StripeInvoiceUrl", model.isSubscribed);

        }
    }
}
