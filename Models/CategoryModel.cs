namespace FitAwayApi.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
    }

    public class CategoriesWrapper
    {
        public List<CategoryModel> Categories { get; set; }
    }
}
