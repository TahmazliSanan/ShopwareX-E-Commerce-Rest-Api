using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopwareX.Dtos.GeneralResponse;
using ShopwareX.Dtos.Order;
using ShopwareX.Services.Abstracts;

namespace ShopwareX.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController(IOrderService orderService, IProductService productService)
        : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IProductService _productService = productService;

        [Authorize(Roles = "User")]
        [HttpPost("add")]
        public async Task<ActionResult<ApiResponse<OrderResponseDto>>>
            AddOrderAsync([FromBody] OrderCreateDto dto)
        {
            ApiResponse<OrderResponseDto> apiResponse;

            var user = User.FindFirst("Id");
            
            if (user is null)
            {
                apiResponse = new ApiResponse<OrderResponseDto>
                {
                    Message = "User doesn't exist",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            foreach (var item in dto.Items)
            {
                var existProduct = _productService.GetProductByIdAsync(item.ProductId);

                if (existProduct is null)
                {
                    apiResponse = new ApiResponse<OrderResponseDto>
                    {
                        Message = "Product doesn't exist",
                        Response = null
                    };

                    return NotFound(apiResponse);
                }
            }

            var userId = long.Parse(user.Value);
            var newOrder = await _orderService.AddOrderAsync(dto, userId);

            apiResponse = new ApiResponse<OrderResponseDto>
            {
                Message = "Order was created successfully",
                Response = newOrder
            };

            return Created($"api/order/get/{newOrder!.Id}", apiResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderResponseDto>>>>
            GetAllOrdersAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            var apiResponse = new ApiResponse<IEnumerable<OrderResponseDto>>
            {
                Message = "All orders were fetched successfully",
                Response = orders
            };

            return Ok(apiResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/{id:long}")]
        public async Task<ActionResult<ApiResponse<OrderResponseDto>>>
            GetOrderByIdAsync([FromRoute] long id)
        {
            var existOrder = await _orderService.GetOrderByIdAsync(id);
            ApiResponse<OrderResponseDto> apiResponse;

            if (existOrder is null)
            {
                apiResponse = new ApiResponse<OrderResponseDto>
                {
                    Message = "Order not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            apiResponse = new ApiResponse<OrderResponseDto>
            {
                Message = "Order was fetched successfully",
                Response = existOrder
            };

            return Ok(apiResponse);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id:long}")]
        public async Task<ActionResult<OrderResponseDto>> DeleteOrderAsync([FromRoute] long id)
        {
            var existOrder = await _orderService.GetOrderByIdAsync(id);
            ApiResponse<OrderResponseDto> apiResponse;

            if (existOrder is null)
            {
                apiResponse = new ApiResponse<OrderResponseDto>
                {
                    Message = "Order not found",
                    Response = null
                };

                return NotFound(apiResponse);
            }

            await _productService.DeleteProductByIdAsync(id);

            apiResponse = new ApiResponse<OrderResponseDto>
            {
                Message = "Order was deleted successfully",
                Response = existOrder
            };

            return Ok(apiResponse);
        }
    }
}
