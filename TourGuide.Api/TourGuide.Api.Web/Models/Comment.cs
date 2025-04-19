namespace TourGuide.Api.Web.Models;

public class Comment
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public string UserName { get; set; }
    public string AvatarUrl { get; set; }
    public string CommentDate { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
}