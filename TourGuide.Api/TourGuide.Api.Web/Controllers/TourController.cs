using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourGuide.Api.Web.Entities;

namespace TourGuide.Api.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public TourController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tour>>> GetAll()
    {
        return await _dbContext.Tours
            .Select(p => new Tour
            {
                Id = p.Id,
                Name = p.Name,
                City = p.City,
                Theme = p.Theme,
                Reviews = p.Reviews,
                ImageUrls = p.ImageUrls,
                Price = p.Price,
                Description = p.Description,
                Activities = p.Activities,
                Includes = p.Includes,
                NotIncludes = p.NotIncludes,
                Safety = p.Safety,
                Languages = p.Languages,
                Duration = p.Duration,
                NumberOfPeople = p.NumberOfPeople,
                Address = p.Address
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tour>> GetTourById(int id)
    {
        var tour = await _dbContext.Tours
            .Where(t => t.Id == id)
            .Select(t => new Tour
            {
                Id = t.Id,
                Name = t.Name,
                City = t.City,
                Theme = t.Theme,
                Reviews = t.Reviews,
                ImageUrls = t.ImageUrls,
                Price = t.Price,
                Description = t.Description,
                Activities = t.Activities,
                Includes = t.Includes,
                NotIncludes = t.NotIncludes,
                Safety = t.Safety,
                Languages = t.Languages,
                Duration = t.Duration,
                NumberOfPeople = t.NumberOfPeople,
                Address = t.Address
            })
            .FirstOrDefaultAsync();

        if (tour == null)
        {
            return NotFound($"Тур с ID {id} не найден");
        }

        return tour;
    }


    [HttpGet("filtered")]
    public async Task<ActionResult<IEnumerable<Tour>>> GetFilteredTours(
        [FromQuery] List<string>? themes,
        [FromQuery] List<string>? destinations,
        [FromQuery] List<string>? durations,
        [FromQuery] string? sortBy) // "popularity", "price_low_to_high", "price_high_to_low"
    {
        var query = _dbContext.Tours.AsQueryable();

        // Фильтрация по темам (через Activities)
        if (themes != null && themes.Any())
        {
            query = query.Where(t =>
                t.Activities != null &&
                themes.Any(th => t.Activities.Contains(th)));
        }

        // Фильтрация по городам назначения
        if (destinations != null && destinations.Any())
        {
            query = query.Where(t => destinations.Contains(t.City));
        }

        // Фильтрация по продолжительности
        if (durations != null && durations.Any())
        {
            query = query.Where(t =>
                t.Duration != null &&
                durations.Any(dur => t.Duration.Contains(dur)));
        }

        // Сортировка
        query = sortBy switch
        {
            "popularity" => query.OrderByDescending(t => t.Reviews),
            "price_low_to_high" => query.OrderBy(t => t.Price),
            "price_high_to_low" => query.OrderByDescending(t => t.Price),
            _ => query
        };

        var result = await query
            .Select(t => new Tour
            {
                Id = t.Id,
                Name = t.Name,
                City = t.City,
                Theme = t.Theme,
                Reviews = t.Reviews,
                ImageUrls = t.ImageUrls,
                Price = t.Price,
                Description = t.Description,
                Activities = t.Activities,
                Includes = t.Includes,
                NotIncludes = t.NotIncludes,
                Safety = t.Safety,
                Languages = t.Languages,
                Duration = t.Duration,
                NumberOfPeople = t.NumberOfPeople,
                Address = t.Address
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpGet("{tourId}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetTourComments(int tourId)
    {
        var comments = await _dbContext.Comments
            .Where(c => c.TourId == tourId)
            .Select(c => new Comment
            {
                Id = c.Id,
                TourId = c.TourId,
                UserName = c.UserName,
                AvatarUrl = c.AvatarUrl,
                CommentDate = c.CommentDate,
                Text = c.Text,
                Rating = c.Rating
            })
            .ToListAsync();

        if (!comments.Any())
        {
            return NotFound($"Комментарии для тура с ID {tourId} не найдены");
        }

        return Ok(comments);
    }
}
