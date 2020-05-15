using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using BizCover.Repository.Cars;

namespace AspNetCore.Cars.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        CarRepository m_carRepository = new CarRepository();

        // GET api/cars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Result = new List<Car>();
            try
            {
                Result = await m_carRepository.GetAllCars();
                Result = Result.Where(x => x.Id == id).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(Result);
        }

        // GET api/cars
        // Optional GET api/cars?model=abc&year=1993
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string model, [FromQuery] int? year)
        {
            var Result = new List<Car>();
            try
            { 
                Result = await m_carRepository.GetAllCars();
                if (!string.IsNullOrEmpty(model))
                    Result = Result.Where(x => x.Model.ToLower().Contains(model)).ToList();

                if (year.HasValue)
                    Result = Result.Where(x => x.Year == year).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(Result);
        }

        // GET api/cars
        [HttpPost("discount")]
        public async Task<IActionResult> Discount([FromBody]List<int> param)
        {
            decimal ItemCost = 0;
            decimal TotalCost = 0;
            decimal Discount = 0;

            try
            {                
                var CarList = new List<Car>();
                CarList = await m_carRepository.GetAllCars();
                CarList = CarList.Where(x => param.ToList().Contains(x.Id)).ToList();

                if (CarList.Count == 0)
                    return Ok("No Car Selected");

                foreach (Car item in CarList)
                {
                    if (item.Year < 2000)
                    {
                        Discount = item.Price * (decimal)0.1;
                        ItemCost = item.Price - Discount;
                        TotalCost += ItemCost;
                    }
                    else
                        TotalCost += item.Price;
                }            

                if (CarList.Count > 2)
                {
                    Discount = TotalCost * (decimal)0.03;
                    TotalCost = TotalCost - Discount;
                }

                if (TotalCost > 100000)
                {
                    Discount = TotalCost * (decimal)0.05;
                    TotalCost = TotalCost - Discount;
                }

                TotalCost = decimal.Truncate(TotalCost);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(TotalCost);
        }


        // POST api/cars
        [HttpPost]
        public async Task<IActionResult> Post(Car value)
        {
            int NewValue = 0;
            try
            {
                var CarList = new List<Car>();
                CarList = await m_carRepository.GetAllCars();
                CarList = CarList.Where(x => x.Id == value.Id).ToList();

                if (CarList.Count>0)
                    return Ok("Same Id already exist");

                NewValue = await m_carRepository.Add(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Car Added");
        }

        // PUT api/cars
        [HttpPut]
        public async Task<IActionResult> Put(Car value)
        {
            try
            {
                var CarList = new List<Car>();
                CarList = await m_carRepository.GetAllCars();
                CarList = CarList.Where(x => x.Id == value.Id).ToList();

                if (CarList.Count == 0)
                    return Ok("Expected Car Not Exist");

                await m_carRepository.Update(value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

                return Ok("Car has been updated");
            }


    }
}
