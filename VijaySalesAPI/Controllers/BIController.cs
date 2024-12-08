using JWTWebApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VijaySalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BIController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("getCustomer/{id}")]
        public async Task<IActionResult> GetCustomerProfile(int id)
        {
            var api = "http://localhost:5120/api/User";
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(api);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(new{ message="customer not found!" });
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var userData = JsonConvert.DeserializeObject<List<User>>(content);

                    var user = userData.Find(x => x.Id == id);
                    if (user == null) return BadRequest(new { message = "customer not found!" });
                    return Ok(user);
                }

             }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpGet("{id}")]
        //public string GetPaymentHistory(int id)
        //{
        //    return "value";
        //}

        [HttpGet("getOrders/{id}")]
        public async Task<IActionResult> GetOrderHistory(int id)
        {
            //shipment api -- api/shipment/getshipment/id
            //order api --- api/orders/getorders/id
            var shipmentStatusAPI= "";
            var ordersAPI = "";


            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(ordersAPI);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(new { message = "customer not found!" });
                }
                else
                {
                    var orderResponse = await response.Content.ReadAsStringAsync();

                    var orderData = JsonConvert.DeserializeObject<List<Order>>(orderResponse);

                    List<CompleteOrder> orders = new List<CompleteOrder>();

                    foreach (Order order in orderData)
                    {
                        var shipmentResponse = await client.GetAsync(shipmentStatusAPI);
                        var status = await response.Content.ReadAsStringAsync();

                        var shipmentStatus = JsonConvert.DeserializeObject<string>(status);

                        orders.Add(new CompleteOrder {order, shipmentStatus});
                    }

                    return Ok(orders);
                }
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

    }
}
