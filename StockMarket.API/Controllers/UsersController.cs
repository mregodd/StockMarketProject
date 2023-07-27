using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace StockMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    private Iuser _userservice;
    public UsersController(IGenericService UserService)
    {
        _hotelService = hotelService;

    }

    /// <summary>
    /// Get All Hotels
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var hotels = await _hotelService.GetAllHotels();
        return Ok(hotels); //200 + data
    }


    /// <summary>
    /// Get Hotel By Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("[action]/{id}")] //api/hotels/gethotelbyid/2
    public async Task<IActionResult> GetHotelById(int id)
    {
        var hotel = await _hotelService.GetHotelById(id);
        if (hotel != null)
        {
            return Ok(hotel); //200 + data
        }
        return NotFound(); //404
    }

    [HttpGet]
    [Route("[action]/{name}")]
    public async Task<IActionResult> GetHotelByName(string name)
    {
        var hotel = await _hotelService.GetHotelByName(name);
        if (hotel != null)
        {
            return Ok(hotel); //200 + data
        }
        return NotFound();
    }
    /// <summary>
    /// Create an Hotel
    /// </summary>
    /// <param name="hotel"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
    {

        var createdHotel = await _hotelService.CreateHotel(hotel);
        return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel); //201 + data
    }

    /// <summary>
    /// Update Hotel
    /// </summary>
    /// <param name="hotel"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("[action]")]
    public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
    {
        if (await _hotelService.GetHotelById(hotel.Id) != null)
        {
            return Ok(await _hotelService.UpdateHotel(hotel)); //200 + data
        }
        return NotFound();

    }
    /// <summary>
    /// Delete Hotel
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete]
    [Route("[action]/{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {

        if (await _hotelService.GetHotelById(id) != null)
        {
            await _hotelService.DeleteHotel(id);
            return Ok(); //200
        }
        return NotFound();
    }
}
}
