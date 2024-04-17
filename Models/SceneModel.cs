namespace FitAwayApi.Models
{
    public class SceneModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class ScenesWrapper
    {
        public List<SceneModel> Scenes { get; set; }
    }

}
