namespace FitAwayApi.Models
{
    public class VideoModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }    
        public string Title { get; set; }
        public string Url { get; set; }
        public List<VideoDescription> Description { get; set; }
    }

    public class VideoDescription
    {
        public string ChannelName { get; set; }
        public string Paragraph { get; set; }
        public string Length { get; set; }
    }

    

    public class VideosWrapper
    {
        public List<VideoModel> Videos { get; set; }
    }

}
